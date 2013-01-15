using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZBullet : MZCharacter, IMZMove
{
	public int strength = 0;
	//
	bool _drawCollisionCheck = false;
	MZControlUpdate<MZMove> _moveControlUpdate = null;

	public override Vector2 currentMovingVector
	{
		get
		{
			return ( _moveControlUpdate != null )? _moveControlUpdate.currentControl.currentMovingVector : Vector2.zero;
		}
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

	public MZMove AddMove(string name, string typeString)
	{
		MZMove move = (MZMove)MZObjectHelp.CreateClass( "MZMove_" + typeString );
		move.name = name;
		move.controlDelegate = this;

		movesList.Add( move );

		return move;
	}

	public override void Enable()
	{
		base.Enable();

		if( _moveControlUpdate != null )
			_moveControlUpdate.Enable();
	}

	public override void Disable()
	{
		base.Disable();

		if( _moveControlUpdate != null )
			_moveControlUpdate.Disable();

		_moveControlUpdate = null;
	}

	public override void OnRemoving()
	{
		base.OnRemoving();
		_moveControlUpdate = null;
	}

	//

	public override void Clear()
	{
		base.Clear();
		enableRemoveTime = 0.3f;
		strength = 0;
	}

	protected override void UpdateWhenActive()
	{
		base.UpdateWhenActive();

		if( _moveControlUpdate != null )
			_moveControlUpdate.Update();
	}

	public override bool IsCollide(MZCharacter other)
	{
		_drawCollisionCheck = true;
		return base.IsCollide( other );
	}

	void OnDrawGizmos()
	{
		if( _drawCollisionCheck == false || MZGameSetting.SHOW_BULLET_ON_COLLISION_CHECK == false || MZGameSetting.SHOW_GIZMOS == false )
			return;

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube( position, new Vector3( 30, 30, 30 ) );

		_drawCollisionCheck = false;
	}
}