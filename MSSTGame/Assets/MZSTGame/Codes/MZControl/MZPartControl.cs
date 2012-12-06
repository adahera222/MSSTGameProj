using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZPart : IMZAttack, IMZMove
{

}

public class MZPartControl : MZControlBase
{
	public new IMZPart controlTarget = null;
	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;
	MZControlUpdate<MZAttack_Base> _attackControlUpdate = null;

	public override void Reset()
	{
		base.Reset();

		if( _moveControlUpdate != null )
			_moveControlUpdate.ResetAll();

		if( _attackControlUpdate != null )
			_attackControlUpdate.ResetAll();
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

	public List<MZAttack_Base> attacksList
	{
		get
		{
			if( _attackControlUpdate == null )
				_attackControlUpdate = new MZControlUpdate<MZAttack_Base>();

			return _attackControlUpdate.controlsList;
		}
	}

	public MZMove_Base AddMove(string name, string typeString)
	{
		MZMove_Base move = (MZMove_Base)MZObjectHelp.CreateClass( "MZMove_" + typeString );

		move.name = name;
		move.controlTarget = controlTarget;

		movesList.Add( move );

		return move;
	}

	public MZAttack_Base AddAttack(string typeString)
	{
		MZAttack_Base attack = (MZAttack_Base)MZObjectHelp.CreateClass( "MZAttack_" + typeString );

		attack.name = "attack";
		attack.controlTarget = controlTarget;

		attacksList.Add( attack );

		return attack;
	}

	protected override void UpdateWhenActive()
	{
		if( _moveControlUpdate != null )
			_moveControlUpdate.Update();

		if( _attackControlUpdate != null )
		{
			_attackControlUpdate.Update();
		}
	}
}