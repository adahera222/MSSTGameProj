using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZOTFramesManager
{
	static MZOTFramesManager _instance;

	static public MZOTFramesManager GetInstance()
	{
		if( _instance == null )
			_instance = new MZOTFramesManager();

		return _instance;
	}

	public void CreateFramesByExistedContainer()
	{
		foreach( OTSpriteAtlasCocos2D container in (OTSpriteAtlasCocos2D[])GameObject.FindObjectsOfType( typeof( OTSpriteAtlasCocos2D ) ) )
		{
			OTAtlasData[] datas = container.GetComponent<OTSpriteAtlasCocos2D>().atlasData;
			foreach( OTAtlasData data in datas )
			{
				AddSpriteAtlasCocos2DFrame( data.name, container );
			}
		}
	}

	public int AddSpriteAtlasCocos2DFrame(string frameName, OTSpriteAtlasCocos2D spritesheetsContainer)
	{
		if( spritesheetsContainerByFrameName == null )
			spritesheetsContainerByFrameName = new Dictionary<string, OTSpriteAtlasCocos2D>();

		spritesheetsContainerByFrameName.Add( frameName, spritesheetsContainer );

		return spritesheetsContainerByFrameName.Count;
	}

	public OTSpriteAtlasCocos2D GetFrameContainter(string frameName)
	{
		MZDebug.Assert( spritesheetsContainerByFrameName.ContainsKey( frameName ), "spritesheetContainer by name( " + frameName + " ) is null" );

		OTSpriteAtlasCocos2D spritesheetContainer = spritesheetsContainerByFrameName[ frameName ];
		return spritesheetContainer;
	}

	public OTAtlasData GetAtlasData(string frameName)
	{
		OTSpriteAtlasCocos2D spritesheetContainer = GetFrameContainter( frameName );

		int frameIndex = -1;

		for( int i = 0; i < spritesheetContainer.atlasData.Length; i++ )
		{
			if( frameName == spritesheetContainer.atlasData[ i ].name )
			{
				frameIndex = i;
				break;
			}
		}

		OTAtlasData data = spritesheetContainer.atlasData[ frameIndex ];
		return data;
	}

	Dictionary<string, OTSpriteAtlasCocos2D> spritesheetsContainerByFrameName;

	private MZOTFramesManager()
	{

	}
}
