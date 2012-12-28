using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZOTFramesManager : MZSingleton<MZOTFramesManager>
{
	Dictionary<string, OTSpriteAtlasCocos2D> _spritesheetsContainerByFrameName;
	Dictionary<string, OTSpriteAtlasCocos2D> _spritesheetsContainerByName;

	public void CreateFramesByExistedContainer()
	{
		if( _spritesheetsContainerByName == null )
			_spritesheetsContainerByName = new Dictionary<string, OTSpriteAtlasCocos2D>();

		OTSpriteAtlasCocos2D[] containers = (OTSpriteAtlasCocos2D[])GameObject.FindObjectsOfType( typeof( OTSpriteAtlasCocos2D ) );
		foreach( OTSpriteAtlasCocos2D container in containers )
		{
			if( container.texture == null )
			{
				MZDebug.Log( "container(" + container.name + ").texture is null, continue to next" );
				continue;
			}

			_spritesheetsContainerByName.Add( container.name, container );

			OTAtlasData[] datas = container.GetComponent<OTSpriteAtlasCocos2D>().atlasData;
			foreach( OTAtlasData data in datas )
			{
				AddSpriteAtlasCocos2DFrame( data.name, container );
			}
		}
	}

	public int AddSpriteAtlasCocos2DFrame(string frameName, OTSpriteAtlasCocos2D spritesheetsContainer)
	{
		if( _spritesheetsContainerByFrameName == null )
			_spritesheetsContainerByFrameName = new Dictionary<string, OTSpriteAtlasCocos2D>();

		MZDebug.Assert( _spritesheetsContainerByFrameName.ContainsKey( frameName ) == false, "Duplicated frame name=" + frameName + "(on add " + spritesheetsContainer.name +")");
		_spritesheetsContainerByFrameName.Add( frameName, spritesheetsContainer );

		return _spritesheetsContainerByFrameName.Count;
	}

	public OTSpriteAtlasCocos2D GetFrameContainterByFrameName(string frameName)
	{
		MZDebug.Assert( _spritesheetsContainerByFrameName.ContainsKey( frameName ), "spritesheetContainer by frame name( " + frameName + " ) is null" );

		OTSpriteAtlasCocos2D spritesheetContainer = _spritesheetsContainerByFrameName[ frameName ];
		return spritesheetContainer;
	}

	public OTSpriteAtlasCocos2D GetFrameContainterByName(string containerName)
	{
		MZDebug.Assert( _spritesheetsContainerByName.ContainsKey( containerName ), "spritesheetContainer by name( " + containerName + " ) is null" );

		OTSpriteAtlasCocos2D spritesheetContainer = _spritesheetsContainerByName[ containerName ];
		return spritesheetContainer;
	}

	public OTAtlasData GetAtlasData(string frameName)
	{
		OTSpriteAtlasCocos2D spritesheetContainer = GetFrameContainterByFrameName( frameName );

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
}
