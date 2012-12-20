using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMZMode : IMZControl
{
	IMZMove moveControlTarget
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
	public new IMZMode controlTarget = null;
	//
	MZControlUpdate<MZMove> _moveControlUpdate = new MZControlUpdate<MZMove>();
	List<MZControlUpdate<MZPartControl>> _multiPartControlUpdate = new List<MZControlUpdate<MZPartControl>>();

	public override void Reset()
	{
		base.Reset();

		if( _moveControlUpdate != null )
			_moveControlUpdate.ResetAll();

		if( _multiPartControlUpdate != null )
		{
			foreach( MZControlUpdate<MZPartControl> pcUpdate in _multiPartControlUpdate )
				pcUpdate.ResetAll();
		}
	}

	public Vector2 currentMovingVector
	{
		get{ return _moveControlUpdate.currentControl.currentMovingVector; }
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
		MZMove move = MZMove.Create( type, name, controlTarget.moveControlTarget );
		movesList.Add( move );

		return move;
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