using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

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


	public MZTargetHelp_Target target
	{
		get
		{
			if( _targetHelp == null )
				_targetHelp = MZTargetHelp.Create<MZTargetHelp_Target>( this );
			return _targetHelp;
		}
	}

	//

	MZTargetHelp_Target _targetHelp = null;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		target.BeginOneTime();
	}

	protected override void UpdateWhenActive()
	{
		MaintainCurrentDirectionValue( _targetHelp.GetResultDirection() );
		float movement = currentVelocity*MZTime.deltaTime;
		controlDelegate.position = controlDelegate.position + new Vector2( currentMovingVector.x*movement, currentMovingVector.y*movement );

		target.EndOneTime();
	}
}