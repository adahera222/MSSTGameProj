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
//		MZCharacter selfCharacter = gameObject.GetComponent<MZCharacter>();

		MZCharactersManager charactersManager = MZGameComponents.GetInstance().charactersManager;

//		List<GameObject> list = charactersManager.GetList( MZCharacterType.EnemyAir );

		foreach( GameObject enemy in MZOTSpritesPoolManager.GetInstance().GetSpritesList( MZCharacterType.EnemyAir ) )
		{
//			MZCharacter enemyCharacter = enemy.GetComponent<MZCharacter>();

//			if( selfCharacter.IsCollide( enemyCharacter ) )
//			{
//				// damge to enemy
//				selfCharacter.Disable();
//			}
		}
	}

	void UpdateWithPlayer()
	{
		MZCharacter playerCharacter = MZGameComponents.GetInstance().charactersManager.playerCharacter; //.GetPlayer().GetComponent<MZCharacter>();

		if( IsCollide( playerCharacter ) )
		{
			Disable();
		}
	}
}
