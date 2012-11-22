/*
 * Collision
 * */

using UnityEngine;
using System.Collections;
using MZGameCore;
using MZUnitySupport;

public class MZBaseObject : MonoBehaviour
{
	public Vector2 position
	{
		set{ GetOTAnimatingSprite().position = value; }
		get{ return GetOTAnimatingSprite().position; }
	}

	public int depth
	{
		set{ GetOTAnimatingSprite().depth = value; }
		get{ return GetOTAnimatingSprite().depth; }
	}

	public float AnimationSpeed
	{
		set{ GetOTAnimatingSprite().speed = value; }
	}

	public void PlayAnimation(string animationName)
	{
		GetOTAnimatingSprite().Play( animationName );
	}

	GameObject spriteCache;
	OTAnimatingSprite otAnimatingSpriteCache;

	void Start()
	{

	}

	void Update()
	{

	}

	OTAnimatingSprite GetOTAnimatingSprite()
	{
		if( otAnimatingSpriteCache == null )
			otAnimatingSpriteCache = GetSprite().GetComponent<OTAnimatingSprite>();

		MZDebug.Assert( otAnimatingSpriteCache != null, "otAnimatingSpriteCache is null" );

		return otAnimatingSpriteCache;
	}

	GameObject GetSprite()
	{
		if( spriteCache == null )
		{
			spriteCache = MZResources.InstantiateOrthelloSprite( "AnimatingSprite" );
			spriteCache.transform.parent = gameObject.transform;
			spriteCache.GetComponent<OTAnimatingSprite>().name = "Sprite2D";
			spriteCache.GetComponent<OTAnimatingSprite>().animation = MZOTAnimationsManager.GetInstance().otAnimation;
		}

		MZDebug.Assert( spriteCache != null , "spriteCache is null" );

		return spriteCache;
	}
}
