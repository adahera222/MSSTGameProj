using UnityEngine;
using System.Collections;

public class MZFaceTo_Target : MZFaceTo
{
	protected override void UpdateWhenActive()
	{
		MZTargetHelp targetHelp = new MZTargetHelp_Target();
		targetHelp.controlObject = this;
		float rotation = MZMath.DegreesFromXAxisToVector( targetHelp.GetMovingVector() );

		controlTarget.rotation = rotation;
	}
}