using UnityEngine;
using System.Collections;

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
	static public MZTargetHelp Create(Type type)
	{
		MZTargetHelp targetHelp = (MZTargetHelp)MZObjectHelp.CreateClass( "MZTargetHelp_" + type.ToString() );
		return targetHelp;
	}

	static public MZTargetHelp Create(Type type, IMZTargetHelp controlDelegate)
	{
		MZDebug.Assert( controlDelegate != null, "controlDelegate is null" );

		MZTargetHelp targetHelp = (MZTargetHelp)MZObjectHelp.CreateClass( "MZTargetHelp_" + type.ToString() );
		targetHelp.controlDelegate = controlDelegate;
		return targetHelp;
	}


	public enum Type
	{
		Target,
		AssignMovingVector,
		AssignPosition,
	}

	//

	public bool calcuteEveryTime = false;
	public IMZTargetHelp controlDelegate = null;

	//

	bool _needCalcute = true;
	Vector2 _movingVectorResult = Vector2.zero;

	//

	public void Reset()
	{
		_needCalcute = true;
		_movingVectorResult = Vector2.zero;
	}

	public Vector2 GetMovingVector()
	{
		MZDebug.Assert( controlDelegate != null, "controlObject is null" );

		if( _needCalcute == true )
			_movingVectorResult = CalculateMovingVector();

		return _movingVectorResult;
	}

	public virtual void BeginOneTime()
	{

	}

	public virtual void EndOneTime()
	{
		_needCalcute = calcuteEveryTime;
	}

	//

	protected abstract Vector2 CalculateMovingVector();

	//

	protected Vector2 GetPlayerPosition()
	{
		return MZGameComponents.GetInstance().charactersManager.GetPlayerPosition();
	}
}

public class MZTargetHelp_Target : MZTargetHelp
{
	protected override Vector2 CalculateMovingVector()
	{
		return MZMath.UnitVectorFromP1ToP2( controlDelegate.selfPosition, GetTargetPosition() );
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

public class MZTargetHelp_AssignMovingVector : MZTargetHelp
{
	public Vector2 movingVector = Vector2.zero;

	protected override Vector2 CalculateMovingVector()
	{
		return movingVector;
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

	protected override Vector2 CalculateMovingVector()
	{
		Vector2 mvTotargetPos = MZMath.UnitVectorFromP1ToP2( controlDelegate.selfPosition, _absolutePosition );
		return mvTotargetPos;
	}
}