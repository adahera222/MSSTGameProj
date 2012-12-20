using UnityEngine;
using System.Collections;

public class MZMove_Linear : MZMove
{
	Vector2 startPosition = Vector2.zero;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		startPosition = controlTarget.position;
	}

	protected override void UpdateWhenActive()
	{
		controlTarget.position = startPosition + currentMovingVector*currentVelocity*lifeTimeCount;
	}
}