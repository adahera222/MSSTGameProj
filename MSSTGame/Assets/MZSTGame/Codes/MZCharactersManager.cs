using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;
	Dictionary<MZCharacterType, List<GameObject>> _charactersListByType;
	MZCharacter _playerCharacter = null;
	Dictionary<MZCharacterType, GameObject> _charactersContainerByType = null;

	public MZCharacter playerCharacter
	{
		set{ _playerCharacter = value; }
		get{ return _playerCharacter; }
	}

	public int Add(MZCharacterType characterType, GameObject characterObject)
	{
		if( _charactersListByType == null )
		{
			_charactersListByType = new Dictionary<MZCharacterType, List<GameObject>>();
		}

		if( _charactersListByType.ContainsKey( characterType ) == false )
		{
			List<GameObject> newCharacterList = new List<GameObject>();
			_charactersListByType.Add( characterType, newCharacterList );
		}

//		List<GameObject> characterList = _charactersListByType[ characterType ];
//		characterList.Add( characterObject );
//
//		GameObject charactersContainer = GameObject.Find( GetContainerNameByType( characterType ) );
//		MZDebug.Assert( charactersContainer != null, "charactersContainer is null, type=" + characterType.ToString() );
//		characterObject.transform.parent = charactersContainer.transform;

		characterObject.transform.parent = _charactersContainerByType[ characterType ].transform;

		if( characterType == MZCharacterType.Player )
		{
			_playerCharacter = characterObject.GetComponent<MZCharacter>();
		}

//		return characterList.Count;
		return 0;
	}

	public GameObject GetPlayer()
	{
		if( _charactersListByType.ContainsKey( MZCharacterType.Player ) == false || _charactersListByType[ MZCharacterType.Player ].Count == 0 )
			return null;

		return _charactersListByType[ MZCharacterType.Player ][ 0 ];
	}

	public Vector2 GetPlayerPosition()
	{
		return _playerCharacter.position;
	}

	public string GetContainerNameByType(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return "MZPlayers";

			case MZCharacterType.PlayerBullet:
				return "MZPlayerBullets";

			case MZCharacterType.EnemyAir:
				return "MZEnemiesAir";

			case MZCharacterType.EnemyBullet:
				return "MZEnemyBullets";

			default:
				MZDebug.Assert( false, "Undefine type: " + type.ToString() );
				return "";
		}
	}

	public List<GameObject> GetList(MZCharacterType type)
	{
		if( _charactersListByType.ContainsKey( type ) == false )
			return new List<GameObject>();

		return _charactersListByType[ type ];
	}

	public void Remove(MZCharacterType type, GameObject character)
	{
		List<GameObject> list = GetList( type );
		MZDebug.Assert( list.Contains( character ) == true, "character not in list, name=" + character.name + ", type=" + type.ToString() );

		list.Remove( character );
		Destroy( character );
	}

	void Awake()
	{
		_charactersContainerByType = new Dictionary<MZCharacterType, GameObject>();
		_charactersContainerByType.Add( MZCharacterType.Player, GameObject.Find( "MZPlayers" ) );
		_charactersContainerByType.Add( MZCharacterType.PlayerBullet, GameObject.Find( "MZPlayerBullets" ) );
		_charactersContainerByType.Add( MZCharacterType.EnemyAir, GameObject.Find( "MZEnemiesAir" ) );
		_charactersContainerByType.Add( MZCharacterType.EnemyBullet, GameObject.Find( "MZEnemyBullets" ) );
	}

	void Start()
	{

	}

	void Update()
	{

	}

	void LateUpdate()
	{
		RemoveDisableCharacterObject( MZCharacterType.Player );
		RemoveDisableCharacterObject( MZCharacterType.PlayerBullet );
		RemoveDisableCharacterObject( MZCharacterType.EnemyAir );
		RemoveDisableCharacterObject( MZCharacterType.EnemyBullet );
	}

	void RemoveDisableCharacterObject(MZCharacterType type)
	{
		MZCharacter[] charactersList = MZCharacterObjectsPoolManager.GetInstance().GetCharacterList( type );
		GameObject[] characterObjectsList = MZCharacterObjectsPoolManager.GetInstance().GetCharacterObjectsList( type );
		int listCount = MZCharacterObjectsPoolManager.GetInstance().GetCharacterObjectsListCount( type );

		for( int i = 0; i < listCount; i++ )
		{
			if( characterObjectsList[ i ].active == true && charactersList[ i ].isActive == false )
			{
				charactersList[ i ].Clear();
				MZCharacterObjectsPoolManager.GetInstance().ReturnCharacterObject( characterObjectsList[ i ], type );
				characterObjectsList[ i ].active = false;
			}
		}
	}


//	void RemoveUnActiveCharacters()
//	{
//		if( _charactersListByType == null || _charactersListByType.Count == 0 )
//			return;

//		foreach( List<GameObject> list in _charactersListByType.Values )
//		{
//			for( int i = 0; i < list.Count; i++ )
//			{
//				GameObject characterObject = list[ i ];
//				if( characterObject.GetComponent<MZCharacter>().isActive == false )
//				{
//					characterObject.GetComponent<MZCharacter>().BeforeDestory();
//					list.Remove( characterObject );
//					Destroy( characterObject );
//					i--;
//				}
//			}
//		}

//		GameObject[] ebList = MZOTSpritesPoolManager.GetInstance().GetSpritesList( MZCharacterType.EnemyBullet );
//		List<GameObject> ebList = _charactersListByType[MZCharacterType.EnemyBullet];
//		foreach( GameObject eb in ebList )
//		{
//			if( eb.GetComponent<MZCharacter>() == null )
//			{
//				MZDebug.Log( "????" );
//			}

//			if( eb.active == true && eb.GetComponent<MZCharacter>().isActive == false )
//			{
//				eb.GetComponent<MZCharacter>().BeforeDestory();
//				eb.active = false;
//			}
//		}
//	}

	void OnGUI()
	{
		if( guiCharactersInfo == null || _charactersListByType == null || _charactersListByType.Count == 0 )
			return;

		string infoText = "";

		foreach( MZCharacterType type in _charactersListByType.Keys )
		{
			List<GameObject> list = _charactersListByType[ type ];
			infoText += type.ToString() + ": count=" + list.Count.ToString() + "\n";
		}

		guiCharactersInfo.text = infoText;
	}
}