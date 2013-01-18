using UnityEngine;
using System.Collections;

public class MZAttack_EvenWay : MZAttack
{

	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		float centerDirection = this.targetHelp.GetResultDirection();
		float currentDegrees = intervalDegrees/2;

		for( int i = 0; i < currentWays; i++ )
		{
			MZBullet bullet = GetNewBullet( i );
			AddLinearMoveToBullet( bullet );

			bullet.movesList[ 0 ].direction = centerDirection + currentDegrees;
			currentDegrees = ( i%2 == 0 )? -currentDegrees : -currentDegrees + intervalDegrees;

			EnableBullet( bullet );
		}

		UpdateAdditionalVelocity();
	}
}
