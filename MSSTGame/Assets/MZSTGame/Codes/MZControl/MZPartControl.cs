using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZPart : IMZAttack, IMZMove
{
	MZFaceTo faceTo
	{
		set;
		get;
	}
}

public class MZPartControl : MZControlBase
{
	public delegate MZFaceTo GetFaceTo();

	//
	public new IMZPart controlDelegate = null;
	public GetFaceTo getFaceTo;

	//
	MZControlUpdate<MZMove> _moveControlUpdate = null;
	MZControlUpdate<MZAttack> _attackControlUpdate = null;

	//
	public MZPartControl()
	{

	}

	public MZPartControl(IMZPart controlTarget)
	{
		MZDebug.Assert( controlTarget != null, "controlTarget is null" );
		this.controlDelegate = controlTarget;
	}

	public override void Enable()
	{
		base.Enable();

		if( getFaceTo != null )
		{
			controlDelegate.faceTo = getFaceTo();
			controlDelegate.faceTo.Enable();
		}

		if( _moveControlUpdate != null )
			_moveControlUpdate.Enable();

		if( _attackControlUpdate != null )
			_attackControlUpdate.Enable();
	}

	public override void Disable()
	{
		base.Disable();

		if( _moveControlUpdate != null )
			_moveControlUpdate.Disable();

		if( _attackControlUpdate != null )
			_attackControlUpdate.Disable();
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

	public M AddMove<M>(string name) where M : MZMove, new()
	{
		M move = MZMove.Create<M>( name, controlDelegate );
		movesList.Add( move );

		return move;
	}

	public A AddAttack<A>() where A : MZAttack, new()
	{
		A attack = MZAttack.Create<A>( "attack", controlDelegate );
		attacksList.Add( attack );

		return attack;
	}

	protected override void UpdateWhenActive()
	{
		if( _moveControlUpdate != null )
			_moveControlUpdate.Update();

		if( _attackControlUpdate != null )
			_attackControlUpdate.Update();
	}
}