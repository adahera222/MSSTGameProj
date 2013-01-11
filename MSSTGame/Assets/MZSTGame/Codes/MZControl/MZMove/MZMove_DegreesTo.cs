using UnityEngine;
using System.Collections;

public class MZMove_DegreesTo : MZMove
{
	public float destinationDegrees;
	public float totalTime = -1;
	public RotationType rotationType = RotationType.CW;

	//

	float _initDegrees;
	float _destinationDegrees;
	float _diffDegrees;
	float _timeCount;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		MZDebug.Assert( totalTime > 0, "totalTime must be set" );

		_timeCount = 0;
		_initDegrees = MZMath.DegreesFromXAxisToVector( this.initMovingVector );

		_destinationDegrees = NormalizeDestinationDegrees( rotationType, destinationDegrees );
		_diffDegrees = _destinationDegrees - _initDegrees;
	}

	protected override void UpdateWhenActive()
	{
		_timeCount += MZTime.deltaTime;

		if( _timeCount > totalTime )
			_timeCount = totalTime;

		float currentDegrees = _initDegrees + _diffDegrees*( _timeCount/totalTime );
		currentMovingVector = MZMath.UnitVectorFromDegrees( currentDegrees );

		controlDelegate.position += currentMovingVector*currentVelocity*MZTime.deltaTime;
	}

	float NormalizeDestinationDegrees(MZMove.RotationType rotType, float originDestinationDegrees)
	{
		float _destDeg = originDestinationDegrees;

		if( rotType == RotationType.CW && _destDeg > 0 )
		{
			int rounds = ( (int)_destDeg )/360;
			int remain = ( (int)_destDeg )%360;

			_destDeg = -( 360.0f - remain ) + 360*rounds;
		}

		if( rotType == RotationType.CCW && _destDeg < 0 )
		{
			int rounds = ( (int)_destDeg )/360;
			int remain = -( (int)_destDeg )%360;

			_destDeg = ( 360 - remain ) + 360*rounds;
		}

		return _destDeg;
	}
}
