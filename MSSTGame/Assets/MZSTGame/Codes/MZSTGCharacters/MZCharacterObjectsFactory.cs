using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZCharacterObjectsFactory : MZSingleton<MZCharacterObjectsFactory>
{
	public Dictionary<MZCharacterType, List<string>> characterObjectNamesByType = null;

	//

	Dictionary<MZCharacterType, Dictionary<string, MZPool<GameObject>>> _charactersPoolsDictionaryByType = null;
	Dictionary<MZCharacterType, Transform> _charactersParentTransformByType = null;
	MZOnCreatedCharacterStates onCreatedCharacterStates = new MZOnCreatedCharacterStates();

	public void Init()
	{
		// maybe someday ... we can remove in hierarchy, and create it by code ... :D
		_charactersParentTransformByType = new Dictionary<MZCharacterType, Transform>();
		_charactersParentTransformByType.Add( MZCharacterType.Player, GameObject.Find( "MZPlayers" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.PlayerBullet, GameObject.Find( "MZPlayerBullets" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.EnemyAir, GameObject.Find( "MZEnemiesAir" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.EnemyBullet, GameObject.Find( "MZEnemyBullets" ).transform );
	}

	public void Add(MZCharacterType type, string name, int number)
	{
		MZDebug.Assert( _charactersParentTransformByType != null, "You should call Init() first" );

		OT.PreFabricate( name, number );

		if( _charactersPoolsDictionaryByType == null )
			_charactersPoolsDictionaryByType = new Dictionary<MZCharacterType, Dictionary<string, MZPool<GameObject>>>();

		if( _charactersPoolsDictionaryByType.ContainsKey( type ) == false )
		{
			Dictionary<string, MZPool<GameObject>> newPoolDict = new Dictionary<string, MZPool<GameObject>>();
			_charactersPoolsDictionaryByType.Add( type, newPoolDict );
		}

		Dictionary<string, MZPool<GameObject>> poolDict = _charactersPoolsDictionaryByType[ type ];

		MZDebug.Assert( poolDict.ContainsKey( name ) == false, "already have this, name=" + name );

		onCreatedCharacterStates.Set( name, type );

		MZPool<GameObject> newPool = new MZPool<GameObject>();
		newPool.createNewObjectHandle = new MZPool<GameObject>.CreateNewObject( CreateNewGameObject );
		newPool.onGetValidItemHandle = new MZPool<UnityEngine.GameObject>.OnGetValidItem( OnCharacterObjectBecomeVaild );
		newPool.onReturnItemHandle = new MZPool<UnityEngine.GameObject>.OnReturnItem( OnCharacterObjectRemove );
		newPool.CreateContent( number );

		onCreatedCharacterStates.Restore();

		poolDict.Add( name, newPool );

		if( characterObjectNamesByType == null )
			characterObjectNamesByType = new Dictionary<MZCharacterType, List<string>>();
		if( characterObjectNamesByType.ContainsKey( type ) == false )
			characterObjectNamesByType.Add( type, new List<string>() );

		characterObjectNamesByType[ type ].Add( name );
	}

	public GameObject Get(MZCharacterType type, string name)
	{
		MZDebug.Assert( _charactersPoolsDictionaryByType != null && _charactersPoolsDictionaryByType.ContainsKey( type ) == true, "null not contain key(" + name + ")" );
		MZDebug.Assert( _charactersPoolsDictionaryByType.ContainsKey( type ) == true, "type not exist, type=" + type.ToString() );
		MZDebug.Assert( _charactersPoolsDictionaryByType[ type ].ContainsKey( name ) == true, "name not exist, name=" + name );

		GameObject characterObject = _charactersPoolsDictionaryByType[ type ][ name ].GetValidItem();
		MZDebug.Assert( characterObject != null, "characterObject is null" );

//		MZGameComponents.instance.charactersManager.Add( type, characterObject.GetComponent<MZCharacter>() );

		return characterObject;
	}

	public void Remove(MZCharacterType type, string name, GameObject characterObject)
	{
		MZDebug.Assert( _charactersPoolsDictionaryByType != null && _charactersPoolsDictionaryByType.ContainsKey( type ) == true, "null not contain key(" + name + ")" );
		_charactersPoolsDictionaryByType[ type ][ name ].Return( characterObject );
	}

	#region handle function of MZPool<GameObject>

	GameObject CreateNewGameObject()
	{
		MZDebug.Assert( onCreatedCharacterStates != null && onCreatedCharacterStates.hasSet == true, "must set create state first" );

		GameObject newObject = OT.CreateObject( onCreatedCharacterStates.name );
		MZDebug.Assert( newObject != null, "create newObject fail, name=" + onCreatedCharacterStates.name );

		newObject.active = false;
		newObject.transform.parent = _charactersParentTransformByType[ onCreatedCharacterStates.type ];

		MZCharacter character = newObject.GetComponent<MZCharacter>();
		character.InitCharacterPartsData();

		character.characterType = onCreatedCharacterStates.type;
		character.depth = MZGameSetting.GetDepthOfCharacter( onCreatedCharacterStates.type );
		character.position = MZGameSetting.INVALID_POSITIONV2;
		character.name = onCreatedCharacterStates.name;
		character.renderEnable = false;

		return newObject;
	}

	void OnCharacterObjectBecomeVaild(GameObject characterObject)
	{
		characterObject.active = true;

		MZCharacter characterScript = characterObject.GetComponent<MZCharacter>();
		characterScript.renderEnable = true;
		characterScript.Clear();
	}

	void OnCharacterObjectRemove(GameObject characterObject)
	{
		characterObject.active = false;
		characterObject.GetComponent<MZCharacter>().renderEnable = false;
		characterObject.GetComponent<MZCharacter>().position = MZGameSetting.INVALID_POSITIONV2;
	}

	#endregion

	//
	class MZOnCreatedCharacterStates
	{
		bool _hasSet = false;
		string _name = null;
		MZCharacterType _type = MZCharacterType.Unknow;

		public bool hasSet
		{ get { return _hasSet; } }

		public string name
		{ get { return _name; } }

		public MZCharacterType type
		{ get { return _type; } }

		public void Set(string name, MZCharacterType type)
		{
			_name = name;
			_type = type;

			_hasSet = true;
		}

		public void Restore()
		{
			_name = null;
			_type = MZCharacterType.Unknow;

			_hasSet = false;
		}
	}
}