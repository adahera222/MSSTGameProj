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
	static public MZTargetHelp Create(Type type, IMZTargetHelp controlObject)
	{
		MZTargetHelp targetHelp = (MZTargetHelp)MZObjectHelp.CreateClass( "MZTargetHelp_" + type.ToString() );
		targetHelp.controlObject = controlObject;
		return targetHelp;
	}


	public enum Type
	{
		Target,
		AssignMovingVector,
	}

	//

	public bool calcuteEveryTime = false;
	public IMZTargetHelp controlObject = null;

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
		MZDebug.Assert( controlObject != null, "controlObject is null" );

		if( _needCalcute == true )
			_movingVectorResult = CalculateMovingVector();

		return _movingVectorResult;
	}

	public void EndOneTime()
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
		return MZMath.UnitVectorFromP1ToP2( controlObject.selfPosition, GetTargetPosition() );
	}

	Vector2 GetTargetPosition()
	{
		switch( controlObject.characterType )
		{
			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
			case MZCharacterType.EnemyBullet:
				return GetPlayerPosition();

			default:
				MZDebug.Assert( false, "SORRY, not support this type=" + controlObject.characterType.ToString() );
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