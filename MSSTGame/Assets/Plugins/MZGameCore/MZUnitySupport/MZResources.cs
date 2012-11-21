using UnityEngine;
using System.Collections;

namespace MZUnitySupport
{
	public class MZResources
	{
		static string ORTHELLO_OBJECTS_PATH = "Assets/Plugins/Libs/Orthello/Objects/";

		static public GameObject InstantiateOrthelloSprite(string spriteName)
		{
			GameObject clone = (GameObject)GameObject.Instantiate( GetOrthelloPrefabSprite( spriteName ) );
			return clone;
		}

		static public GameObject GetOrthelloPrefabSprite(string spriteName)
		{
			GameObject prefab = (GameObject)Resources.LoadAssetAtPath( ORTHELLO_OBJECTS_PATH + "Sprites/" + spriteName + ".prefab", typeof( GameObject ) );
			MZDebug.Assert( prefab != null, "Invaild sprite name(" + spriteName + ")" );
			return prefab;
		}

		static public OTContainer InstantiateOrthelloContainer_SpriteAtlasCocos2D()
		{
			OTContainer prefab = (OTContainer)Resources.LoadAssetAtPath( ORTHELLO_OBJECTS_PATH + "Sprites/SpriteAtlas/SpriteAtlas-Cocos2D.prefab",
				typeof( OTContainer ) );
			MZDebug.Assert( prefab != null, "prefabContainer isn null" );
			OTContainer clone = (OTContainer)GameObject.Instantiate( prefab );
			
			return clone;
		}

		private MZResources()
		{

		}
	}
}