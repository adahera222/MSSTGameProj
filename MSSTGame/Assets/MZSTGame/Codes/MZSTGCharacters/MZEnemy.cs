using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public int _healthPoint = 1;
	MZControlUpdate<MZMode> _modeControlUpdate = new MZControlUpdate<MZMode>();

	#region IMZMode implementation

	public IMZMove moveControlTarget
	{ get { return this; } }

	#endregion

	public int healthPoint
	{
		get{ return _healthPoint; }
	}

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
		_healthPoint = 1;
		enableRemoveTime = 10.0f;
		MZGameComponents.GetInstance().charactersManager.enemyNumber++;
	}

	public override void Disable()
	{
		base.Disable();

		MZGameComponents.GetInstance().charactersManager.enemyNumber--;
	}

	public override void Clear()
	{
		base.Clear();
		_modeControlUpdate = null;
	}

	public override Vector2 currentMovingVector
	{
		get{ return _modeControlUpdate.currentControl.currentMovingVector; }
	}

	public MZMode AddMode(string name)
	{
		MZMode mode = new MZMode();
		mode.name = name;
		mode.controlTarget = this;

		modesList.Add( mode );

		return mode;
	}

	public void TakenDamage(int damage)
	{
		_healthPoint -= damage;
	}

	protected override void Update()
	{
		base.Update();

		_modeControlUpdate.Update();

		if( _healthPoint <= 0 )
			Disable();
	}
}