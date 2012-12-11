using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZBullet : MZCharacter, IMZMove
{
	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;

	public Vector2 currentMovingVector
	{
		get
		{
			return ( _moveControlUpdate != null )? _moveControlUpdate.currentControl.currentMovingVector : Vector2.zero;
		}
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

		UpdateWithTarget();
	}

	void UpdateWithTarget()
	{
		switch( characterType )
		{
			case MZCharacterType.EnemyBullet:
				UpdateWithPlayer();
				break;

			case MZCharacterType.PlayerBullet:
				UpdateWithEnemies();
				break;
		}
	}

	void UpdateWithEnemies()
	{
		foreach( MZEnemy enemyCharacter in MZCharacterObjectsPoolManager.GetInstance().GetCharacterList( MZCharacterType.EnemyAir ) )
		{
			if( enemyCharacter.isActive == false )
				continue;

			if( IsCollide( enemyCharacter ) )
			{
				enemyCharacter.TakenDamage( 1 );
				Disable();
			}
		}
	}

	void UpdateWithPlayer()
	{
		if( MZGameComponents.GetInstance().charactersManager.playerCharacter == null )
			return;

		// test
		if( !( MZGameComponents.GetInstance().charactersManager.enemyBulletCanUpdateStart <= poolIndex && poolIndex <=
			MZGameComponents.GetInstance().charactersManager.enemyBulletCanUpdateStart ) )
			return;

		MZDebug.Log( "i am " + poolIndex.ToString() );

		MZCharacter playerCharacter = MZGameComponents.GetInstance().charactersManager.playerCharacter;

		if( IsCollide( playerCharacter ) )
		{
			Disable();
		}
	}
}
