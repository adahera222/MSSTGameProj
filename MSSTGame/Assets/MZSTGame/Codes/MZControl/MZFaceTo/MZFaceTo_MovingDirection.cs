using UnityEngine;
using System.Collections;

public class MZFaceTo_MovingDirection : MZFaceTo
{
	protected override void UpdateWhenActive()
	{
		controlDelegate.rotation = controlDelegate.movingDirection;
	}
}
