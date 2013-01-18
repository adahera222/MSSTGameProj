using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZMode : IMZControl
{
	IMZMove moveDelegate
	{
		get;
	}

	Dictionary<string, MZCharacterPart> partsByNameDictionary
	{
		get;
	}
}

public class MZMode : MZControlBase
{
	public new IMZMode controlDelegate = null;
	//
	MZControlUpdate<MZMove> _moveControlUpdate = new MZControlUpdate<MZMove>();
	List<MZControlUpdate<MZPartControl>> _multiPartControlUpdate = new List<MZControlUpdate<MZPartControl>>();

	public override void Enable()
	{
		base.Enable();
		_moveControlUpdate.Enable();
		foreach( MZControlUpdate<MZPartControl> pc in _multiPartControlUpdate )
			pc.Enable();
	}

	public override void Disable()
	{
		base.Disable();
		_moveControlUpdate.Disable();
		foreach( MZControlUpdate<MZPartControl> pc in _multiPartControlUpdate )
			pc.Disable();
	}

	public Vector2 currentMovingVector
	{
		get
		{ return ( _moveControlUpdate != null && _moveControlUpdate.currentControl != null )? _moveControlUpdate.currentControl.currentMovingVector : new Vector2( 0, 0 ); }
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

	public MZMove AddMove(string name, MZMove.Type type)
	{
		MZMove move = MZMove.Create( type, name, controlDelegate.moveDelegate );
		movesList.Add( move );

		return move;
	}

	public M AddMove<M>(string name) where M : MZMove, new()
	{
		M move = new M();
		( (MZMove)move ).name = name;
		( (MZMove)move ).controlDelegate = controlDelegate.moveDelegate;
		movesList.Add( move );

		return move;
	}

	public MZMove AddMove(MZMove.Type type)
	{
		return AddMove( "", type );
	}

	public MZControlUpdate<MZPartControl> AddPartControlUpdater()
	{
		if( _multiPartControlUpdate == null )
			_multiPartControlUpdate = new List<MZControlUpdate<MZPartControl>>();

		MZControlUpdate<MZPartControl> partControlUpdate = new MZControlUpdate<MZPartControl>();
		_multiPartControlUpdate.Add( partControlUpdate );

		return partControlUpdate;
	}

	protected override void UpdateWhenActive()
	{
		if( _moveControlUpdate != null )
		{
			_moveControlUpdate.Update();
		}

		if( _multiPartControlUpdate != null )
		{
			foreach( MZControlUpdate<MZPartControl> partControlUpdate  in _multiPartControlUpdate )
				partControlUpdate.Update();
		}
	}
}