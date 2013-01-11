using UnityEngine;
using System.Collections;

public class MZMove_ToTarget : MZMove, IMZTargetHelp
{
	#region IMZTargetHelp implementation

	public Vector2 selfPosition
	{
		get
		{
			return controlDelegate.position;
		}
	}

	public MZCharacterType characterType
	{
		get
		{
			return controlDelegate.characterType;
		}
	}

	#endregion

	//

	MZTargetHelp _targetHelp = null;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.Target, this );
		_targetHelp.calcuteEveryTime = true;
		_targetHelp.BeginOneTime();
	}

	protected override void UpdateWhenActive()
	{
		// mv not change

		// mv always change
		currentMovingVector = _targetHelp.GetResultMovingVector();
		float movement = currentVelocity*MZTime.deltaTime;
		controlDelegate.position = controlDelegate.position + new Vector2( currentMovingVector.x*movement, currentMovingVector.y*movement );

		_targetHelp.EndOneTime();
	}
}