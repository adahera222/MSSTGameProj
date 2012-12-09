using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZBullet : MZCharacter, IMZMove
{
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
//		UpdateWithTarget();
	}

	void UpdateWithTarget()
	{
		MZCharacterType characterType = gameObject.GetComponent<MZCharacter>().characterType;

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
		MZCharacter selfCharacter = gameObject.GetComponent<MZCharacter>();

		MZCharactersManager manager = MZGameComponents.GetInstance().charactersManager;

//		List<GameObject> list = MZGameComponents.GetInstance().charactersManager.GetList( MZCharacterType.EnemyAir );

//		foreach( GameObject enemy in MZGameComponents.GetInstance().charactersManager.GetList( MZCharacterType.EnemyAir ) )
//		{
//			MZCharacter enemyCharacter = enemy.GetComponent<MZCharacter>();

//			if( selfCharacter.IsCollide( enemyCharacter ) )
//			{
//				// damge to enemy
//				selfCharacter.Disable();
//			}
//		}
	}

	void UpdateWithPlayer()
	{
//		MZCharacter playerCharacter = MZGameComponents.GetInstance().charactersManager.GetPlayer().GetComponent<MZCharacter>();
//		MZCharacter selfCharacter = gameObject.GetComponent<MZCharacter>();

//		if( selfCharacter.IsCollide( playerCharacter ) )
//		{
//			selfCharacter.Disable();
//		}
	}
}
