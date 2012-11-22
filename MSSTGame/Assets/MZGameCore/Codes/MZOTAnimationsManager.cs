using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using MZUtility;
using MZGameCore;

public class MZOTAnimationsManager
{
	static MZOTAnimationsManager instance;
	GameObject cloneAniamtion;
	Dictionary<string,OTContainer> animationsDictionary;

	public OTAnimation otAnimation
	{
		get
		{
			if( cloneAniamtion == null )
			{
				Debug.Log( "XXXXXXXXXX" );
			}
			return (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) );
		}
	}

	static public MZOTAnimationsManager GetInstance()
	{
		if( instance == null )
			instance = new MZOTAnimationsManager();

		return instance;
	}

	public void AddContainter(string spritesheetPath)
	{
//		MZDebug.Log( "Add Container (spriteheetPath = " + spritesheetPath + ")" );
//
//		GameObject cloneSpritesheetsContainer = MZResources.InstantiateOrthelloContainer_SpriteAtlasCocos2D();
//		OTSpriteAtlasCocos2D spriteAtlasCocos2D = (OTSpriteAtlasCocos2D)cloneSpritesheetsContainer.GetComponent( typeof( OTSpriteAtlasCocos2D ) );
//
		TextAsset spritesheetTextAsset = (TextAsset)Resources.Load( spritesheetPath );
//
//		string fileName = System.IO.Path.GetFileName( spritesheetPath );
//		spriteAtlasCocos2D.name = fileName;
//		spriteAtlasCocos2D.atlasDataFile = spritesheetTextAsset;

		GameObject cloneSpritesheetsContainer = GameObject.Find( "[test]enemies_atlas" );

		if( cloneSpritesheetsContainer == null )
			Debug.Log( "Oh my GOD" );

		AddAnimations( cloneSpritesheetsContainer.GetComponent<OTContainer>(), spritesheetTextAsset );
	}

	private MZOTAnimationsManager()
	{

	}

	private void AddAnimations(OTContainer container, TextAsset spritesheetTextAsset)
	{
//		Dictionary<string,object> spritesheetDesc = NSDictionaryHelp.Create( spritesheetTextAsset.bytes );
//
//		Dictionary<string,object> framesDictionary = NSDictionaryHelp.GetDictionary( "frames", spritesheetDesc );
//
		List<OTAnimationFrameset> framesetsList = new List<OTAnimationFrameset>();
		List<string> frameNamesList = new List<string>();

		SetFramesets( ref framesetsList, ref frameNamesList, /*framesDictionary,*/ container );

		if( cloneAniamtion == null )
		{
			cloneAniamtion = MZResources.InstantiateOrthelloSprite( "Animation" );
			( (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) ) ).name = "AnimationsCollection";
		}

		// suck code here, when add new spritesheet, it will re-new again
		OTAnimation animation = (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) );
		List<OTAnimationFrameset> newFramesetsList = new List<OTAnimationFrameset>( animation.framesets );
		newFramesetsList.AddRange( framesetsList );
		animation.framesets = newFramesetsList.ToArray();
	}

	private string GetResourcesPath()
	{
		return "Assets/Resources/";
	}

	private string GetFrameNameWithoutIndex(string originFrameName)
	{
		string frameNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension( originFrameName );
		return frameNameWithoutExtension.Substring( 0, frameNameWithoutExtension.Length - 4 );
	}

	private OTAnimationFrameset CreateFrameset(string name, OTContainer container, int startFrameIndex, int endFrameIndex)
	{
		MZDebug.Log( "Add frameset: " + name );
			
		OTAnimationFrameset frameset = new OTAnimationFrameset();
		frameset.name = name;
		frameset.container = container;
		frameset.startFrame = startFrameIndex;
		frameset.endFrame = endFrameIndex;

		return frameset;
	}

	private void SetFramesets(ref List<OTAnimationFrameset> framesetsList, ref List<string> frameNamesList,
			/*Dictionary<string,object> framesDictionary,*/ OTContainer container)
	{
		if( framesetsList == null )
			framesetsList = new List<OTAnimationFrameset>();
		if( frameNamesList == null )
			frameNamesList = new List<string>();

		int startFrameIndex = 0;
		int endFrameIndex = -1;
		int index = 0;

		string preClearFrameName = null;

		OTAtlasData[] datas = container.GetComponent<OTSpriteAtlasCocos2D>().atlasData;
		foreach( OTAtlasData data in  datas )
//		foreach( string frameName in framesDictionary.Keys )
		{
			string frameName = data.name;

			string currentClearFrameName = GetFrameNameWithoutIndex( frameName );

			if( preClearFrameName == null )
			{
				preClearFrameName = currentClearFrameName;
			}

			if( currentClearFrameName != preClearFrameName || index == /*framesDictionary.Count - 1*/ datas.Length - 1 )
			{
				endFrameIndex = index - 1;

				OTAnimationFrameset frameset = CreateFrameset( preClearFrameName, container, startFrameIndex, endFrameIndex );

				framesetsList.Add( frameset );
				frameNamesList.Add( preClearFrameName );

				startFrameIndex = index + 1;
				preClearFrameName = currentClearFrameName;
			}

			index++;
		}
	}
}