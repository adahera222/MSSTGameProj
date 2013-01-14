using UnityEngine;
using System.Collections;

public class MZAttack_OddWay : MZAttack
{
	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		// temp
		float centerDirection = targetHelp.GetResultDirection();
		float currentDegrees = 0;

		for( int i = 0; i < currentWays; i++ )
		{
			GameObject bullet = GetNewBulletObject();
			AddLinearMoveToBullet( bullet );
			MZBullet bulletScript = bullet.GetComponent<MZBullet>();

			if( i == 0 )
			{
				bulletScript.movesList[ 0 ].direction = centerDirection;
				currentDegrees += intervalDegrees;
			}
			else
			{
				bulletScript.movesList[ 0 ].direction = centerDirection + currentDegrees;
				currentDegrees = ( i%2 == 1 )? -currentDegrees : -currentDegrees + intervalDegrees;
			}

			EnableBullet( bullet.GetComponent<MZBullet>() );
		}

		UpdateAdditionalVelocity();
	}
}