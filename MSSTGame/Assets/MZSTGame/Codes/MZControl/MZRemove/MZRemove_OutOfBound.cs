using UnityEngine;
using System.Collections;

public interface IMZRemove: IMZControl
{
	float lifeTimeCount
	{ get; }

	Vector2 frameSize
	{ get; }

	Vector2 position
	{ get; }

	void DoRemoveOutOfBound();
}

public class MZRemove_OutOfBound : MZControlBase
{
	public new IMZRemove controlTarget;

	protected override void UpdateWhenActive()
	{
		if( lifeTimeCount <= 5.0f )
			return;

		Vector2 pos = controlTarget.position;
		Vector2 halfSize = controlTarget.frameSize/2;

		Rect boundRect = MZGameSetting.GetPlayerMovableBoundRect();

		if( pos.x + halfSize.x < boundRect.x ||
			pos.x - halfSize.x > boundRect.x + boundRect.width ||
			pos.y + halfSize.y < boundRect.y - boundRect.height ||
			pos.y - halfSize.y > boundRect.y )
		{
			controlTarget.DoRemoveOutOfBound();
		}
	}
}
