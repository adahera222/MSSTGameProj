using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZAttack_OddWay : MZAttack
{
	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		float centerDirection = targetHelp.GetResultDirection();
		float currentDegreesOffset = 0;

		for( int i = 0; i < currentWays; i++ )
		{
			MZBullet bullet = GetNewBullet( i );
			AddLinearMoveToBullet( bullet );

			if( i == 0 )
			{
				bullet.movesList[ 0 ].direction = centerDirection;
				currentDegreesOffset += intervalDegrees;
			}
			else
			{
				bullet.movesList[ 0 ].direction = centerDirection + currentDegreesOffset;
				currentDegreesOffset = ( i%2 == 1 )? -currentDegreesOffset : -currentDegreesOffset + intervalDegrees;
			}

			EnableBullet( bullet );
		}

		UpdateAdditionalVelocity();
	}
}