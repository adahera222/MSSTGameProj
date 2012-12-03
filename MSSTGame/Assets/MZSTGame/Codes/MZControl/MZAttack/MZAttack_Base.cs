using UnityEngine;
using System.Collections;

public interface IMZAttack : IMZControl
{
	Vector2 position
	{
		set;
		get;
	}
}

public abstract class MZAttack_Base : MZControlBase
{
	public new IMZAttack controlTarget = null;
}
