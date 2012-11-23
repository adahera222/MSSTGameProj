using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using MZUtility;

public class MZOTAnimationsManager
{
	static MZOTAnimationsManager instance;
	GameObject cloneAniamtion;
	Dictionary<string,OTContainer> _spritesheetContainer;

	public OTAnimation otAnimation
	{
		get{ return (OTAnimation)cloneAniamtion.GetComponent( typeof( OTAnimation ) ); }
	}

	public Dictionary<string,OTContainer> spritesheetContainer
	{
		get{ return _spritesheetContainer; }
	}

	static public MZOTAnimationsManager GetInstance()
	{
		if( instance == null )
			instance = new MZOTAnimationsManager();

		return instance;
	}

	public void CreateAnimationsByExistedContainer()
	{
		foreach( OTSpriteAtlasCocos2D container in (OTSpriteAtlasCocos2D[])GameObject.FindObjectsOfType( typeof( OTSpriteAtlasCocos2D ) ) )
		{
			AddAnimations( container );
		}
	}

	private MZOTAnimationsManager()
	{

	}

	private void AddAnimations(OTContainer container)
	{
		if( _spritesheetContainer == null )
			_spritesheetContainer = new Dictionary<string, OTContainer>();

		spritesheetContainer.Add( container.name, container );

		List<OTAnimationFrameset> framesetsList = new List<OTAnimationFrameset>();
		List<string> frameNamesList = new List<string>();

		SetFramesets( ref framesetsList, ref frameNamesList, container );

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

	private void SetFramesets(ref List<OTAnimationFrameset> framesetsList, ref List<string> frameNamesList, OTContainer container)
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
		foreach( OTAtlasData data in datas )
		{
			string frameName = data.name;

			string currentClearFrameName = GetFrameNameWithoutIndex( frameName );

			if( preClearFrameName == null )
			{
				preClearFrameName = currentClearFrameName;
			}

			if( currentClearFrameName != preClearFrameName || index == datas.Length - 1 )
			{
				endFrameIndex = ( index == datas.Length - 1 )? index : index - 1;

				OTAnimationFrameset frameset = CreateFrameset( preClearFrameName, container, startFrameIndex, endFrameIndex );

				framesetsList.Add( frameset );
				frameNamesList.Add( preClearFrameName );

				startFrameIndex = index;
				preClearFrameName = currentClearFrameName;
			}

			index++;
		}
	}
}