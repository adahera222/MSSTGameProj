using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterObjectsFactory : MZSingleton<MZCharacterObjectsFactory>
{
	Dictionary<MZCharacterType, Dictionary<string, MZPool<GameObject>>> _charactersPoolsDictionaryByType = null;
	Dictionary<MZCharacterType, Transform> _charactersParentTransformByType = null;
	// create object states
	string _onCreateObjectName = null;
	MZCharacterType _onCreateCharacterType = MZCharacterType.Unknow;

	public void Init()
	{
		// maybe someday ... remove in hierarchy, and create it by code ... :D
		_charactersParentTransformByType = new Dictionary<MZCharacterType, Transform>();
		_charactersParentTransformByType.Add( MZCharacterType.Player, GameObject.Find( "MZPlayers" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.PlayerBullet, GameObject.Find( "MZPlayerBullets" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.EnemyAir, GameObject.Find( "MZEnemiesAir" ).transform );
		_charactersParentTransformByType.Add( MZCharacterType.EnemyBullet, GameObject.Find( "MZEnemyBullets" ).transform );
	}

	public void Add(MZCharacterType type, string name, int number)
	{
		MZDebug.Assert( _charactersParentTransformByType != null, "You should call Init() first" );

		if( _charactersPoolsDictionaryByType == null )
			_charactersPoolsDictionaryByType = new Dictionary<MZCharacterType, Dictionary<string, MZPool<GameObject>>>();

		if( _charactersPoolsDictionaryByType.ContainsKey( type ) == false )
		{
			Dictionary<string, MZPool<GameObject>> newPoolDict = new Dictionary<string, MZPool<GameObject>>();
			_charactersPoolsDictionaryByType.Add( type, newPoolDict );
		}

		Dictionary<string, MZPool<GameObject>> poolDict = _charactersPoolsDictionaryByType[ type ];

		MZDebug.Assert( poolDict.ContainsKey( name ) == false, "already have this, name=" + name );

		SetCreateObjectState( name, type );

		MZPool<GameObject> newPool = new MZPool<GameObject>();
		newPool.createNewObjectHandle = new MZPool<GameObject>.CreateNewObject( CreateNewGameObject );
		newPool.onGetValidItemHandle = new MZPool<UnityEngine.GameObject>.OnGetValidItem( OnCharacterObjectBecomeVaild );
		newPool.onReturnItemHandle = new MZPool<UnityEngine.GameObject>.OnReturnItem( OnCharacterObjectRemove );
		newPool.CreateContent( number );

		RestoreCreateObjectState();

		poolDict.Add( name, newPool );
	}

	public GameObject Get(MZCharacterType type, string name)
	{
		MZDebug.Assert( _charactersPoolsDictionaryByType != null && _charactersPoolsDictionaryByType.ContainsKey( type ) == true, "null not contain key(" + name + ")" );

		GameObject characterObject = _charactersPoolsDictionaryByType[ type ][ name ].GetValidItem();
		MZDebug.Assert( characterObject != null, "characterObject is null" );

		MZGameComponents.instance.charactersManager.Add( type, characterObject.GetComponent<MZCharacter>() );

		return characterObject;
	}

	public void Remove(MZCharacterType type, string name, GameObject characterObject)
	{
		MZDebug.Assert( _charactersPoolsDictionaryByType != null && _charactersPoolsDictionaryByType.ContainsKey( type ) == true, "null not contain key(" + name + ")" );
		_charactersPoolsDictionaryByType[ type ][ name ].Return( characterObject );
	}

	void SetCreateObjectState(string name, MZCharacterType type)
	{
		_onCreateObjectName = name;
		_onCreateCharacterType = type;
	}

	void RestoreCreateObjectState()
	{
		_onCreateObjectName = null;
		_onCreateCharacterType = MZCharacterType.Unknow;
	}

	#region handle function of MZPool<GameObject>

	GameObject CreateNewGameObject()
	{
		MZDebug.Assert( _onCreateObjectName != null && _onCreateCharacterType != MZCharacterType.Unknow, "must set create state first" );

		GameObject newObject = OT.CreateObject( _onCreateObjectName );
		newObject.active = false;
		newObject.transform.parent = _charactersParentTransformByType[ _onCreateCharacterType ];

		MZCharacter character = newObject.GetComponent<MZCharacter>();
		character.depth = MZGameSetting.GetDepthOfCharacter( _onCreateCharacterType );
		character.position = MZGameSetting.INVALID_POSITIONV2;
		character.name = _onCreateObjectName;

		return newObject;
	}

	void OnCharacterObjectBecomeVaild(GameObject characterObject)
	{
//		characterObject.active = true;
	}

	void OnCharacterObjectRemove(GameObject characterObject)
	{
		characterObject.active = false;
		characterObject.GetComponent<MZCharacter>().position = MZGameSetting.INVALID_POSITIONV2;
	}

	#endregion
}