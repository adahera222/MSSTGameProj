using UnityEngine;
using System.Collections;

public class MZFaceTo_Target : MZFaceTo, IMZTargetHelp
{
	public MZTargetHelp target
	{
		set
		{
			_target = value;
			_target.controlDelegate = this;
		}
		get
		{
			return _target;
		}
	}

	MZTargetHelp _target = null;


	protected override void UpdateWhenActive()
	{
		MZDebug.Assert( _target != null, "_target is null" );
		float rotation = _target.GetResultDirection();
		controlDelegate.rotation = rotation;
	}
}