using UnityEngine;
using System.Collections;

public class MZCharacterFactory
{
	static MZCharacterFactory _instance = null;

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
				return "MZEnemiesAir";

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