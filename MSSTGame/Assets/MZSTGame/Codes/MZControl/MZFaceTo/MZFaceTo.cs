using UnityEngine;
using System.Collections;

public interface IMZFaceTo : IMZControl
{
	Vector2 targetRealPosition
	{
		get;
	}

	Vector2 parentMovingVector
	{
		get;
	}

	Vector2 selfMovingVector
	{
		get;
	}

	Vector2 realPosition
	{
		get;
	}

	float rotation
	{
		set;
		get;
	}

	MZCharacterType characterType
	{
		get;
	}
}

public abstract class MZFaceTo : MZControlBase, IMZTargetHelp
{
	static public MZFaceTo Create(Type type, IMZFaceTo controlTarget)
	{
		MZFaceTo faceTo = (MZFaceTo)MZObjectHelp.CreateClass( "MZFaceTo_" + type.ToString() );
		faceTo.controlTarget = controlTarget;

		return faceTo;
	}

	public enum Type
	{
		None,
		Target,
		MovingVector,
	}

	public enum MZMovingVectorType
	{
		None,
		Self,
		Parent,
	}

	public new IMZFaceTo controlTarget = null;
	public MZMovingVectorType usingMovingVectorType = MZMovingVectorType.Parent;

	#region IMZTargetHelp implementation
	public Vector2 selfPosition
	{
		get	{ return controlTarget.realPosition; }
	}

	public MZCharacterType characterType
	{
		get	{ return controlTarget.characterType; }
	}
	#endregion
}
