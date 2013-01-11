using UnityEngine;
using System.Collections;

public class MZMove_DegreesBy : MZMove
{
	public float variationDegreesPerSecond;

	//

	float _initDegrees;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_initDegrees = MZMath.DegreesFromXAxisToVector( this.initMovingVector );
	}

	protected override void UpdateWhenActive()
	{
		float currentDegrees = _initDegrees + variationDegreesPerSecond*lifeTimeCount;
		currentMovingVector = MZMath.UnitVectorFromDegrees( currentDegrees );

		controlDelegate.position += currentMovingVector*currentVelocity*MZTime.deltaTime;
	}
}