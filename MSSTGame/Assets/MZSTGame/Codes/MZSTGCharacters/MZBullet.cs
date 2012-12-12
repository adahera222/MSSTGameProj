using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZBullet : MZCharacter, IMZMove
{
	public int strength = 0;
	//
	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;

	public override Vector2 currentMovingVector
	{
		get
		{ return ( _moveControlUpdate != null )? _moveControlUpdate.currentControl.currentMovingVector : Vector2.zero; }
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

	public override void Enable()
	{
		base.Enable();
		enableRemoveTime = 0.3f;

		if( characterType == MZCharacterType.PlayerBullet )
			MZGameComponents.GetInstance().charactersManager.playerBulletNumber++;
		else
			MZGameComponents.GetInstance().charactersManager.enemyBulletNumber++;
	}

	public override void Disable()
	{
		base.Disable();

		if( characterType == MZCharacterType.PlayerBullet )
			MZGameComponents.GetInstance().charactersManager.playerBulletNumber--;
		else
			MZGameComponents.GetInstance().charactersManager.enemyBulletNumber--;

		_moveControlUpdate = null;
		strength = 0;
	}

	public override void Clear()
	{
		base.Clear();
		_moveControlUpdate = null; // maybe GC? ... SUCK ...
	}

	protected override void Update()
	{
		base.Update();

		if( _moveControlUpdate != null )
			_moveControlUpdate.Update();
	}

	public override bool IsCollide(MZCharacter other)
	{
		_drawCollisionCheck = true;
		return base.IsCollide( other );
	}

	bool _drawCollisionCheck = false;

	void OnDrawGizmos()
	{
		if( _drawCollisionCheck == false || !MZGameSetting.SHOW_BULLET_ON_COLLISION_CHECK )
			return;

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube( position, new Vector3( 30, 30, 30 ) );

		_drawCollisionCheck = false;
	}
}