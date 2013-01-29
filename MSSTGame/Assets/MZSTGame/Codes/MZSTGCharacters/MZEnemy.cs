using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public MZFormation belongFormation = null;
	public int healthPointOnEditor;
	public int healthPoint = 10;
	//
	int _currentHealthPoint = 1;
	MZControlUpdate<MZMode> _modeControlUpdate = null;

	#region IMZMode implementation

	public IMZMove moveDelegate
	{ get { return this; } }

	#endregion

	public List<MZMode> modesList
	{
		get
		{
			MZDebug.Assert( _modeControlUpdate != null, "_modeControlUpdate is null, please call CreateNewModes()" );
			return _modeControlUpdate.controlsList;
		}
	}

	public int currentHealthPoint
	{ get { return _currentHealthPoint; } }

	//

	public override void Enable()
	{
		base.Enable();

		if( _modeControlUpdate != null )
			_modeControlUpdate.Enable();
	}

	public override void Disable()
	{
		base.Disable();
		_modeControlUpdate.Disable();

		if( belongFormation != null )
			belongFormation.Remove( this );
	}

	public override void OnRemoving()
	{
		base.OnRemoving();
	}

	public override float currentMovingDirection
	{
		get
		{
			return ( _modeControlUpdate != null && _modeControlUpdate.currentControl != null )? _modeControlUpdate.currentControl.currentMovingDirection : 0;
		}
	}

	public MZMode AddMode(string name)
	{
		MZMode mode = new MZMode();
		mode.name = name;
		mode.controlDelegate = this;

		modesList.Add( mode );

		return mode;
	}

	public void TakenDamage(int damage)
	{
		_currentHealthPoint -= damage;
		healthPointOnEditor = _currentHealthPoint;
	}

	//

	public override void Clear()
	{
		base.Clear();
		enableRemoveTime = 3.0f;
		_modeControlUpdate = new MZControlUpdate<MZMode>();
	}

	//

	protected override void InitValues()
	{
		base.InitValues();
		_currentHealthPoint = healthPoint;
	}

	protected override void UpdateWhenActive()
	{
		base.UpdateWhenActive();

		if( _modeControlUpdate != null )
			_modeControlUpdate.Update();
		
		if( _currentHealthPoint <= 0 )
			DieAction();
	}

	protected void DieAction()
	{
		MZDebug.Assert( belongFormation != null, "belongFormation is null" );
		belongFormation.NotifyDefeated( this );

		Disable();
	}
}