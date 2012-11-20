using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MZGameCore;
using MZUnitySupport;

public class OTCharacterBase : MonoBehaviour
{
	public int a
	{
		set{ a = value; }
		get{ return a; }
	}

	public MZGameCore.Collision[] collisions;
	OTSprite otSprite;

	void Start()
	{
		otSprite = (OTSprite)gameObject.GetComponent( typeof( OTSprite ) );
		MZDebug.Assert( otSprite != null, "otSprite is null" );
	}

	void Update()
	{

	}

	void OnDrawGizmos()
	{
		if( collisions == null )
			return;

		Color preColor = Gizmos.color = Color.red;
		Gizmos.color = Color.red;
		Vector2 pos = otSprite.position;
		int depth = otSprite.depth;

		foreach( MZGameCore.Collision c in collisions )
		{
			Gizmos.DrawWireSphere( new Vector3( pos.x + c.center.x, pos.y + c.center.y, depth ), c.radius );
		}

		Gizmos.color = preColor;
	}
}