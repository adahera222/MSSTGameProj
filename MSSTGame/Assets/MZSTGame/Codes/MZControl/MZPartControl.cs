using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZPart : IMZAttack, IMZMove
{

}

public class MZPartControl : MZControlBase
{
	public new IMZPart controlTarget = null;
	MZControlUpdate<MZMove> _moveControlUpdate = null;
	MZControlUpdate<MZAttack> _attackControlUpdate = null;

	public MZPartControl()
	{

	}

	public MZPartControl(IMZPart controlTarget)
	{
		MZDebug.Assert( controlTarget != null, "controlTarget is null" );
		this.controlTarget = controlTarget;
	}

	public override void Reset()
	{
		base.Reset();

		if( _moveControlUpdate != null )
			_moveControlUpdate.ResetAll();

		if( _attackControlUpdate != null )
			_attackControlUpdate.ResetAll();
	}

	public List<MZMove> movesList
	{
		get
		{
			if( _moveControlUpdate == null )
				_moveControlUpdate = new MZControlUpdate<MZMove>();

			return _moveControlUpdate.controlsList;
		}
	}

	public List<MZAttack> attacksList
	{
		get
		{
			if( _attackControlUpdate == null )
				_attackControlUpdate = new MZControlUpdate<MZAttack>();

			return _attackControlUpdate.controlsList;
		}
	}

	public MZMove AddMove(string name, string typeString)
	{
		MZMove move = (MZMove)MZObjectHelp.CreateClass( "MZMove_" + typeString );

		move.name = name;
		move.controlTarget = controlTarget;

		movesList.Add( move );

		return move;
	}

	// delete
	public MZAttack AddAttack(string typeString)
	{
		MZAttack attack = (MZAttack)MZObjectHelp.CreateClass( "MZAttack_" + typeString );

		attack.name = "attack";
		attack.controlTarget = controlTarget;

		attacksList.Add( attack );

		return attack;
	}

	public MZAttack AddAttack(MZAttack.Type type)
	{
		MZAttack attack = MZAttack.Create( "attack", type, controlTarget );
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