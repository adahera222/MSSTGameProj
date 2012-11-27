using UnityEngine;
using System.Collections;

public class MZCharacterFactory
{
	public enum MZCharacterType
	{
		Unknow,
		Player,
		PlayerBullet,
		EnemyAir,
		EnemyGround,
		EnemyBullet,
	}

	static MZCharacterFactory instance;

	static public MZCharacterFactory GetInstance()
	{
		if( instance == null )
			instance = new MZCharacterFactory();
		return instance;
	}

	static public int GetDepth(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return -30;

			case MZCharacterType.PlayerBullet:
				return -20;

			case MZCharacterType.EnemyAir:
				return -50;

			default:
				return 0;
		}
	}

	static public Color GetCollisionColor(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return Color.red;

			case MZCharacterType.PlayerBullet:
				return Color.cyan;

			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return Color.green;

			case MZCharacterType.EnemyBullet:
				return Color.blue;

			default:
				return Color.white;
		}
	}

	public GameObject CreateCharacter(MZCharacterType type, string name)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return CreatePlayer( name );

			case MZCharacterType.PlayerBullet:
				return CreatePlayerBullet( name );

			case MZCharacterType.EnemyAir:
				return CreateEnemyAir( name );
		}

		MZDebug.Assert( false, "undefine character type: " + type.ToString() );
		return null;
	}

	private MZCharacterFactory()
	{

	}

	GameObject CreatePlayer(string name)
	{
		GameObject player = CreateMZCharacter( "MZCharacter", "MZPlayers", MZCharacterType.Player );

		player.AddComponent<MZPlayer>();

		MZCharacterPartSetting partSetting = new MZCharacterPartSetting();
		partSetting.name = "MainBody";
//		partSetting.frameName = "Donut_normal0001";
		partSetting.scale = 0.5f;
		partSetting.rotation = 90;
		partSetting.animationName = "[Celestial]_Army_med2_normal";
		partSetting.animationSpeed = 1;

		player.GetComponent<MZCharacter>().AddPart( partSetting );
		player.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );
		player.name = ( name != null )? name : "Player";

		return player;
	}

	GameObject CreatePlayerBullet(string name)
	{
		GameObject playerBullet = CreateMZCharacter( "MZCharacter", "MZPlayerBullets", MZCharacterType.PlayerBullet );

		playerBullet.AddComponent<MZPlayerBullet>();

		MZCharacterPartSetting partSetting = new MZCharacterPartSetting();
		partSetting.name = "MainBody";
		partSetting.frameName = "Donut_normal0001";
		partSetting.scale = 0.3f;
//		partSetting.scaleX = 2.0f;
//		partSetting.scaleY = 0.5f;
		partSetting.rotation = 90;
//		partSetting.animationName = "[Celestial]_Army_med2_normal";
//		partSetting.animationSpeed = 0.1f;

		playerBullet.GetComponent<MZCharacter>().AddPart( partSetting );
		playerBullet.name = ( name != null )? name : "PlayerBullet";

		playerBullet.GetComponent<MZCharacter>().position = GameObject.Find( "Player" ).GetComponent<MZCharacter>().position;

		return playerBullet;
	}

	GameObject CreateEnemyAir(string name)
	{
		GameObject enemy = CreateMZCharacter( "MZCharacter", "MZEnemiesAir", MZCharacterType.EnemyAir );

		enemy.AddComponent<MZEnemy>();

		MZCharacterPartSetting partSetting = new MZCharacterPartSetting();
		partSetting.name = "MainBody";
//		partSetting.frameName = "Donut_normal0001";
		partSetting.scale = 0.6f;
//		partSetting.scaleX = 2.0f;
//		partSetting.scaleY = 0.5f;
		partSetting.rotation = 270;
		partSetting.animationName = "[Celestial]_Army_med3_normal";
//		partSetting.animationSpeed = 0.1f;

		enemy.GetComponent<MZCharacter>().AddPart( partSetting );
		enemy.name = ( name != null )? name : "Enemy";

		return enemy;
	}

	GameObject CreateMZCharacter(string characterName, string gameContainerName, MZCharacterType type)
	{
		GameObject character = MZResources.InstantiateMZGameCoreObject( characterName );
		GameObject gameContainer = GameObject.Find( gameContainerName );

		MZDebug.Assert( gameContainer != null, "gameContainer not found (" + gameContainerName + ")" );

		character.transform.parent = gameContainer.transform;
		character.transform.position = new Vector3( 9999, 9999, GetDepth( type ) );

		character.GetComponent<MZCharacter>().characterType = type;

		GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().Add( type, character );

		return character;
	}
}