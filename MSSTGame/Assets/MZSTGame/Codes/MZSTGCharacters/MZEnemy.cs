using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public int healthPoint = 1;
	MZControlUpdate<MZMode> _modeControlUpdate = new MZControlUpdate<MZMode>();

	#region IMZMode implementation

	public IMZMove moveControlTarget
	{ get { return this; } }

	#endregion

	public List<MZMode> modesList
	{
		get
		{
			if( _modeControlUpdate == null )
				_modeControlUpdate = new MZControlUpdate<MZMode>();

			return _modeControlUpdate.controlsList;
		}
	}

	public override void Enable()
	{
		base.Enable();
		healthPoint = 1;
		enableRemoveTime = 10.0f;
	}

	public override void Clear()
	{
		base.Clear();
		_modeControlUpdate = null;
	}

	public Vector2 currentMovingVector
	{
		get{ return _modeControlUpdate.currentControl.currentMovingVector;}
	}

	public MZMode AddMode(string name)
	{
		MZMode mode = new MZMode();
		mode.name = name;
		mode.controlTarget = this;

		modesList.Add( mode );

		return mode;
	}

//	protected override void Start()
//	{
//		base.Start();
//
//		enableRemoveTime = 10.0f;
//	}

	protected override void Update()
	{
		base.Update();

		_modeControlUpdate.Update();

		if( healthPoint <= 0 )
			Disable();
	}
}