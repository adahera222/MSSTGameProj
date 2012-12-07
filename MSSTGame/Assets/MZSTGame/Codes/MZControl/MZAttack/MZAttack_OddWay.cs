using UnityEngine;
using System.Collections;

public class MZAttack_OddWay : MZAttack_Base
{
	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		Vector2 centerMovingVector = GetCenterMovingVector();

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
		}
	}

	Vector2 GetCenterMovingVector()
	{
		Vector2 playerPosition = MZGameComponents.GetInstance().charactersManager.GetPlayerPosition();
		Vector2 selfPosition = controlTarget.realPosition;

		return 	MZMath.UnitVectorFromP1ToP2( selfPosition, playerPosition );
	}

	void SetCenterWayToBullet(GameObject bulletObject, Vector2 centerMovingVector)
	{
		bulletObject.GetComponent<MZEnemyBullet>().movesList[ 0 ].initMovingVector = centerMovingVector;
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
		bulletObject.GetComponent<MZEnemyBullet>().movesList[ 0 ].initMovingVector = MZMath.UnitVectorFromVectorAddDegree( centerMovingVector, -( index/2 + 1 )*intervalDegrees );
	}

	void SetRightSideWayToBullet(GameObject bulletObject, Vector2 centerMovingVector, int index)
	{
		bulletObject.GetComponent<MZEnemyBullet>().movesList[ 0 ].initMovingVector = MZMath.UnitVectorFromVectorAddDegree( centerMovingVector, ( index/2 )*intervalDegrees );
	}

	// temp
	void AddLinearMoveToBullet(GameObject bulletObject)
	{
		MZEnemyBullet bullet = bulletObject.GetComponent<MZEnemyBullet>();
		MZMove_Base bulletMove = bullet.AddMove( "Linear", "Linear" );
		bulletMove.initVelocity = currentVelocity;
	}
}