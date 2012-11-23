/*
 * delete sprite : GameObject ... ok
 * dynamic add/remove OTSprite script ... ok, but frame sprite need more support
 * switch exchange ... zzzz
*/

using UnityEngine;
using System.Collections;

public class MZBaseObject : MonoBehaviour
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

	public float animationSpeed
	{
		set{ GetAnimatingSprite().speed = value; }
	}

	public void SetFrame(string frameName)
	{
//		GetFrameSprite().spriteContainer = // XXXXXXX ... need manager chan ><////
		GetFrameSprite().frameName = frameName;

	}

	public void PlayAnimation(string animationName)
	{
		GetAnimatingSprite().Play( animationName );
	}

	OTSprite _spriteCache;

	void Start()
	{

	}

	void Update()
	{

	}

	OTSprite GetSprite()
	{
		return GetFrameSprite();
	}

	OTSprite GetFrameSprite()
	{
		DisableAllSprite();

		if( gameObject.GetComponent<OTSprite>() == null )
		{
			OTSprite s = gameObject.AddComponent<OTSprite>();
			s.name = "Sprite";
		}

		_spriteCache = gameObject.GetComponent<OTSprite>();
		_spriteCache.enabled = true;

		return _spriteCache;
	}

	OTAnimatingSprite GetAnimatingSprite()
	{
		DisableAllSprite();

		if( gameObject.GetComponent<OTSprite>() == null )
		{
			OTAnimatingSprite s = gameObject.AddComponent<OTAnimatingSprite>();
			s.animation = MZOTAnimationsManager.GetInstance().otAnimation;
			s.name = "AnimatingSprite";
		}

		_spriteCache = gameObject.GetComponent<OTAnimatingSprite>();
		_spriteCache.enabled = true;

		return (OTAnimatingSprite)_spriteCache;
	}

	void DisableAllSprite()
	{
		foreach( OTSprite sprite in gameObject.GetComponents<OTSprite>() )
			sprite.enabled = false;
	}
}