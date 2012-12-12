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
		GameObject characterObject = MZCharacterObjectsPoolManager.GetInstance().GetCharacterObject( type );

		SetCharacterToSetting( characterObject, settingName );
		characterObject.name = ( name != null )? name : "DefaultCharacter";

		MZGameComponents.GetInstance().charactersManager.Add( type, characterObject.GetComponent<MZCharacter>() );

		return characterObject;
	}

	private MZCharacterFactory()
	{

	}

	void SetCharacterToSetting(GameObject characterObject, string settingName)
	{
		CharacterSettingBase setting = (CharacterSettingBase)MZObjectHelp.CreateClass( settingName );
		MZDebug.Assert( setting != null, "setting is null, name=" + settingName );

		setting.SetToCharacter( characterObject );
	}
}