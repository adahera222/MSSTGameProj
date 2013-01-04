using UnityEngine;
using System.Collections;

public interface IMZMove : IMZControl
{
	Vector2 position
	{
		set;
		get;
	}

	MZCharacterType characterType
	{
		get;
	}
}

public abstract class MZMove : MZControlBase
{
	static public MZMove Create(Type type, string name, IMZMove controlDelegate)
	{
		MZDebug.Assert( controlDelegate != null, "controlTarget is null" );

		MZMove move = (MZMove)MZObjectHelp.CreateClass( "MZMove_" + type.ToString() );
		MZDebug.Assert( move != null, "create fail, type=" + type.ToString() );

		move.name = name;
		move.controlDelegate = controlDelegate;

		return move;
	}

	public enum Type
	{
		Idle,
		Linear,
		ToPosition,
		ToTarget,
	}

	public new IMZMove controlDelegate;
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