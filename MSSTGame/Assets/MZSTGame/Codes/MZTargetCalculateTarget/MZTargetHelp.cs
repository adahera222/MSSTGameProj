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

public abstract class MZTargetHelp_Base
{
	public IMZTargetHelp controlObject = null;

	public Vector2 GetMovingVector()
	{
		MZDebug.Assert( controlObject != null, "controlObject is null" );
		return CalculateMovingVector();
	}

	protected abstract Vector2 CalculateMovingVector();

	protected Vector2 GetPlayerPosition()
	{
		return MZGameComponents.GetInstance().charactersManager.GetPlayerPosition();
	}
}

public class MZTargetHelp_Target : MZTargetHelp_Base
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
				return GetPlayerPosition();

			default:
				MZDebug.Assert( false, "SORRY, not support this type=" + controlObject.characterType.ToString() );
				return Vector2.zero;
		}
	}
}

public class MZTargetHelp_AssignMovingVector : MZTargetHelp_Base
{
	Vector2 _movingVector = Vector2.zero;

	public MZTargetHelp_AssignMovingVector(Vector2 movingVector)
	{
		_movingVector = movingVector;
	}

	protected override Vector2 CalculateMovingVector()
	{
		return _movingVector;
	}
}