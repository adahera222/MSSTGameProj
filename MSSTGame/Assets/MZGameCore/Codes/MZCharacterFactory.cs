using System.Collections;

public class MZCharacterFactory
{
	// depth
	public enum CharacterType
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
}