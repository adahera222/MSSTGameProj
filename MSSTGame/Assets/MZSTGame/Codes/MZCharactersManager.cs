using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;

	//

	GameObject _playerObject = null;
	MZCharacter _playerCharacter = null;
	Dictionary<MZCharacterType, List<MZCharacter>> _dicActiveCharactersListByType = null;
	MZCharactersCollisionTest<MZBullet, MZPlayer> _enemyBulletAndPlayerCollisionTest = null;
	MZCharactersCollisionTest<MZBullet, MZEnemy> _playerBulletAndEnemyCollisionTest = null;

	//

	public GameObject playerObject
	{
		get{ return _playerObject; }
	}

	public MZCharacter playerCharacter
	{
		get{ return _playerCharacter; }
	}

	public void Add(MZCharacterType characterType, MZCharacter character)
	{
		MZDebug.Assert( _dicActiveCharactersListByType != null, "_dicActiveCharactersListByType is null" );
		MZDebug.Assert( _dicActiveCharactersListByType.ContainsKey( characterType ) != false, "characterType(" + characterType.ToString() + ") is not support" );

		character.Enable();

		_dicActiveCharactersListByType[ characterType ].Add( character );

		if( characterType == MZCharacterType.Player )
		{
			AddPlayerCacheInfo( character );
		}
	}

	public void RemoveAllCharactersByType(MZCharacterType characterType)
	{
		foreach( MZCharacter character in  _dicActiveCharactersListByType[characterType] )
		{
			character.Disable();
		}
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

	//

	void Awake()
	{
		_dicActiveCharactersListByType = new Dictionary<MZCharacterType, List<MZCharacter>>();

		foreach( MZCharacterType type in System.Enum.GetValues( typeof(MZCharacterType) ) )
		{
			List<MZCharacter> list = new List<MZCharacter>();
			_dicActiveCharactersListByType.Add( type, list );
		}

		_enemyBulletAndPlayerCollisionTest = new MZCharactersCollisionTest<MZBullet, MZPlayer>();
		_enemyBulletAndPlayerCollisionTest.maxTestPerTime = 50;
		_enemyBulletAndPlayerCollisionTest.splitUpdateList = _dicActiveCharactersListByType[ MZCharacterType.EnemyBullet ];
		_enemyBulletAndPlayerCollisionTest.fullUpdateList = _dicActiveCharactersListByType[ MZCharacterType.Player ];
		_enemyBulletAndPlayerCollisionTest.preTest = new MZCharactersCollisionTest<MZBullet, MZPlayer>.PreTest( PreTestEnemyBulletCollidePlayer );
		_enemyBulletAndPlayerCollisionTest.onCollide = new MZCharactersCollisionTest<MZBullet, MZPlayer>.OnCollide( OnEnemyBulletCollidePlayer );

		_playerBulletAndEnemyCollisionTest = new MZCharactersCollisionTest<MZBullet, MZEnemy>();
		_playerBulletAndEnemyCollisionTest.splitUpdateList = _dicActiveCharactersListByType[ MZCharacterType.PlayerBullet ];
		_playerBulletAndEnemyCollisionTest.fullUpdateList = _dicActiveCharactersListByType[ MZCharacterType.EnemyAir ];
		_playerBulletAndEnemyCollisionTest.preTest = new MZCharactersCollisionTest<MZBullet, MZEnemy>.PreTest( PreTestPlayerBulletCollideEnemy );
		_playerBulletAndEnemyCollisionTest.onCollide = new MZCharactersCollisionTest<MZBullet, MZEnemy>.OnCollide( OnPlayerBulletCollideEnemy );
	}

	void AddPlayerCacheInfo(MZCharacter character)
	{
		_playerCharacter = character;
		_playerObject = character.gameObject;
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
				charactersList[ i ].OnRemoving();
				MZCharacterObjectsFactory.instance.Remove( type, charactersList[ i ].name, charactersList[ i ].gameObject );
				charactersList.Remove( charactersList[ i ] );
				i--;
			}
		}
	}

	void OnGUI()
	{
		if( !MZGameSetting.SHOW_CHARACTERS_INFO )
			return;

		if( guiCharactersInfo == null || _dicActiveCharactersListByType == null )
			return;

		string infoText = "";

		foreach( MZCharacterType type in _dicActiveCharactersListByType.Keys )
		{
			string info = type.ToString() + ": " + _dicActiveCharactersListByType[ type ].Count + "\n";
			infoText += info;
		}

		guiCharactersInfo.text = infoText;
	}

	bool PreTestEnemyBulletCollidePlayer(MZBullet enemyBullet, MZPlayer player)
	{
		return true;
	}

	bool PreTestPlayerBulletCollideEnemy(MZBullet playerBullet, MZEnemy enemy)
	{
		return !( enemy.isActive == false || enemy.currentHealthPoint <= 0 );
	}

	int _playerHitTime = 0;
	void OnEnemyBulletCollidePlayer(MZBullet enemyBullet, MZPlayer player)
	{
		enemyBullet.Disable();
		_playerHitTime++;

		MZDebug.Log( "Your hit by " + _playerHitTime.ToString() + " times" );
	}

	void OnPlayerBulletCollideEnemy(MZBullet playerBullet, MZEnemy enemy)
	{
		enemy.GetComponent<MZEnemy>().TakenDamage( playerBullet.GetComponent<MZBullet>().strength );
		playerBullet.Disable();
	}
}