using UnityEngine;
using System.Collections;

public class MZAttack_OddWay : MZAttack
{
	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		Vector2 centerMovingVector = targetHelp.GetResultMovingVector();

		for( int i = 0; i < currentWays; i++ )
		{
			GameObject bullet = GetNewBulletObject();
			AddLinearMoveToBullet( bullet );

			if( i == 0 )
			{
				SetCenterWayToBullet( bullet, centerMovingVector );
			}
			else
			{
				SetSideWayToBullet( bullet, centerMovingVector, i );
			}

			EnableBullet( bullet.GetComponent<MZBullet>() );
		}

		UpdateAdditionalVelocity();
	}

	void SetCenterWayToBullet(GameObject bulletObject, Vector2 centerMovingVector)
	{
		bulletObject.GetComponent<MZBullet>().movesList[ 0 ].initMovingVector = centerMovingVector;
	}

	void SetSideWayToBullet(GameObject bulletObject, Vector2 centerMovingVector, int index)
	{
		if( index%2 == 1 )
			SetLeftSideWayToBullet( bulletObject, centerMovingVector, index );
		else
			SetRightSideWayToBullet( bulletObject, centerMovingVector, index );
	}

	void SetLeftSideWayToBullet(GameObject bulletObject, Vector2 centerMovingVector, int index)
	{
		bulletObject.GetComponent<MZBullet>().movesList[ 0 ].initMovingVector = MZMath.UnitVectorFromVectorAddDegree( centerMovingVector, -( index/2 + 1 )*intervalDegrees );
	}

	void SetRightSideWayToBullet(GameObject bulletObject, Vector2 centerMovingVector, int index)
	{
		bulletObject.GetComponent<MZBullet>().movesList[ 0 ].initMovingVector = MZMath.UnitVectorFromVectorAddDegree( centerMovingVector, ( index/2 )*intervalDegrees );
	}

	// temp
	void AddLinearMoveToBullet(GameObject bulletObject)
	{
		MZBullet bullet = bulletObject.GetComponent<MZBullet>();
		MZMove bulletMove = bullet.AddMove( "Linear", "Linear" );
		bulletMove.initVelocity = currentVelocity;
	}
}