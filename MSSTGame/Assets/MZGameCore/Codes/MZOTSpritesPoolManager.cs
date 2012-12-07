using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZOTSpritesPoolManager
{
	static MZOTSpritesPoolManager _instance;
	Dictionary<MZCharacterType, MZOTSpritesPool> _spritesPoolDictionaryByType;

	static public MZOTSpritesPoolManager GetInstance()
	{
		if( _instance == null )
			_instance = new MZOTSpritesPoolManager();
		return _instance;
	}

	public void AddPool(string containerName, MZCharacterType charcterType, int number, int depth)
	{
		if( _spritesPoolDictionaryByType == null )
			_spritesPoolDictionaryByType = new Dictionary<MZCharacterType, MZOTSpritesPool>();

		OTContainer container = MZOTFramesManager.GetInstance().GetFrameContainterByName( containerName );

		MZDebug.Assert( _spritesPoolDictionaryByType.ContainsKey( charcterType ) == false, "SORRY, can't use two texture to one type now" ); // one texture one pool now // temp

		MZOTSpritesPool spritesPool = new MZOTSpritesPool( container, number, depth );
		_spritesPoolDictionaryByType.Add( charcterType, spritesPool );
	}

	public GameObject GetSpriteObject(MZCharacterType charcterType)
	{
		MZDebug.Assert( _spritesPoolDictionaryByType.ContainsKey( charcterType ) != false, "no pool for type=" + charcterType.ToString() );
		return _spritesPoolDictionaryByType[ charcterType ].GetSpriteObject();
	}

	public void ReturnSpriteObject(GameObject spriteObject, MZCharacterType charcterType)
	{
		MZDebug.Assert( _spritesPoolDictionaryByType.ContainsKey( charcterType ) != false, "no pool for type=" + charcterType.ToString() );
		_spritesPoolDictionaryByType[ charcterType ].ReturnSpriteObject( spriteObject );
	}

	private MZOTSpritesPoolManager()
	{
	}

	public class MZOTSpritesPool
	{
		Vector2 invalidPosition = new Vector2( -9999, -9999 );
		List<GameObject> _spritesList = null;
		OTContainer _container = null;
		int _maxUsingIndex = 0;
		int _depth = 0;

		public MZOTSpritesPool(OTContainer container, int number, int depth)
		{
			MZDebug.Assert( container != null, "container is null" );

			if( number < 0 )
				number = 0;

			_container = container;
			_depth = depth;

			_spritesList = new List<GameObject>();

			for( int i = 0; i < number; i++ )
			{
				GameObject spriteObject = MZResources.InstantiateOrthelloSprite( "Sprite" );

				spriteObject.active = false;
				spriteObject.transform.parent = GetSpriteDisableTransform();

				spriteObject.GetComponent<OTSprite>().depth = _depth;
				spriteObject.GetComponent<OTSprite>().position = invalidPosition;
				spriteObject.GetComponent<OTSprite>().spriteContainer = _container;

				_spritesList.Add( spriteObject );
			}
		}

		public GameObject GetSpriteObject()
		{
			GameObject spriteObject = null;

			for( int i = 0; i < _spritesList.Count; i++ )
			{
				if( _spritesList[ i ].active == false )
				{
					spriteObject = _spritesList[ i ];

					if( i > _maxUsingIndex )
						_maxUsingIndex = i;

					break;
				}
			}

			MZDebug.Assert( spriteObject != null, "can not get valid sprite in pool, max use=" + _maxUsingIndex.ToString() );

			spriteObject.active = true;
			return spriteObject;
		}

		public void ReturnSpriteObject(GameObject spriteObject)
		{
			spriteObject.active = false;
			spriteObject.transform.parent = GetSpriteDisableTransform();
		}

		Transform GetSpriteDisableTransform()
		{
			return MZGameComponents.GetInstance().spritesPool.transform;
		}
	}
}
