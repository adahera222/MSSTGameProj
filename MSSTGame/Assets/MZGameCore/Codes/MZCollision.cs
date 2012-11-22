using UnityEngine;
using System.Collections;

[System.Serializable]
public class MZCollision
{
	public Vector2 center = new Vector2( 0, 0 );
	public float radius = 0;

	public MZCollision()
	{

	}

	public MZCollision(Vector2 center, float radius)
	{
		this.center = center;
		this.radius = radius;
	}
}