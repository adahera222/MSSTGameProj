using UnityEngine;
using System.Collections;

public class MZResources
{
	static string ORTHELLO_OBJECTS_PATH = "OrthelloPrefabs/Objects/";
	static string MZGAMECORE_PREFABS_PATH = "MZPrefabs/";

	static public GameObject InstantiateOrthelloSprite(string spriteName)
	{
		GameObject clone = (GameObject)GameObject.Instantiate( GetOrthelloPrefabSprite( spriteName ) );
		return clone;
	}

	static public GameObject GetOrthelloPrefabSprite(string spriteName)
	{
		GameObject prefab = (GameObject)Resources.Load( ORTHELLO_OBJECTS_PATH + "Sprites/" + spriteName, typeof( GameObject ) );
		MZDebug.Assert( prefab != null, "Invaild sprite name(" + spriteName + ")" );
		return prefab;
	}

	static public GameObject InstantiateOrthelloContainer_SpriteAtlasCocos2D()
	{
		MZDebug.Assert( false, "I am suck ... do't use me now" );

		GameObject prefab = (GameObject)Resources.Load( ORTHELLO_OBJECTS_PATH + "Sprites/SpriteAtlas/SpriteAtlas-Cocos2D", typeof( GameObject ) );
		MZDebug.Assert( prefab != null, "prefabContainer isn null" );
		GameObject clone = (GameObject)GameObject.Instantiate( prefab );

		return clone;
	}

	static public GameObject InstantiateMZGamePrefab(string name)
	{
		GameObject prefab = (GameObject)Resources.Load( MZGAMECORE_PREFABS_PATH + name, typeof( GameObject ) );
		MZDebug.Assert( prefab != null, "prefab is null" );
		return (GameObject)GameObject.Instantiate( prefab );
	}

	private MZResources()
	{

	}
}