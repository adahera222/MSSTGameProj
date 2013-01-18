using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public interface IMZTargetHelp
{
	Vector2 selfPosition
	{
		get;
	}

	MZCharacterType characterType
	{
		get;
	}
}

public abstract class MZTargetHelp
{
	static public T Create<T>() where T : MZTargetHelp, new()
	{
		T targetHelp = new T();
		return targetHelp;
	}

	static public T Create<T>(IMZTargetHelp controlDelegate) where T : MZTargetHelp, new()
	{
		MZDebug.Assert( controlDelegate != null, "controlDelegate is null" );

		T targetHelp = new T();
		targetHelp.controlDelegate = controlDelegate;

		return targetHelp;
	}


	public enum Type
	{
		Target,
		AssignDirection,
		AssignPosition,
	}

	//

	public bool calcuteEveryTime = false;
	public IMZTargetHelp controlDelegate = null;

	//

	bool _needCalcute = true;
	float _resultDirection = 0;
//	Vector2 _movingVectorResult = Vector2.zero;

	//

	public void Reset()
	{
		_needCalcute = true;
		_resultDirection = 0;
	}

	public float GetResultDirection()
	{
		MZDebug.Assert( controlDelegate != null, "controlObject is null" );

		if( _needCalcute == true )
			_resultDirection = CalculateResultDirection();

		return _resultDirection;
	}

	public abstract Vector2 GetResultPosition();

	public virtual void BeginOneTime()
	{

	}

	public virtual void EndOneTime()
	{
		_needCalcute = calcuteEveryTime;
	}

	//

//	protected abstract Vector2 CalculateMovingVector();
	protected abstract float CalculateResultDirection();

	//

	protected Vector2 GetPlayerPosition()
	{
		return MZGameComponents.GetInstance().charactersManager.GetPlayerPosition();
	}
}

public class MZTargetHelp_Target : MZTargetHelp
{
	public override Vector2 GetResultPosition()
	{
		MZDebug.AssertFalse( "not support" );
		return Vector2.zero;
	}

	protected override float CalculateResultDirection()
	{
		Vector2 vector = GetTargetPosition() - controlDelegate.selfPosition;
		return MZMath.DegreesFromXAxisToVector( vector );
	}

	Vector2 GetTargetPosition()
	{
		switch( controlDelegate.characterType )
		{
			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
			case MZCharacterType.EnemyBullet:
				return GetPlayerPosition();

			default:
				MZDebug.Assert( false, "SORRY, not support this type=" + controlDelegate.characterType.ToString() );
				return Vector2.zero;
		}
	}
}

public class MZTargetHelp_AssignDirection : MZTargetHelp
{
	public float direction = 0;

	public override Vector2 GetResultPosition()
	{
		MZDebug.AssertFalse( "not support" );
		return Vector2.zero;
	}

	protected override float CalculateResultDirection()
	{
		return direction;
	}
}

public class MZTargetHelp_AssignPosition : MZTargetHelp
{
	public enum AssignType
	{
		Absolute,
		Relative,
	}

	public AssignType assignType = AssignType.Absolute;
	public Vector2 assignPosition = Vector2.zero;

	Vector2 _absolutePosition;

	public override void BeginOneTime()
	{
		base.BeginOneTime();

		_absolutePosition = ( assignType == AssignType.Relative )? controlDelegate.selfPosition + assignPosition : assignPosition;
	}

	public override Vector2 GetResultPosition()
	{
		return _absolutePosition;
	}

	protected override float CalculateResultDirection()
	{
		Vector2 vector = _absolutePosition - controlDelegate.selfPosition;
		return MZMath.DegreesFromXAxisToVector( vector );
	}
}