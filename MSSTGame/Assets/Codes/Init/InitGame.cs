using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MZUnitySupport;
using MZGameCore;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public OTContainer originSpritesheetsContainer;
	public GameObject originAnimation;
	public OTAnimatingSprite testAnimatingSprite;
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		OTAnimationsManager.GetInstance().originContainer = originSpritesheetsContainer;
		OTAnimationsManager.GetInstance().originAnimation = originAnimation;
		OTAnimationsManager.GetInstance().AddContainter( "Spritesheets/[test]enemies_atlas" );
		OTAnimationsManager.GetInstance().AddContainter( "Spritesheets/[test]atlas2" );
//		OTAnimationsManager.GetInstance().AddContainter( "Spritesheets/[test]large" );

//		testAnimatingSprite.animation = OTAnimationsManager.GetInstance().otAnimation;
//		testAnimatingSprite.looping = true;
//		testAnimatingSprite.Play( "Donut_normal" );
//		testAnimatingSprite.speed = 0.01f;

		GameObject obj = GameObject.Find( "MZOTAnimatingSprite" );
		OTAnimatingSprite ota = (OTAnimatingSprite)obj.GetComponent( typeof( OTAnimatingSprite ) );
		ota.animation = OTAnimationsManager.GetInstance().otAnimation;
		ota.looping = true;
		ota.Play( "Donut_normal" );
	}

	void Update()
	{
	
	}

	void InitSpritesheets()
	{
		// create containter
		if( MZDebug.Alert( originSpritesheetsContainer == null, "originSpritesheetsContainer not bind" ) )
			return;

		OTContainer cloneSpritesheetsContainer = (OTContainer)Instantiate( originSpritesheetsContainer );
		OTSpriteAtlasCocos2D spriteAtlasCocos2D = (OTSpriteAtlasCocos2D)cloneSpritesheetsContainer.GetComponent( typeof( OTSpriteAtlasCocos2D ) );

		if( MZDebug.Alert( spriteAtlasCocos2D == null, "spriteAtlasCocos2D is null" ) )
			return;

		TextAsset spritesheetTextAsset = (TextAsset)Resources.Load( "Spritesheets/[test]enemies_atlas" );
		MZDebug.Assert( spritesheetTextAsset, "spritesheetTextAsset(" + "[test]enemies_atlas" + ") is null" );

		spriteAtlasCocos2D.name = "[test]enemies_atlas";
		spriteAtlasCocos2D.atlasDataFile = spritesheetTextAsset;

		// create animations
		if( MZDebug.Alert( originAnimation == null, "originAnimation not bind" ) )
			return;

		OTAnimationFrameset frameSet = new OTAnimationFrameset();
		frameSet.container = (OTContainer)cloneSpritesheetsContainer;
		frameSet.startFrame = 0;
		frameSet.endFrame = 4;

		OTAnimationFrameset[] framesSet = new OTAnimationFrameset[1];
		framesSet[ 0 ] = frameSet;

		GameObject cloneAniamtion = (GameObject)Instantiate( originAnimation );
		OTAnimation animation = (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) );
		animation.name = "Animation[test]enemies_atlas";
		animation.framesets = framesSet;

		MZDebug.Alert( testAnimatingSprite == null, "why null???" );

//		testAnimatingSprite.spriteContainer = cloneSpritesheetsContainer;
		testAnimatingSprite.animation = animation;
		testAnimatingSprite.speed = 3;
	}
}
