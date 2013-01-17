using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

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
		LinearBy,
		LinearTo,
		ToPosition,
		ToTarget,
		Rotation,
		DegreesBy,
		DegreesTo,
	}

	public enum RotationType
	{
		CW,
		CCW,
	}


	public new IMZMove controlDelegate;
	public float direction = 0;
	public float velocity = 0;

	public float currentDirection
	{ get { return _currentDirection; } }

	public float currentVelocity
	{ get { return _currentVelocity; } }

	public virtual Vector2 currentMovingVector
	{
		get { return MZMath.UnitVectorFromDegrees( currentDirection );}
	}

	public override void Enable()
	{
		base.Enable();
		_currentVelocity = velocity;
		_currentDirection = direction;
	}

	//

	float _currentDirection = 0;

	//

	float _currentVelocity = 0;

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_currentVelocity = velocity;
		_currentDirection = direction;
	}

	protected void MaintainCurrentDirectionValue(float dir)
	{
		_currentDirection = dir;
	}
}