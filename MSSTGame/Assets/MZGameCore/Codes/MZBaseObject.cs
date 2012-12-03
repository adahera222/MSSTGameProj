using UnityEngine;
using System.Collections;

public class MZBaseObject : MonoBehaviour
{
	public enum MZShaderType
	{
		Additive,
		AlphaBlended,
	}

	public bool isAnimatingObject
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
			anyPropertiesChanged = true;
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
			anyPropertiesChanged = true;
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
			if( this.isAnimatingObject )
			{
				GetAnimatingSprite().speed = value;
				anyPropertiesChanged = true;
			}
		}
	}

	public Color color
	{
		set
		{
			_color = value;
			anyPropertiesChanged = true;
		}
		get{ return _color; }
	}

	public new string name
	{
		set{ _name = value;	}
		get{ return _name; }
	}

	public MZShaderType shaderType
	{
		set
		{
			_shaderType = value;
			anyPropertiesChanged = true;
		}
		get{ return _shaderType; }
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

	protected virtual void Update()
	{
		if( anyPropertiesChanged == true )
		{
			GetSprite().GetComponent<MeshRenderer>().material.SetColor( "_TintColor", _color );
			GetSprite().GetComponent<MeshRenderer>().material.shader = Shader.Find( GetShaderPath( _shaderType ) );

			if( isAnimatingObject )
			{
				GetSprite().size = _scale*GetSprite().oSize;
			}

			anyPropertiesChanged = false;
		}

//		if( GetSprite().GetComponent<MeshRenderer>().material.shader.name != GetShaderPath( _shaderType ) )
//			GetSprite().GetComponent<MeshRenderer>().material.shader = Shader.Find( GetShaderPath( _shaderType ) );
	}

	bool anyPropertiesChanged = false;
	OTSprite _spriteCache = null;
	Vector2 _originSize = Vector2.zero;
	float _scale = 1;
	float _scaleX = 1;
	float _scaleY = 1;
	float _rotation = 1;
	Color _color = new Color( 0.5f, 0.5f, 0.5f );
	MZShaderType _shaderType = MZShaderType.AlphaBlended;
	string _name = "";

	void Start()
	{
		if( GetSprite() != null && GetSprite().name != name )
		{
			GetSprite().name = name;
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
		return ( this.isAnimatingObject )? (OTAnimatingSprite)_spriteCache : null;
	}

	string GetShaderPath(MZShaderType shaderType)
	{
		switch( shaderType )
		{
			case MZShaderType.AlphaBlended:
				return "Particles/Alpha Blended";

			case MZShaderType.Additive:
				return "Particles/Additive";

			default:
				MZDebug.Assert( false, "Unknow shade type" );
				return "";
		}
	}
}