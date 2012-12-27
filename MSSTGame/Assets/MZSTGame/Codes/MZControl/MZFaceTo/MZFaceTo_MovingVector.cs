using UnityEngine;
using System.Collections;

public class MZFaceTo_MovingVector : MZFaceTo
{
	protected override void UpdateWhenActive()
	{
		MZDebug.Assert( usingMovingVectorType != MZFaceTo.MZMovingVectorType.None, "usingMovingVectorType must be set" );

		Vector2 movingVector = ( usingMovingVectorType == MZFaceTo.MZMovingVectorType.Self )? controlDelegate.selfMovingVector : controlDelegate.parentMovingVector;
		float rotation = MZMath.DegreesFromXAxisToVector( movingVector );

		controlDelegate.rotation = rotation;
	}
}
