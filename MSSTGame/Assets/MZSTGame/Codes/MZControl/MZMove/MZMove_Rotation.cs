using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZMove_Rotation : MZMove, IMZTargetHelp
{
	public float angularVelocity = 0;
	public float variationOfRadians = 0;
	public float radiansLimited = -1;

	public MZTargetHelp_AssignPosition targetHelp
	{
		get
		{
			if( _targetHelp == null )
			{
				_targetHelp = new MZTargetHelp_AssignPosition();
				_targetHelp.controlDelegate = this;
				_targetHelp.calcuteEveryTime = false;
			}

			return _targetHelp;
		}
	}

	public override Vector2 currentMovingVector
	{
		get
		{
			return MZMath.UnitVectorFromDegrees( _currentTheta + ( ( angularVelocity >= 0 )? 90 : -90 ) );
		}
	}

	//

	float _currentTheta;
	float _currentRadians;
	MZTargetHelp_AssignPosition _targetHelp;

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

		targetHelp.Reset();
		targetHelp.BeginOneTime();

		Vector2 vector = MZMath.UnitVectorFromP1ToP2( GetCenter(), controlDelegate.position );
		float degree = MZMath.DegreesFromXAxisToVector( vector );

		_currentTheta = degree;
		_currentRadians = MZMath.Distance( GetCenter(), controlDelegate.position );

		if( radiansLimited < 0 )
			radiansLimited = 0;
	}

	protected override void UpdateWhenActive()
	{
		UpdateTheta();
		UpdateRadians();
		UpdateControlDelegate();
	}

	//

	void UpdateTheta()
	{
		_currentTheta += angularVelocity*MZTime.deltaTime;
	}

	void UpdateRadians()
	{
		if( variationOfRadians == 0 )
			return;

		_currentRadians += variationOfRadians*MZTime.deltaTime;

		if( radiansLimited > 0 )
		{
			if( ( variationOfRadians > 0 && _currentRadians > radiansLimited ) || ( variationOfRadians < 0 && _currentRadians < radiansLimited ) )
				_currentRadians = radiansLimited;
		}
	}

	void UpdateControlDelegate()
	{
		float _currentThetaRadians = MZMath.DegreesToRadians( _currentTheta );

		float x = GetCenter().x + _currentRadians*Mathf.Cos( _currentThetaRadians );
		float y = GetCenter().y + _currentRadians*Mathf.Sin( _currentThetaRadians );

		controlDelegate.position = new Vector2( x, y );
	}

	Vector2 GetCenter()
	{
		return targetHelp.GetResultPosition();
	}
}