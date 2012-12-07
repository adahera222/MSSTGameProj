using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZBullet : MZCharacter, IMZMove
{
	public Vector2 movingVector;
	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;

	public Vector2 currentMovingVector
	{
		get{ return _moveControlUpdate.currentControl.currentMovingVector; }
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
		move.controlTarget = this;

		movesList.Add( move );

		return move;
	}

	protected override void Start()
	{
		base.Start();

		enableRemoveTime = 0.3f;
	}

	protected override void Update()
	{
		base.Update();

		_moveControlUpdate.Update();
	}
}
