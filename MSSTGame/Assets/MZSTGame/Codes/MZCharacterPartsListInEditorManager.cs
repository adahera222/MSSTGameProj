using UnityEngine;
using System.Collections;

public class MZCharacterPartsListInEditorManager : MZSingleton<MZCharacterPartsListInEditorManager>
{
	GameObject _characterPartsListObject =null;

	public GameObject CreateListByOTContainer(string otContainerName, string itemName, string frameName, MZCharacterType characterType)
	{
		OTSpriteAtlasCocos2D otContainer = MZOTFramesManager.instance.GetFrameContainterByName( otContainerName );
		MZDebug.Assert( otContainer != null, "otContainer is null, name = " + otContainerName );

		GameObject partItem = MZBaseObjectsFactory.instance.Create( characterType );

		MZCharacterPart characterPart = partItem.GetComponent<MZCharacterPart>();
		characterPart.SetFrame( frameName );

		OTSprite otSprite = partItem.GetComponent<OTSprite>();
		otSprite.name = itemName;
//		otSprite.frameName = frameName;
//		otSprite.spriteContainer = otContainer;
		otSprite.dirtyChecks = true;
		otSprite.StartUp();

		partItem.transform.parent = GetParent( characterType );

		return partItem;
	}

	Transform GetParent(MZCharacterType characterType)
	{
		if( _characterPartsListObject == null )
			_characterPartsListObject = GameObject.Find( "MZCharacterPartsList" );

		MZDebug.Assert( _characterPartsListObject != null, "_characterPartsListObject is null" );

		switch( characterType )
		{
			case MZCharacterType.EnemyAir:
				return _characterPartsListObject.transform.FindChild( "Enemies" );

			case MZCharacterType.EnemyBullet:
				return _characterPartsListObject.transform.FindChild( "EnemyBullets" );

			default:
				MZDebug.Assert( false, "not support type = " + characterType.ToString() );
				return null;
		}
	}
}
