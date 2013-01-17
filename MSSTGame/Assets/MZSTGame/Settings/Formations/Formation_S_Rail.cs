using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_S_Rail : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 3;
		}
	}

	protected override int maxCreatedNumber
	{
		get
		{
			return 3;
		}
	}

	//

	float _createInterval;
	float _createTimeCount;
	Vector2 _initPosition;
	float _initDirection;
	float _destDirection;
	MZMove.RotationType rotationType;
	string enemyName = "EnemySYellow";

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_createInterval = 0.4f;
		_createTimeCount = 0;

		SetInitByType();
	}

	protected override void UpdateWhenActive()
	{
		_createTimeCount -= MZTime.deltaTime;

		if( _createTimeCount < 0 )
		{
			_createTimeCount += _createInterval;

			AddNewEnemy( MZCharacterType.EnemyAir, enemyName, false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 1;
		enemy.position = _initPosition;

		MZMode mode = enemy.AddMode( "mode" );

		// move
		float velocity = 400;

		MZMove moveLinear1 = mode.AddMove( "l1", MZMove.Type.LinearBy );
		moveLinear1.direction = _initDirection;
		moveLinear1.velocity = velocity;
		moveLinear1.duration = 1.5f;

		MZMove_DegreesTo moveTurn = mode.AddMove( "t", MZMove.Type.DegreesTo ) as MZMove_DegreesTo;
		moveTurn.direction = _initDirection;
		moveTurn.destinationDegrees = _destDirection;
		moveTurn.rotationType = rotationType;
		moveTurn.totalTime = 0.5f;
		moveTurn.duration = 100;
		moveTurn.velocity = velocity;

		// main attack
		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack attack = mainPartControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 1;
		attack.initVelocity = 300;
		attack.additionalVelocity = 200;
		attack.bulletName = "EBDonuts";
		attack.colddown = 0.1f;
		attack.duration = 0.3f;
		attack.targetHelp = new MZTargetHelp_Target();

		MZAttack attackIdle = mainPartControl.AddAttack( MZAttack.Type.Idle );
		attackIdle.duration = moveLinear1.duration - 0.35f;
	}

	void SetInitByType()
	{
		float sideX = 320;
		float offsetX = 30;
		float sideY = 600;

		switch( positionType )
		{
			case PositionType.Left:
				_initPosition = new Vector2( -sideX - offsetX, sideY );
				_initDirection = -45;
				_destDirection = 360 - 135;
				rotationType = MZMove.RotationType.CW;
				break;

			case PositionType.Right:
				_initPosition = new Vector2( sideX + offsetX, sideY );
				_initDirection = 360 - 135;
				_destDirection = -45;
				rotationType = MZMove.RotationType.CCW;
				break;

			case PositionType.Mid:
			default:
				MZDebug.AssertFalse( "not support" );
				break;
		}
	}
}