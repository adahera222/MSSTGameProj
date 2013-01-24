using UnityEngine;
using System.Collections;

public class MZAttack_Vortex : MZAttack
{
	public bool resetAdditionalVelocityPerWave = false;
	public float timePerWave = -1;
	public float resetTime = -1;

	//

	float initDegrees;
	float launchTimeCount;
	float resetTimeCount;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		MZDebug.Assert( targetHelp != null, "plz set targetHelp" );

		launchTimeCount = 0;
		resetTimeCount = 0;

		targetHelp.calcuteEveryTime = false;
		targetHelp.BeginOneTime();

		initDegrees = targetHelp.GetResultDirection();
	}

	protected override void LaunchBullet()
	{
		base.LaunchBullet();

		if( IsResting() )
			return;

		LaunchWaveBullet();
	}

	//

	bool IsResting()
	{
		resetTimeCount -= MZTime.deltaTime;
		return ( resetTimeCount >= 0 );
	}

	void LaunchWaveBullet()
	{
		MZBullet bullet = GetNewBullet( 0 );

		float currentDegrees = initDegrees + intervalDegrees*( launchCount - 1 );
		AddLinearMoveToBullet( bullet );

		bullet.movesList[ 0 ].velocity = currentVelocity;
		bullet.movesList[ 0 ].direction = currentDegrees;

		EnableBullet( bullet );

		if( timePerWave <= 0 )
			return;

		launchTimeCount += MZTime.deltaTime;

		if( launchTimeCount >= timePerWave )
		{
			resetTimeCount = resetTime;
			launchTimeCount = 0;

			if( resetAdditionalVelocityPerWave )
			{
				currentAdditionalVelocity = 0;
			}
		}

		UpdateAdditionalVelocity();
	}
}
