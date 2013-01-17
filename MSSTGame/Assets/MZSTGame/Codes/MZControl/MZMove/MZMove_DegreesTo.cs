using UnityEngine;
using System.Collections;

public class MZMove_DegreesTo : MZMove
{
	public float destinationDegrees;
	public float totalTime = 10;
	public RotationType rotationType = RotationType.CW;

	//

	float _degreesDistance;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		MZDebug.Assert( totalTime > 0, "totalTime must be set" );
		_degreesDistance = GetDegreesDistance( rotationType, direction, destinationDegrees );
	}

	protected override void UpdateWhenActive()
	{
		float currentProportion = lifeTimeCount/totalTime;
		if( currentProportion > 1 )
			currentProportion = 1;

		MaintainCurrentDirectionValue( direction + _degreesDistance*currentProportion );
		Vector2 movementXY = currentMovingVector*currentVelocity*MZTime.deltaTime;

		controlDelegate.position += movementXY;
	}

	float GetDegreesDistance(RotationType rotType, float initDeg, float destDeg)
	{
		float _destDeg = destDeg;
		int rounds = ( (int)_destDeg )/360;
		int remain = ( (int)_destDeg )%360;

		if( remain < 0 )
			remain = 360 + remain;

		float distance = Mathf.Abs( remain - initDeg );

		if( rotType == RotationType.CW )
			distance = -( 360 - distance );
		distance += 360*rounds;

		return distance;
	}

}
