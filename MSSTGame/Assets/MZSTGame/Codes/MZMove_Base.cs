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
	public Vector2 initMovingVector = Vector2.zero;

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

	float _currentVelocity = 0;
	Vector2 _currentMovingVector = Vector2.zero;
}
