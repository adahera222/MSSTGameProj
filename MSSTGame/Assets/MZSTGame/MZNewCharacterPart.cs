using UnityEngine;
using System.Collections;

public class MZNewCharacterPart : MonoBehaviour
{
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

	public new string name
	{
		set{ _name = value;	}
		get{ return _name; }
	}

	public void SetFrame(string frameName)
	{
		GetSprite().spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainter( frameName );
		GetSprite().frameName = frameName;
		GetSprite().size = MZOTFramesManager.GetInstance().GetAtlasData( frameName ).size;
		_originSize = GetSprite().size;
	}

//	protected virtual void Update()
//	{
//		if( anyPropertiesChanged == true )
//		{
//			GetSprite().GetComponent<MeshRenderer>().material.SetColor( "_TintColor", _color );
//			GetSprite().GetComponent<MeshRenderer>().material.shader = Shader.Find( GetShaderPath( _shaderType ) );
//
//			if( isAnimatingObject )
//			{
//				GetSprite().size = _scale*GetSprite().oSize;
//			}
//
//			anyPropertiesChanged = false;
//		}
//
////		if( GetSprite().GetComponent<MeshRenderer>().material.shader.name != GetShaderPath( _shaderType ) )
////			GetSprite().GetComponent<MeshRenderer>().material.shader = Shader.Find( GetShaderPath( _shaderType ) );
//	}

//	bool anyPropertiesChanged = false;
	OTSprite _spriteCache = null;
	Vector2 _originSize = Vector2.zero;
	float _scale = 1;
	float _scaleX = 1;
	float _scaleY = 1;
	float _rotation = 1;
	string _name = "";

	void Start()
	{
		InitNormalSpriteCache();

		if( GetSprite() != null && GetSprite().name != name )
		{
			GetSprite().name = name;
		}
	}

	void InitNormalSpriteCache()
	{
		_spriteCache = gameObject.GetComponent<OTSprite>();
		_spriteCache.name = "Sprite";
	}

	OTSprite GetSprite()
	{
		MZDebug.Assert( _spriteCache != null, "must set frame or aniamtion first" );
		return _spriteCache;
	}
}