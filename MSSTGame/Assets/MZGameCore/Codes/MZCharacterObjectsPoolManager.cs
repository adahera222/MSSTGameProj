using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterObjectsPoolManager
{
	static MZCharacterObjectsPoolManager _instance = null;
	Dictionary<MZCharacterType, MZCharacterObjectsList> _characterObjectsListByType = null;
	Dictionary<MZCharacterType, GameObject> _charactersContainerByType = null;

	public Dictionary<MZCharacterType, MZCharacterObjectsList> characterObjectsListByType
	{
		get	{ return _characterObjectsListByType;}
	}

	static public MZCharacterObjectsPoolManager GetInstance()
	{
		if( _instance == null )
			_instance = new MZCharacterObjectsPoolManager();

		return _instance;
	}

	public void Init()
	{
		_charactersContainerByType = new Dictionary<MZCharacterType, GameObject>();
		_charactersContainerByType.Add( MZCharacterType.Player, GameObject.Find( "MZPlayers" ) );
		_charactersContainerByType.Add( MZCharacterType.PlayerBullet, GameObject.Find( "MZPlayerBullets" ) );
		_charactersContainerByType.Add( MZCharacterType.EnemyAir, GameObject.Find( "MZEnemiesAir" ) );
		_charactersContainerByType.Add( MZCharacterType.EnemyBullet, GameObject.Find( "MZEnemyBullets" ) );
	}

	public GameObject GetCharacterObject(MZCharacterType characterType)
	{
		MZDebug.Assert( _characterObjectsListByType.ContainsKey( characterType ) == true, "not found type=" + characterType.ToString() );
		return _characterObjectsListByType[ characterType ].GetCharacterObject();
	}

	public void ReturnCharacterObject(GameObject characterObject, MZCharacterType characterType)
	{
		MZDebug.Assert( _characterObjectsListByType.ContainsKey( characterType ) == true, "not found type=" + characterType.ToString() );
		_characterObjectsListByType[ characterType ].ReturnCharacterObject( characterObject );
	}

	public GameObject[] GetCharacterObjectsList(MZCharacterType characterType)
	{
		MZDebug.Assert( _characterObjectsListByType.ContainsKey( characterType ) == true, "not found type=" + characterType.ToString() );
		return _characterObjectsListByType[ characterType ].characterObjectsList;
	}

	public int GetListCount(MZCharacterType characterType)
	{
		MZDebug.Assert( _characterObjectsListByType.ContainsKey( characterType ) == true, "not found type=" + characterType.ToString() );
		return _characterObjectsListByType[ characterType ].number;
	}

	public MZCharacter[] GetCharacterList(MZCharacterType characterType)
	{
		MZDebug.Assert( _characterObjectsListByType.ContainsKey( characterType ) == true, "not found type=" + characterType.ToString() );
		return _characterObjectsListByType[ characterType ].charactersList;
	}

	private MZCharacterObjectsPoolManager()
	{
	}

	public void SetGameObjectsArray(MZCharacterType characterType, int number)
	{
		if( _characterObjectsListByType == null )
			_characterObjectsListByType = new Dictionary<MZCharacterType, MZCharacterObjectsList>();

		_characterObjectsListByType.Add( characterType, new MZCharacterObjectsList( characterType, number, _charactersContainerByType[ characterType ] ) );
	}

	public class MZCharacterObjectsList
	{
		int _number = -1;
		MZCharacterType _characterType;
		GameObject _parentObject = null;
		GameObject[] _characterObjectsList = null;
		MZCharacter[] _charactersList = null;

		public int number
		{ get { return _number; } }

		public GameObject[] characterObjectsList
		{ get { return _characterObjectsList; } }

		public MZCharacter[] charactersList
		{ get { return _charactersList; } }

		public MZCharacterType characterType
		{ get { return _characterType; } }

		public MZCharacterObjectsList(MZCharacterType characterType, int number, GameObject parentObject)
		{
			_characterType = characterType;
			_number = number;
			_characterObjectsList = new GameObject[_number];
			_charactersList = new MZCharacter[_number];
			_parentObject = parentObject;

			for( int i = 0; i < number; i++ )
			{
				_characterObjectsList[ i ] = new GameObject();
				_characterObjectsList[ i ].active = false;
				_characterObjectsList[ i ].transform.parent = _parentObject.transform;
				_charactersList[ i ] = (MZCharacter)_characterObjectsList[ i ].AddComponent( GetCharacterScriptNameByType( characterType ) );
				_charactersList[ i ].characterType = characterType;
				_charactersList[ i ].poolIndex = i;
			}
		}

		public GameObject GetCharacterObject()
		{
			for( int i = 0; i < _number; i++ )
			{
				if( characterObjectsList[ i ].active == false )
				{
					characterObjectsList[ i ].active = true;
//					charactersList[ i ].Enable();
					return characterObjectsList[ i ];
				}
			}

			MZDebug.Assert( false, "can not get valid characterObject in pool" );
			return null;
		}

		public void ReturnCharacterObject(GameObject characterObject)
		{
			characterObject.active = false;
		}

		string GetCharacterScriptNameByType(MZCharacterType type)
		{
			switch( type )
			{
				case MZCharacterType.Player:
					return "MZPlayer";

				case MZCharacterType.EnemyAir:
				case MZCharacterType.EnemyGround:
					return "MZEnemy";

				case MZCharacterType.PlayerBullet:
				case MZCharacterType.EnemyBullet:
					return "MZBullet";

				case MZCharacterType.Unknow:
				default:
					MZDebug.Assert( false, "Not support type=" + type.ToString() );
					return null;
			}
		}
	}
}
