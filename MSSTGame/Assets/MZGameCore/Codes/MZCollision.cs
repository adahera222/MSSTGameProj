using UnityEngine;
using System.Collections;

[System.Serializable]
public class MZCollision
{
	public Vector2 center = new Vector2( 0, 0 );
	public float radius = 0;

	static public bool IsCollision(MZCollision c1, MZCollision c2)
	{
		return ( MZMath.DistancePow2( c1.center, c2.center ) <= Mathf.Pow( c1.radius + c2.radius, 2 ) );
	}

	public MZCollision()
	{

	}

	public MZCollision(Vector2 center, float radius)
	{
		this.center = center;
		this.radius = radius;
	}

	public bool IsCollision(MZCollision other)
	{
		return ( MZMath.DistancePow2( center, other.center ) <= Mathf.Pow( radius + other.radius, 2 ) );
	}
}