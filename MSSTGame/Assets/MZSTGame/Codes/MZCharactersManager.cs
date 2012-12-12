using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;
	public int playerBulletNumber = 0;
	public int enemyNumber = 0;
	public int enemyBulletNumber = 0;
	//
	GameObject _player = null;
	MZCharacter _playerCharacter = null;
	Dictionary<MZCharacterType, List<MZCharacter>> _dicActiveCharactersListByType = null;
	MZCharactersCollisionTest _enemyBulletAndPlayerCollisionTest = null;
	MZCharactersCollisionTest _playerBulletAndEnemyCollisionTest = null;

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

	public void Add(MZCharacterType characterType, MZCharacter chracter)
	{
		MZDebug.Assert( _dicActiveCharactersListByType != null, "_dicActiveCharactersListByType is null" );
		MZDebug.Assert( _dicActiveCharactersListByType.ContainsKey( characterType ) != false, "characterType(" + characterType.ToString() + ") is not support" );

		_dicActiveCharactersListByType[ characterType ].Add( chracter );
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
		_dicActiveCharactersListByType = new Dictionary<MZCharacterType, List<MZCharacter>>();

		foreach( MZCharacterType type in System.Enum.GetValues( typeof(MZCharacterType) ) )
		{
			List<MZCharacter> list = new List<MZCharacter>();
			_dicActiveCharactersListByType.Add( type, list );
		}

		_enemyBulletAndPlayerCollisionTest = new MZCharactersCollisionTest();
		_enemyBulletAndPlayerCollisionTest.splitUpdateList = _dicActiveCharactersListByType[ MZCharacterType.EnemyBullet ];
		_enemyBulletAndPlayerCollisionTest.fullUpdateList = _dicActiveCharactersListByType[ MZCharacterType.Player ];
		_enemyBulletAndPlayerCollisionTest.onCollide = new MZCharactersCollisionTest.OnCollideHandler( OnEnemyBulletCollidePlayer );

		_playerBulletAndEnemyCollisionTest = new MZCharactersCollisionTest();
		_playerBulletAndEnemyCollisionTest.splitUpdateList = _dicActiveCharactersListByType[ MZCharacterType.PlayerBullet ];
		_playerBulletAndEnemyCollisionTest.fullUpdateList = _dicActiveCharactersListByType[ MZCharacterType.EnemyAir ];
		_playerBulletAndEnemyCollisionTest.onCollide = new MZCharactersCollisionTest.OnCollideHandler( OnPlayerBulletCollideEnemy );
	}

	void Start()
	{

	}

	void Update()
	{
		_enemyBulletAndPlayerCollisionTest.CollisionTest();
		_playerBulletAndEnemyCollisionTest.CollisionTest();
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
		List<MZCharacter> charactersList = _dicActiveCharactersListByType[ type ];

		for( int i = 0; i < charactersList.Count; i++ )
		{
			if( charactersList[ i ].isActive == false )
			{
				charactersList[ i ].Clear();
				MZCharacterObjectsPoolManager.GetInstance().ReturnCharacterObject( charactersList[ i ].gameObject, type );
//				charactersList.Remove( charactersList[ i ] );
				i--;
			}
		}
	}

	void OnGUI()
	{
		if( !MZGameSetting.SHOW_CHARACTERS_INFO )
			return;

		if( guiCharactersInfo == null && _dicActiveCharactersListByType == null )
			return;

		string infoText = "";

		foreach( MZCharacterType type in _dicActiveCharactersListByType.Keys )
		{
			string info = type.ToString() + ": " + _dicActiveCharactersListByType[ type ].Count + "\n";
			infoText += info;
		}

		guiCharactersInfo.text = infoText;
	}

	void OnEnemyBulletCollidePlayer(MZCharacter enemyBullet, MZCharacter player)
	{
		enemyBullet.Disable();
	}

	void OnPlayerBulletCollideEnemy(MZCharacter playerBullet, MZCharacter enemy)
	{
		enemy.GetComponent<MZEnemy>().TakenDamage( playerBullet.GetComponent<MZBullet>().strength );
		playerBullet.Disable();
	}
}