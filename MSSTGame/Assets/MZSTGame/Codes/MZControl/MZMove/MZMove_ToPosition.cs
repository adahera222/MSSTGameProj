using UnityEngine;
using System.Collections;

public class MZMove_ToPosition : MZMove
{
	public float totalMoveTime = -1;
	public Vector2 destinationPosition = Vector2.zero;

	//

	float _distance;
	Vector2 _startPosition;
	Vector2 _movingVector;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_startPosition = controlDelegate.position;

		_distance = MZMath.Distance( _startPosition, destinationPosition );
		_movingVector = MZMath.UnitVectorFromP1ToP2( _startPosition, destinationPosition );

		if( totalMoveTime != -1 )
			duration = -1;
	}

	protected override void UpdateWhenActive()
	{
		if( totalMoveTime > 0 )
			UpdateMoveByTotalTime();
		else
			UpdateMoveByVelocity();
	}

	//

	void UpdateMoveByTotalTime()
	{
		float moveValue = lifeTimeCount/totalMoveTime;

		if( moveValue > 1 )
			moveValue = 1;

		float currentDistance = moveValue*_distance;
		controlDelegate.position = _startPosition + new Vector2( _movingVector.x*currentDistance, _movingVector.y*currentDistance );

		if( moveValue >= 1 )
			Disable();
	}

	void UpdateMoveByVelocity()
	{
		MZDebug.Assert( duration != -1, "plz set Duration m(_ _)m" );
		controlDelegate.position = _startPosition + new Vector2( _movingVector.x*currentVelocity, _movingVector.y*currentVelocity );
	}
}
