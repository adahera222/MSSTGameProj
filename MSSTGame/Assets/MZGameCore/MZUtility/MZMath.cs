using UnityEngine;
using System.Collections;

public class MZMath
{
	static public float DistancePow2(Vector2 p1, Vector2 p2)
	{
		return ( p2.x - p1.x )*( p2.x - p1.x ) + ( p2.y - p1.y )*( p2.y - p1.y );
	}

	static public float V3ToV2DistancePow2(Vector3 p1, Vector3 p2)
	{
		return ( p2.x - p1.x )*( p2.x - p1.x ) + ( p2.y - p1.y )*( p2.y - p1.y );
	}
}
