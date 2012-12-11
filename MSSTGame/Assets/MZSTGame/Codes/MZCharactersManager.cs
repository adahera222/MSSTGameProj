using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;
	public int playerBulletNumber = 0;
	public int enemyNumber = 0;
	public int enemyBulletNumber = 0;
	GameObject _player = null;
	MZCharacter _playerCharacter = null;
	Dictionary<MZCharacterType, GameObject> _charactersContainerByType = null;

	public GameObject playerObject
	{
		set{ _player = value; }
		get{ return _player; }
	}

	public MZCharacter playerCharacter
	{
		set{ _playerCharacter = value; }
		get{ return _playerCharacter; }
	}

	public Vector2 GetPlayerPosition()
	{
		return ( _playerCharacter != null )? _playerCharacter.position : Vector2.zero;
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

	void OnGUI()
	{
		if( guiCharactersInfo == null /*|| _charactersListByType == null || _charactersListByType.Count == 0*/ )
			return;

		string infoText = "";

//		foreach( MZCharacterType type in _charactersListByType.Keys )
//		{
//			List<GameObject> list = _charactersListByType[ type ];
//			infoText += type.ToString() + ": count=" + list.Count.ToString() + "\n";
//		}

		guiCharactersInfo.text = infoText;
	}
}