using UnityEngine;
using System.Collections;

public interface IMZMove : IMZControl
{
	Vector2 position
	{
		set;
		get;
	}
}

public abstract class MZMove_Base : MZControlBase
{
	public new IMZMove controlTarget;
	public float initVelocity = 0;

	public Vector2 initMovingVector
	{
		set{ _initMovingVector = MZMath.UnitVectorFromVector( value ); }
		get{ return _initMovingVector; }
	}

	public float currentVelocity
	{ get { return _currentVelocity; } }

	public Vector2 currentMovingVector
	{
		set { _currentMovingVector = value; }
		get { return _currentMovingVector; }
	}

	public override void Reset()
	{
		base.Reset();

		_currentVelocity = initVelocity;
		_currentMovingVector = initMovingVector;
	}

	Vector2 _initMovingVector = Vector2.zero;

	float _currentVelocity = 0;
	Vector2 _currentMovingVector = Vector2.zero;
}