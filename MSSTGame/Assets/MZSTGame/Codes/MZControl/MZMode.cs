/*
 * main move ... ok
 * Part control
 **/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZMode : IMZControl
{
	IMZMove moveControlTarget
	{
		get;
	}
}

public class MZMode : MZControlBase
{
	public new IMZMode controlTarget = null;
	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;

	public override void Reset()
	{
		base.Reset();

		_moveControlUpdate.ResetAll();
	}

	public List<MZMove_Base> movesList
	{
		get
		{
			if( _moveControlUpdate == null )
				_moveControlUpdate = new MZControlUpdate<MZMove_Base>();

			return _moveControlUpdate.controlsList;
		}
	}

	public MZMove_Base AddMove(string name, string typeString)
	{
		MZMove_Base move = (MZMove_Base)MZObjectHelp.CreateClass( "MZMove_" + typeString );

		move.name = name;
		move.controlTarget = controlTarget.moveControlTarget;

		movesList.Add( move );

		return move;
	}

	protected override void UpdateWhenActive()
	{
		_moveControlUpdate.Update();
	}
}
