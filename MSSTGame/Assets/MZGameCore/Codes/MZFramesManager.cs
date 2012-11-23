//
// not finish
// frame from spritesheet
// frame from single texture

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFramesManager
{
	static MZFramesManager _instance;

	static MZFramesManager GetInstance()
	{
		if( _instance == null )
			_instance = new MZFramesManager();

		return _instance;
	}

	public int AddFrame(string frameName, OTContainer spritesheetsContainer)
	{
//		if( spritesheetsContainerByFrameName == null )
//			spritesheetsContainerByFrameName = new Dictionary<string, OTContainer>();
//
//		spritesheetsContainerByFrameName.Add( frameName, spritesheetsContainer );

		return 0;
	}

//	public OTAtlasData GetFrameAtlasData(string frameName)
//	{
//		OTContainer spritesheetContainer = spritesheetsContainerByFrameName[ frameName ];
//	}

	Dictionary<string, OTSpriteAtlasCocos2D> spritesheetsContainerByFrameName;
	private MZFramesManager()
	{

	}
}
