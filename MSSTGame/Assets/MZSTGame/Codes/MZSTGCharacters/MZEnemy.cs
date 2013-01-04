using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public MZFormation belongFormation = null;
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
			return _modeControlUpdate.controlsList;
		}
	}

	public int currentHealthPoint
	{ get { return _currentHealthPoint; } }

	//

	public override void Enable()
	{
		base.Enable();
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
		get { return ( _modeControlUpdate != null )? _modeControlUpdate.currentControl.currentMovingVector : new Vector2( 1, 0 ); }
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

	//

	protected override void InitMode()
	{
		base.InitMode();
		_modeControlUpdate = new MZControlUpdate<MZMode>();
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
		if( belongFormation != null )
			belongFormation.Remove( this );
		
		Disable();
	}
}