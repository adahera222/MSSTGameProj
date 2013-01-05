using UnityEngine;
using System.Collections;

public class MZMath
{
	static public int RandomFromRange(int min, int max)
	{
		return Random.Range( min, max + 1 );
	}

	static public float DegreesToRadians(float degrees)
	{
		return UnityEngine.Mathf.Deg2Rad*degrees;
	}

	static public float RadiansToDegrees(float radians)
	{
		return UnityEngine.Mathf.Rad2Deg*radians;
	}

	static public float Dot(Vector2 p1, Vector2 p2)
	{
		return ( p1.x*p2.x ) + ( p1.y*p2.y );
	}

	static public float Distance(Vector2 p1, Vector2 p2)
	{
		return Vector2.Distance( p1, p2 );
	}

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

	static public Vector2 UnitVectorFromP1ToP2(Vector2 p1, Vector2 p2)
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

	static public Vector2 UnitVectorFromVectorAddDegree(Vector2 vector, float degrees)
	{
		float radians = DegreesToRadians( degrees );

		float c = Mathf.Cos( radians );
		float s = Mathf.Sin( radians );

		Vector2 resultVetor = new Vector2( vector.x*c - vector.y*s, vector.x*s + vector.y*c );
		Vector2 unitResultVetor = UnitVectorFromVector( resultVetor );

		return unitResultVetor;
	}

	static public Vector2 UnitVectorFromVectorAddDegree(float degrees)
	{
		return UnitVectorFromVectorAddDegree( new Vector2( 1, 0 ), degrees );
	}

	static public Vector2 UnitVectorFromDegrees(float degrees)
	{
		float degrees_ = ( (int)degrees )%360;

		if( degrees_ == 90 )
			return new Vector2( 0, 1 );
		if( degrees_ == 270 )
			return new Vector2( 0, -1 );
		if( degrees_ == 0 )
			return new Vector2( 1, 0 );
		if( degrees_ == 180 )
			return new Vector2( -1, 0 );

		float radians = MZMath.DegreesToRadians( degrees );
		return new Vector2( Mathf.Cos( radians ), Mathf.Sin( radians ) );
	}

	static public float DegreesFromV1ToV2(Vector2 v1, Vector2 v2)
	{
		float v1Dotv2 = Dot( v1, v2 );
		float v1lenMulv2len = LengthOfVector( v1 )*LengthOfVector( v2 );

		if( v1lenMulv2len == 0 )
			return 0;

		float result = Mathf.Acos( v1Dotv2/v1lenMulv2len );
		result = RadiansToDegrees( result );

		return result;
	}

	static public float DegreesFromXAxisToVector(Vector2 vector)
	{
		if( vector.x == 0 )
		{
			if( vector.y > 0 )
				return 90;
			if( vector.y < 0 )
				return 270;
		}

		if( vector.y == 0 )
		{
			if( vector.x > 0 )
				return 0;
			if( vector.x < 0 )
				return 180;
		}

		float result = DegreesFromV1ToV2( new Vector2( 1, 0 ), vector );
		return ( vector.y >= 0 )? result : 360 - result;
	}
}
