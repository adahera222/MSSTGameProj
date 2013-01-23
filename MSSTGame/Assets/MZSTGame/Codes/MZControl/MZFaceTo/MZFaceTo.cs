using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public interface IMZFaceTo : IMZControl
{
	Vector2 targetRealPosition
	{
		get;
	}

	float movingDirection
	{ get; }

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

public abstract class MZFaceTo : MZControlBase
{
	static public F Create<F>(IMZFaceTo controlTarget) where F : MZFaceTo, new()
	{
		F faceTo = new F();
		faceTo.controlDelegate = controlTarget;

		return faceTo;
	}

	public new IMZFaceTo controlDelegate = null;

	#region IMZTargetHelp implementation
	public Vector2 selfPosition
	{
		get	{ return controlDelegate.realPosition; }
	}

	public MZCharacterType characterType
	{
		get	{ return controlDelegate.characterType; }
	}
	#endregion
}
