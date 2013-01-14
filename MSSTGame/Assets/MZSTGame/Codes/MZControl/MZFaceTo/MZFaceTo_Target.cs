using UnityEngine;
using System.Collections;

public class MZFaceTo_Target : MZFaceTo
{
	protected override void UpdateWhenActive()
	{
		MZTargetHelp targetHelp = new MZTargetHelp_Target();
		targetHelp.controlDelegate = this;
		float rotation = targetHelp.GetResultDirection();

		controlDelegate.rotation = rotation;
	}
}