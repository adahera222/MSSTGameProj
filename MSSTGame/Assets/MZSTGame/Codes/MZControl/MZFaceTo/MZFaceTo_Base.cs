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
}

public abstract class MZFaceTo_Base : MZControlBase
{
	public enum MZMovingVectorType
	{
		None,
		Self,
		Parent,
	}

	public new IMZFaceTo controlTarget = null;
	public MZMovingVectorType usingMovingVectorType = MZMovingVectorType.Parent;
}
