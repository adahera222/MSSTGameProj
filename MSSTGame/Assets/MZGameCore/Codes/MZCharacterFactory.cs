using UnityEngine;
using System.Collections;

public class MZCharacterFactory
{
	// depth
	public enum MZCharacterType
	{
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
		}

		MZDebug.Assert( false, "undefine character type: " + type.ToString() );
		return null;
	}

	public UnityEngine.GameObject _test_create_characterPart()
	{
		MZCharacterPartSetting setting = new MZCharacterPartSetting();
		setting.animationName = "Donut_normal";
		setting.name = "call me test";

		UnityEngine.GameObject prefab = MZResources.InstantiateMZGameCoreObject( "MZCharacterPart" );
		MZDebug.Assert( prefab != null, "prefab(" + "MZCharacterPart.prefab" + ") is null" );

		UnityEngine.GameObject clone = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate( prefab );
		clone.GetComponent<MZCharacterPart>().Init( setting );

		return clone;
	}

	public UnityEngine.GameObject _test_create_character()
	{
		MZCharacterPartSetting partSetting1 = new MZCharacterPartSetting();
		partSetting1.animationName = "Donut_normal";
		partSetting1.name = "Part1";
		partSetting1.position = new UnityEngine.Vector2( 50, 50 );

		UnityEngine.GameObject character = MZResources.InstantiateMZGameCoreObject( "MZCharacter" );
		character.GetComponent<MZCharacter>().AddPart( partSetting1 );
		character.transform.position = new UnityEngine.Vector3( -300, -490, -50 );

		return character;
	}

	private MZCharacterFactory()
	{

	}

	GameObject CreatePlayer(string name)
	{
		GameObject player = CreateMZCharacter( "MZCharacter", "MZPlayers" );

		player.AddComponent<MZPlayer>();

		MZCharacterPartSetting partSetting = new MZCharacterPartSetting();
		partSetting.name = "MainBody";
//		partSetting.frameName = "[Celestial]_Army_med2_normal0003";
		partSetting.animationName = "[Celestial]_Army_med2_normal";
//		partSetting.animationSpeed = 0.1f;

		player.GetComponent<MZCharacter>().AddPart( partSetting );
		player.transform.position = new Vector3( 0, 0, -30 );
		player.name = ( name != null )? name : "Player";

		return player;
	}

	GameObject CreateMZCharacter(string characterName, string gameContainerName)
	{
		GameObject character = MZResources.InstantiateMZGameCoreObject( characterName );
		GameObject gameContainer = GameObject.Find( gameContainerName );

		MZDebug.Assert( gameContainer != null, "gameContainer not found (" + gameContainerName + ")" );

		character.transform.parent = gameContainer.transform;

		return character;
	}
}