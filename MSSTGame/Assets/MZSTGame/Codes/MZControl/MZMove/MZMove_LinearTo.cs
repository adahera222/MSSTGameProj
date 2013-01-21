using UnityEngine;
using System.Collections;

public class MZMove_LinearTo : MZMove
{
	public bool notEndAtDestation = false;
	public Vector2 destationPosition = Vector2.zero;
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
		_moveDistanceXY = destationPosition - _initPosiiton;
		MaintainCurrentDirectionValue( MZMath.DegreesFromXAxisToVector( _moveDistanceXY ) );
	}

	protected override void UpdateWhenActive()
	{
		float currentProp = lifeTimeCount/totalTime;
		if( currentProp > 1 && notEndAtDestation == false )
			currentProp = 1;

		controlDelegate.position = _initPosiiton + ( _moveDistanceXY*currentProp );
	}
}
