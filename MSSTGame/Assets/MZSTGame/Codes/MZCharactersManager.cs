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

	// test
	int enemyBulletCanUpdateNumberPerStep = 100;
	public int enemyBulletCanUpdateStart = 0;
	public int enemyBulletCanUpdateEnd = 100;

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

	}

	void Start()
	{

	}

	void Update()
	{
		// test
		enemyBulletCanUpdateStart += enemyBulletCanUpdateNumberPerStep;

		if( enemyBulletCanUpdateStart + enemyBulletCanUpdateNumberPerStep >= MZCharacterObjectsPoolManager.GetInstance().GetCharacterObjectsListCount( MZCharacterType.EnemyBullet ) )
		{
			enemyBulletCanUpdateStart = 0;
		}

		enemyBulletCanUpdateEnd = enemyBulletCanUpdateStart + enemyBulletCanUpdateNumberPerStep;

		MZDebug.Log( "Update range: " + enemyBulletCanUpdateStart.ToString() + " to " + enemyBulletCanUpdateEnd.ToString() );
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

		string infoText = "PB: " + playerBulletNumber.ToString() + "\n" +
			"E: " + enemyNumber.ToString() + "\n" +
			"EB: " + enemyBulletNumber.ToString();

		guiCharactersInfo.text = infoText;
	}
}