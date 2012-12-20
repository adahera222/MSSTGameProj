using UnityEngine;
using System.Collections;

public class MZFaceTo_MovingVector : MZFaceTo
{
	protected override void UpdateWhenActive()
	{
		MZDebug.Assert( usingMovingVectorType != MZFaceTo.MZMovingVectorType.None, "usingMovingVectorType must be set" );

		Vector2 movingVector = ( usingMovingVectorType == MZFaceTo.MZMovingVectorType.Self )? controlTarget.selfMovingVector : controlTarget.parentMovingVector;
		float rotation = MZMath.DegreesFromXAxisToVector( movingVector );

		controlTarget.rotation = rotation;
	}
}
