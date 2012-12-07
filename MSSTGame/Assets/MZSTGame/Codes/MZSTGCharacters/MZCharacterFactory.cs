using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		GameObject characterObject = CreateCharacterGameObject( "MZCharacter", type );
		SetCharacterToSetting( characterObject, settingName, type );
		characterObject.name = ( name != null )? name : "DefaultCharacter";

		return characterObject;
	}

	private MZCharacterFactory()
	{

	}

	GameObject CreateCharacterGameObject(string characterName, MZCharacterType type)
	{
		GameObject characterObject = new GameObject();
		MZCharacter character = (MZCharacter)characterObject.AddComponent( GetCharacterScriptNameByType( type ) );
		character.characterType = type;

		GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().Add( type, characterObject );

		return characterObject;
	}

	string GetCharacterScriptNameByType(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return "MZPlayer";

			case MZCharacterType.PlayerBullet:
				return "MZPlayerBullet";

			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return "MZEnemy";

			case MZCharacterType.EnemyBullet:
				return "MZEnemyBullet";

			case MZCharacterType.Unknow:
			default:
				MZDebug.Assert( false, "Not support type=" + type.ToString() );
				return null;
		}
	}

	void SetCharacterToSetting(GameObject characterObject, string settingName, MZCharacterType characterType)
	{
		CharacterSettingBase setting = (CharacterSettingBase)MZObjectHelp.CreateClass( settingName );
		MZDebug.Assert( setting != null, "setting is null, name=" + settingName );

		setting.SetToCharacter( characterObject, characterType );
	}
}