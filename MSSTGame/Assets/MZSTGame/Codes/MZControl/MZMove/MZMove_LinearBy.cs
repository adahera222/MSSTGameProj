using UnityEngine;
using System.Collections;

public class MZMove_LinearBy : MZMove
{
	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		controlDelegate.position += currentMovingVector*currentVelocity*MZTime.deltaTime;
	}
}