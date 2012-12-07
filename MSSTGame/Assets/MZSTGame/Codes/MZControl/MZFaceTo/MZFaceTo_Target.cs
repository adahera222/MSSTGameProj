using UnityEngine;
using System.Collections;

public class MZFaceTo_Target : MZFaceTo_Base
{
	protected override void UpdateWhenActive()
	{
		Vector2 vectorToTarget = MZMath.UnitVectorFromP1ToP2( controlTarget.realPosition, controlTarget.targetRealPosition );
		float rotation = MZMath.DegreesFromXAxisToVector( vectorToTarget );

		controlTarget.rotation = rotation;
	}
}
