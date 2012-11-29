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

			case MZCharacterType.EnemyBullet:
				return CreateEnemyBullet( name );
		}

		MZDebug.Assert( false, "undefine character type: " + type.ToString() );
		return null;
	}

	private MZCharacterFactory()
	{

	}

	GameObject CreatePlayer(string name)
	{
		GameObject player = CreateGameObjectMZCharacter( "MZCharacter", "MZPlayers", MZCharacterType.Player );
		SetCharacterToSetting( player, "PlayerType01Setting", MZCharacterType.Player );

		player.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );
		player.name = ( name != null )? name : "Player";

		return player;
	}

	GameObject CreatePlayerBullet(string name)
	{
		GameObject playerBullet = CreateGameObjectMZCharacter( "MZCharacter", "MZPlayerBullets", MZCharacterType.PlayerBullet );
		SetCharacterToSetting( playerBullet, "PlayerBullet001Setting", MZCharacterType.PlayerBullet );
		playerBullet.name = ( name != null )? name : "PlayerBullet";

		return playerBullet;
	}

	GameObject CreateEnemyAir(string name)
	{
		GameObject enemy = CreateGameObjectMZCharacter( "MZCharacter", "MZEnemiesAir", MZCharacterType.EnemyAir );
		SetCharacterToSetting( enemy, "Enemy001Setting", MZCharacterType.EnemyAir );
		enemy.name = ( name != null )? name : "Enemy";

		return enemy;
	}

	GameObject CreateEnemyBullet(string name)
	{
		GameObject enemyBullet = CreateGameObjectMZCharacter( "MZCharacter", "MZEnemyBullets", MZCharacterType.EnemyBullet );
		SetCharacterToSetting( enemyBullet, "EnemyBullet001Setting", MZCharacterType.EnemyBullet );
		enemyBullet.name = ( name != null )? name : "EnemyBullet";

		return enemyBullet;
	}

	// will remove
	GameObject CreateGameObjectMZCharacter(string characterName, string gameContainerName, MZCharacterType type)
	{
		GameObject character = MZResources.InstantiateMZGameCoreObject( characterName );
		GameObject gameContainer = GameObject.Find( gameContainerName );

		MZDebug.Assert( gameContainer != null, "gameContainer not found (" + gameContainerName + ")" );

		character.transform.parent = gameContainer.transform;
		character.transform.position = new Vector3( 9999, 9999, MZGameSetting.GetCharacterDepth( type ) );

		character.GetComponent<MZCharacter>().characterType = type;

		GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().Add( type, character );

		return character;
	}

	void SetCharacterToSetting(GameObject characterObject, string settingName, MZCharacterType characterType)
	{
		CharacterSettingBase setting = (CharacterSettingBase)MZObjectHelp.CreateClass( settingName );
		MZDebug.Assert( setting != null, "setting is null, name=" + settingName );

		setting.SetToCharacter( characterObject, characterType );
	}
}