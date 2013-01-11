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

		Vector2 mv = targetHelp.GetResultMovingVector();
		initDegrees = MZMath.DegreesFromXAxisToVector( mv );
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
		GameObject bulletObject = GetNewBulletObject();

		float currentDegrees = initDegrees + intervalDegrees*( launchCount - 1 );
		Vector2 currentMovingVector = MZMath.UnitVectorFromDegrees( currentDegrees );

		MZBullet bullet = bulletObject.GetComponent<MZBullet>();
		MZMove bulletMove = bullet.AddMove( "Linear", "Linear" );
		bulletMove.initVelocity = currentVelocity;
		bullet.movesList[ 0 ].initMovingVector = currentMovingVector;

		EnableBullet( bullet.GetComponent<MZBullet>() );

		if( timePerWave == -1 )
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
