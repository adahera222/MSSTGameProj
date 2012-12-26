using UnityEngine;
using System.Collections;

public class MZBaseObject : MonoBehaviour
{
	public enum MZShaderType
	{
		OTDefault,
		Additive,
		AlphaBlended,
	}

	//
//	Vector2 _originSize = Vector2.zero;
	float _scale = 1;
	float _scaleX = 1;
	float _scaleY = 1;
	float _rotation = 1;
	float _alpha = 1;
	Color _color = new Color( 0.5f, 0.5f, 0.5f );
	MZShaderType _shaderType = MZShaderType.OTDefault;
	string _name = "";
	//

	public Vector2 position
	{
		set{ GetSprite().position = value; }
		get{ return GetSprite().position; }
	}

	public float scale
	{
		set
		{
			_scale = value;
			_scaleX = _scale;
			_scaleY = _scale;
			GetSprite().size = GetSprite().oSize*_scale;
		}
		get
		{
			MZDebug.Assert( _scaleX == _scaleY, "_scaleX != _scaleY" );
			return _scale;
		}
	}

	public float scaleX
	{
		set
		{
			_scaleX = value;
			GetSprite().size = new Vector2( GetSprite().oSize.x*_scaleX, GetSprite().size.y );
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
			GetSprite().size = new Vector2( GetSprite().size.x, GetSprite().oSize.y*_scaleY );
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

	public Color color
	{
		set
		{
			_color = value;
			GetSprite().tintColor = _color;
		}
		get{ return _color; }
	}

	public float alpha
	{
		set
		{
			_alpha = value;
			GetSprite().alpha = _alpha;
		}
		get{ return _alpha; }

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
			MZDebug.Assert( false, "Not support now" );
			_shaderType = value;
		}
		get{ return _shaderType; }
	}

	public void SetFrame(string frameName)
	{
		GetSprite().spriteContainer = MZOTFramesManager.instance.GetFrameContainterByFrameName( frameName );
		GetSprite().frameName = frameName;
		GetSprite().size = MZOTFramesManager.GetInstance().GetAtlasData( frameName ).size;
	}

	public float GetMaxEdge()
	{
		float w = GetSprite().size.x;
		float h = GetSprite().size.y;

		return ( w > h )? w : h;
	}

	public virtual void Enable()
	{
		enabled = true;
	}

	public virtual void Disable()
	{
		enabled = false;

//		_originSize = Vector2.zero;
//		_scale = 1;
//		_scaleX = 1;
//		_scaleY = 1;
//		_rotation = 1;
//		_color = new Color( 0.5f, 0.5f, 0.5f );
//		_shaderType = MZShaderType.OTDefault;
//		_name = "";
	}

	protected virtual void Update()
	{

	}

	OTSprite GetSprite()
	{
		return gameObject.GetComponent<OTSprite>();
	}

	string GetShaderPath(MZShaderType shaderType)
	{
		switch( shaderType )
		{
			case MZShaderType.OTDefault:
				return "Mobile/Particles/Alpha Blended";

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