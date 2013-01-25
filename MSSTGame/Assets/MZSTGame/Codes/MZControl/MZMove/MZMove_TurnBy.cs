using UnityEngine;
using System.Collections;

public class MZMove_TurnBy : MZMove
{
	public float variationDegreesPerSecond;

	//

	float _initDegrees;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_initDegrees = this.direction;
	}

	protected override void UpdateWhenActive()
	{
		MaintainCurrentDirectionValue( _initDegrees + variationDegreesPerSecond*lifeTimeCount );
		controlDelegate.position += currentMovingVector*currentVelocity*MZTime.deltaTime;
	}
}