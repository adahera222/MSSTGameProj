using UnityEngine;
using System.Collections;

public class MZFaceTo_Target : MZFaceTo_Base
{
	protected override void UpdateWhenActive()
	{
		MZTargetHelp_Base targetHelp = new MZTargetHelp_Target();
		targetHelp.controlObject = this;
		float rotation = MZMath.DegreesFromXAxisToVector( targetHelp.GetMovingVector() );

		controlTarget.rotation = rotation;
	}
}