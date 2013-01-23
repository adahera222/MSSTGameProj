using UnityEngine;
using System.Collections;

public class MZMove_LinearTo : MZMove
{
	public bool notEndAtDestation = false;
	public bool useRelativePosition = false; // ready to target
	public Vector2 destinationPosition = Vector2.zero; // ready to target
	public float totalTime = 0;

	//

	Vector2 _initPosiiton;
	Vector2 _moveDistanceXY;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		MZDebug.Assert( totalTime > 0, "totalTime must be set" );

		_initPosiiton = controlDelegate.position;
		destinationPosition = GetDestinationPosition( useRelativePosition, _initPosiiton, destinationPosition );

		_moveDistanceXY = destinationPosition - _initPosiiton;
		MaintainCurrentDirectionValue( MZMath.DegreesFromXAxisToVector( _moveDistanceXY ) );
	}

	protected override void UpdateWhenActive()
	{
		float currentProp = lifeTimeCount/totalTime;
		if( currentProp > 1 && notEndAtDestation == false )
			currentProp = 1;

		controlDelegate.position = _initPosiiton + ( _moveDistanceXY*currentProp );
	}

	Vector2 GetDestinationPosition(bool useRelative, Vector2 startPosition, Vector2 destPosition)
	{
		return ( useRelative )? startPosition + destPosition : destPosition;
	}
}
