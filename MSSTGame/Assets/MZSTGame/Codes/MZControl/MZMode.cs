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
	MZControlUpdate<MZMove_Base> _moveControlUpdate = new MZControlUpdate<MZMove_Base>();

	List<MZControlUpdate<MZPartControl>> _multiPartControlUpdate = new List<MZControlUpdate<MZPartControl>>();
//	MZControlUpdate<MZPartControl> _partControlUpdate = new MZControlUpdate<MZPartControl>();

	public override void Reset()
	{
		base.Reset();

		if( _moveControlUpdate != null )
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

//	public MZPartControl AddPartControl(string partName)
//	{
//		MZDebug.Assert( controlTarget.partsByNameDictionary.ContainsKey( partName ), "part(" + partName + ") not found" );
//
//		MZPartControl partControl = new MZPartControl();
//		partControl.controlTarget = controlTarget.partsByNameDictionary[ partName ];
//
//		_partControlUpdate.Add( partControl );
//
//		MZDebug.Log( _partControlUpdate.controlsList.Count.ToString() );
//
//		return partControl;
//	}

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

//		if( _partControlUpdate != null )
//		{
//			_partControlUpdate.Update();
//		}

		if( _multiPartControlUpdate != null )
		{
			foreach( MZControlUpdate<MZPartControl> partControlUpdate  in _multiPartControlUpdate )
				partControlUpdate.Update();
		}
	}
}