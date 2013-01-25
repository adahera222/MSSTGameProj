using UnityEngine;
using System.Collections;

public class MZMove_TurnTo : MZMove
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
		float _destDeg = ( (int)destDeg )%360;
		int rounds = ( (int)_destDeg )/360;
		int remain = ( (int)_destDeg )%360;

		if( remain <= 0 )
			remain = 360 + remain;

		float distance = initDeg - remain;
		MZMove.RotationType resultRotType = ( distance >= 0 )? MZMove.RotationType.CW : MZMove.RotationType.CCW;
		distance = Mathf.Abs( distance );

		if( rotType != resultRotType )
			distance = 360 - distance;

		distance = distance*( ( rotType == MZMove.RotationType.CCW )? 1 : -1 );
		distance += 360*rounds;

		return distance;
	}

}
