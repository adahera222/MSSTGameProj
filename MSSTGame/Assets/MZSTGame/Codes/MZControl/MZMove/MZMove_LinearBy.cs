using UnityEngine;
using System.Collections;

public class MZMove_LinearBy : MZMove
{
	Vector2 _startPosition = Vector2.zero;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		_startPosition = controlDelegate.position;
	}

	protected override void UpdateWhenActive()
	{
		controlDelegate.position += currentMovingVector*currentVelocity*MZTime.deltaTime;
	}
}