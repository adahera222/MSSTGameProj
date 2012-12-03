using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterFactory
{
	static MZCharacterFactory _instance = null;
//	int currentIndex = 0;
//	List<GameObject> gameObjectsPool = new List<GameObject>();

	static public MZCharacterFactory GetInstance()
	{
		if( _instance == null )
			_instance = new MZCharacterFactory();
		return _instance;
	}

	public GameObject CreateCharacter(MZCharacterType type, string name, string settingName)
	{
		string containerName = GetContainerNameByType( type );

		GameObject characterObject = CreateMZCharacterGameObject( "MZCharacter", containerName, type );
		SetCharacterToSetting( characterObject, settingName, type );
		characterObject.name = ( name != null )? name : "DefaultCharacter";

		return characterObject;
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

	private MZCharacterFactory()
	{

	}

	GameObject CreateMZCharacterGameObject(string characterName, string gameContainerName, MZCharacterType type)
	{
		if( type == MZCharacterType.EnemyBullet )
		{
			return test_sepcial_for_EB(characterName, gameContainerName);
		}

		GameObject characterObject = new GameObject();
		GameObject gameContainer = GameObject.Find( gameContainerName );

		MZDebug.Assert( gameContainer != null, "gameContainer not found (" + gameContainerName + ")" );

		characterObject.transform.parent = gameContainer.transform;
		characterObject.transform.position = new Vector3( 9999, 9999, MZGameSetting.GetCharacterDepth( type ) );

		characterObject.AddComponent<MZCharacter>();
		characterObject.GetComponent<MZCharacter>().characterType = type;

		GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().Add( type, characterObject );

		return characterObject;
	}

	void SetCharacterToSetting(GameObject characterObject, string settingName, MZCharacterType characterType)
	{
		CharacterSettingBase setting = (CharacterSettingBase)MZObjectHelp.CreateClass( settingName );
		MZDebug.Assert( setting != null, "setting is null, name=" + settingName );

		setting.SetToCharacter( characterObject, characterType );
	}

	GameObject test_sepcial_for_EB(string characterName, string gameContainerName)
	{
		MZDebug.Log( "sp for eb" );

		GameObject characterObject = MZResources.InstantiateMZGameCoreObject( "EnemyBullet" );
		if( characterObject == null )
			MZDebug.Log( "NULL???" );

		GameObject gameContainer = GameObject.Find( gameContainerName );

		characterObject.transform.parent = gameContainer.transform;
		characterObject.transform.position = new Vector3( 9999, 9999, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyBullet ) );

		characterObject.AddComponent<MZCharacter>();
		characterObject.GetComponent<MZCharacter>().characterType = MZCharacterType.EnemyBullet;

		GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().Add( MZCharacterType.EnemyBullet, characterObject );

		SetCharacterToSetting( characterObject, "EnemyBullet001Setting", MZCharacterType.EnemyBullet );

		return characterObject;

	}
}