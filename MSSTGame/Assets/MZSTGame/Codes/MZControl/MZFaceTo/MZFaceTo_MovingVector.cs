using UnityEngine;
using System.Collections;

public class MZFaceTo_MovingVector : MZFaceTo_Base
{
	protected override void UpdateWhenActive()
	{
		MZDebug.Assert( usingMovingVectorType != MZFaceTo_Base.MZMovingVectorType.None, "usingMovingVectorType must be set" );

		Vector2 movingVector = ( usingMovingVectorType == MZFaceTo_Base.MZMovingVectorType.Self )? controlTarget.selfMovingVector : controlTarget.parentMovingVector;
		float rotation = MZMath.DegreesFromXAxisToVector( movingVector );

		controlTarget.rotation = rotation;
	}
}
