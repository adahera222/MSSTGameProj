using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public MZFormation belongFormation = null;
	public MZMode mode;
	public int healthPoint = 10;
	//
	int _currentHealthPoint = 1;
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
		_modeControlUpdate.ResetAll();
		_currentHealthPoint = healthPoint;
		enableRemoveTime = 3.0f;
	}

	public override void Disable()
	{
		base.Disable();
	}

	public override void OnRemoving()
	{
		base.OnRemoving();
	}

	public override Vector2 currentMovingVector
	{
		get
		{
			return _modeControlUpdate.currentControl.currentMovingVector;
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
	}

	protected override void UpdateWhenActive()
	{
		base.UpdateWhenActive();
		
		if( _modeControlUpdate != null )
			_modeControlUpdate.Update();
		
		if( _currentHealthPoint <= 0 )
			OnDie();
	}

	protected void OnDie()
	{
		if( belongFormation != null )
			belongFormation.Remove( this );
		
		Disable();
	}
}