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

	static public float LengthOfVector(Vector2 vector)
	{
		return Mathf.Sqrt( vector.x*vector.x + vector.y*vector.y );
	}

	static public Vector2 unitVectorFromP1ToP2(Vector2 p1, Vector2 p2)
	{
		float diffY = p2.y - p1.y;
		float diffX = p2.x - p1.x;

		if( diffY == 0 )
		{
			if( diffX > 0 )
				return new Vector2( 1, 0 ) ;
			else if( diffX < 0 )
					return new Vector2( -1, 0 );
				else
					return Vector2.zero;
		}

		float length = Mathf.Sqrt( Mathf.Pow( diffX, 2 ) + Mathf.Pow( diffY, 2 ) );

		return new Vector2( diffX/length, diffY/length );
	}

	static public Vector2 UnitVectorFromVector(Vector2 vector)
	{
		float length = LengthOfVector( vector );
		return new Vector2( vector.x/length, vector.y/length );
	}
}
