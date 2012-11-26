/*
 * delete sprite : GameObject ... ok
 * dynamic add/remove OTSprite script ... ok, but frame sprite need more support
 * switch exchange ... zzzz
*/
using UnityEngine;
using System.Collections;

public class MZBaseObject : MonoBehaviour
{
	public bool IsAnimatingObject
	{
		get{ return ( _spriteCache.GetType() == typeof( OTAnimatingSprite ) ); }
	}

	public Vector2 position
	{
		set{ GetSprite().position = value; }
		get{ return GetSprite().position; }
	}

	public int depth
	{
		set{ GetSprite().depth = value; }
		get{ return GetSprite().depth; }
	}

	public float scale
	{
		set
		{
			_scale = value;
			GetSprite().size = _originSize*_scale;
		}
		get
		{
			MZDebug.Assert( _scaleX != _scaleY, "_scaleX != _scaleY" );
			return _scale;
		}
	}

	public float scaleX
	{
		set
		{
			_scaleX = value;
			GetSprite().size = new Vector2( _originSize.x*_scaleX, GetSprite().size.y );
		}
		get
		{
			return _scaleX;
		}
	}

	public float scaleY
	{
		set
		{
			_scaleY = value;
			GetSprite().size = new Vector2( GetSprite().size.x, _originSize.y*_scaleY );
		}
		get
		{
			return _scaleY;
		}
	}

	public float rotation
	{
		set
		{
			_rotation = value;
			GetSprite().rotation = _rotation;
		}
		get
		{
			return _rotation;
		}
	}

	public float animationSpeed
	{
		set
		{
			if( this.IsAnimatingObject )
				GetAnimatingSprite().speed = value;
		}
	}

	public void SetFrame(string frameName)
	{
		InitNormalSpriteCache();
		GetSprite().spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainter( frameName );
		GetSprite().frameName = frameName;
		GetSprite().size = MZOTFramesManager.GetInstance().GetAtlasData( frameName ).size;
		_originSize = GetSprite().size;
	}

	public void PlayAnimation(string animationName)
	{
		InitAnimatingSpriteCache();
		GetAnimatingSprite().Play( animationName );
		_originSize = new Vector2( 316, 240 );
	}

	OTSprite _spriteCache;
	Vector2 _originSize;
	float _scale;
	float _scaleX;
	float _scaleY;
	float _rotation;

	void Start()
	{

	}

	void Update()
	{
		// sprite control ... maybe to functional programming ... ><"
		if( IsAnimatingObject )
		{
			GetSprite().size = _scale*GetSprite().oSize;
		}
	}

	void InitNormalSpriteCache()
	{
		_spriteCache = gameObject.AddComponent<OTSprite>();
		_spriteCache.name = "Sprite";
	}

	void InitAnimatingSpriteCache()
	{
		_spriteCache = gameObject.AddComponent<OTAnimatingSprite>();
		_spriteCache.name = "AnimatingSprite";
		( (OTAnimatingSprite)_spriteCache ).animation = MZOTAnimationsManager.GetInstance().otAnimation;
	}

	OTSprite GetSprite()
	{
		MZDebug.Assert( _spriteCache != null, "must set frame or aniamtion first" );
		return _spriteCache;
	}

	OTAnimatingSprite GetAnimatingSprite()
	{
		MZDebug.Assert( _spriteCache != null, "must set frame or aniamtion first" );
		return ( this.IsAnimatingObject )? (OTAnimatingSprite)_spriteCache : null;
	}
}