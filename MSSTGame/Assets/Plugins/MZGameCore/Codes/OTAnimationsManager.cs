using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using MZUnitySupport;
using MZGameCore;

namespace MZGameCore
{
	public class OTAnimationsManager
	{
		public OTContainer originContainer;
		public GameObject originAnimation;
		static OTAnimationsManager instance;
		GameObject cloneAniamtion;
		Dictionary<string,OTContainer> animationsDictionary;

		public OTAnimation otAnimation
		{
			get{ return (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) ); }
		}

		static public OTAnimationsManager GetInstance()
		{
			if( instance == null )
				instance = new OTAnimationsManager();

			return instance;
		}

		public void AddContainter(string spritesheetPath)
		{
			MZDebug.Log( "Add Container (spriteheetPath = " + spritesheetPath + ")" );

			MZDebug.Assert( originContainer, "originContainer is null, bind it first" );

			OTContainer cloneSpritesheetsContainer = (OTContainer)GameObject.Instantiate( originContainer );
			OTSpriteAtlasCocos2D spriteAtlasCocos2D = (OTSpriteAtlasCocos2D)cloneSpritesheetsContainer.GetComponent( typeof( OTSpriteAtlasCocos2D ) );

			TextAsset spritesheetTextAsset = (TextAsset)Resources.Load( spritesheetPath );
			string fileName = System.IO.Path.GetFileName( spritesheetPath );
			spriteAtlasCocos2D.name = fileName;
			spriteAtlasCocos2D.atlasDataFile = spritesheetTextAsset;

			AddAnimations( cloneSpritesheetsContainer, spritesheetPath );
		}

		private OTAnimationsManager()
		{

		}

		private void AddAnimations(OTContainer container, string spriteheetPath)
		{
			Dictionary<string,object> spritesheetDesc = NSDictionaryHelp.Load( GetResourcesPath() + spriteheetPath + ".xml" );
			MZDebug.Assert( spritesheetDesc != null, "spritesheetDesc load fail from (" + spriteheetPath + ")" );

			Dictionary<string,object> framesDictionary = NSDictionaryHelp.GetDictionary( "frames", spritesheetDesc );

			List<OTAnimationFrameset> framesetsList = new List<OTAnimationFrameset>();
			List<string> frameNamesList = new List<string>();

			SetFramesets( ref framesetsList, ref frameNamesList, framesDictionary, container );

			if( cloneAniamtion == null )
			{
				MZDebug.Assert( originAnimation != null, "originAnimation is null, bind it first" );
				cloneAniamtion = (GameObject)GameObject.Instantiate( originAnimation );
				( (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) ) ).name = "AnimationsCollection";
			}

			// when new framesets coming in, it will re-add array ... suck !!!!
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
			Dictionary<string,object> framesDictionary, OTContainer container)
		{
			if( framesetsList == null )
				framesetsList = new List<OTAnimationFrameset>();
			if( frameNamesList == null )
				frameNamesList = new List<string>();

			int startFrameIndex = 0;
			int endFrameIndex = -1;
			int index = 0;

			string preClearFrameName = null;

			foreach( string frameName in framesDictionary.Keys )
			{
				string currentClearFrameName = GetFrameNameWithoutIndex( frameName );

				if( preClearFrameName == null )
				{
					preClearFrameName = currentClearFrameName;
				}

				if( currentClearFrameName != preClearFrameName || index == framesDictionary.Count - 1 )
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
}