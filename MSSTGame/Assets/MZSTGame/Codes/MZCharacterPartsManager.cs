using UnityEngine;
using System.Collections;

public class MZCharacterPartsManager
{
	static MZCharacterPartsManager _instances = null;

	static public MZCharacterPartsManager GetInstance()
	{
		if( _instances == null )
			_instances = new MZCharacterPartsManager();

		return _instances;
	}

	public void CreatePartsByOTContainer(string otContainerName, MZCharacterType characterType)
	{
//		OTSpriteAtlasCocos2D container = MZOTFramesManager.GetInstance().GetFrameContainterByName( otContainerName );
//		GameObject part = MZResources.InstantiateOrthelloSprite( "Sprite" );
//		part
	}

	private MZCharacterPartsManager()
	{
	}

//	MZCharacter GetCharacterSprict(MZCharacterType characterType)
//	{}
//
//	Transform GetTransformParentInHierarchy(MZCharacterType characterType)
//	{
//
//	}
	
}
