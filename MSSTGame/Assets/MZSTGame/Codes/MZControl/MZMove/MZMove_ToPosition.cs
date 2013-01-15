using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZMove_ToPosition : MZMove, IMZTargetHelp
{
	public float totalMoveTime = -1;
	public Vector2 destinationPosition = Vector2.zero;

	public MZTargetHelp targetHelp
	{
		set
		{
			_targetHelp = value;
			_targetHelp.controlDelegate = this;
		}
		get
		{
			if( _targetHelp == null )
			{
				_targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.Target, this );
			}

			return _targetHelp;
		}
	}

	//

	float _distance;
	Vector2 _startPosition;
	Vector2 _movingVector;
	MZTargetHelp _targetHelp = null;
	//

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

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_startPosition = controlDelegate.position;

		_distance = MZMath.Distance( _startPosition, destinationPosition );
		_movingVector = MZMath.UnitVectorFromP1ToP2( _startPosition, destinationPosition );

		if( totalMoveTime != -1 )
			duration = -1;
	}

	protected override void UpdateWhenActive()
	{
		if( totalMoveTime > 0 )
			UpdateMoveByTotalTime();
		else
			UpdateMoveByVelocity();
	}

	//

	void UpdateMoveByTotalTime()
	{
		float moveValue = lifeTimeCount/totalMoveTime;

		if( moveValue > 1 )
			moveValue = 1;

		float currentDistance = moveValue*_distance;
		controlDelegate.position = _startPosition + new Vector2( _movingVector.x*currentDistance, _movingVector.y*currentDistance );

		if( moveValue >= 1 )
			Disable();
	}

	void UpdateMoveByVelocity()
	{
		MZDebug.Assert( duration != -1, "plz set Duration m(_ _)m" );
		controlDelegate.position = _startPosition + new Vector2( _movingVector.x*currentVelocity, _movingVector.y*currentVelocity );
	}
}
