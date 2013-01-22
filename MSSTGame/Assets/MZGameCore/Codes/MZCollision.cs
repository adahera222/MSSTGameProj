using UnityEngine;
using System.Collections;

public interface IMZCollision
{
	Vector2 realPosition
	{
		get;
	}
}

[System.Serializable]
public class MZCollision
{
	public Vector2 center = new Vector2( 0, 0 );
	public float radius = 0;
	public IMZCollision collisionDelegate = null;
	
	public MZCollision()
	{

	}

	public void Set(Vector2 center, float radius)
	{
		this.center = center;
		this.radius = radius;
	}

	public bool IsCollision(MZCollision otherCollision)
	{
		if( otherCollision.collisionDelegate == null )
			return false;

		MZDebug.Assert( collisionDelegate != null, "collisionDelegate is null" );

		float collisionDistance = radius + otherCollision.radius;
		
		return MZMath.Distance( collisionDelegate.realPosition + center, otherCollision.collisionDelegate.realPosition + otherCollision.center ) <= collisionDistance;
	}
}