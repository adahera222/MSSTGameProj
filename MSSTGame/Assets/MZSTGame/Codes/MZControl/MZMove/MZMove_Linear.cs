using UnityEngine;
using System.Collections;

public class MZMove_Linear : MZMove
{
	Vector2 _startPosition = Vector2.zero;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		_startPosition = controlDelegate.position;
	}

	protected override void UpdateWhenActive()
	{
		controlDelegate.position = _startPosition + currentMovingVector*currentVelocity*lifeTimeCount;
	}
}