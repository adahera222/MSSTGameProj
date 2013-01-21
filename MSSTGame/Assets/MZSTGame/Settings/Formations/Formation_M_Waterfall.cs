using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_M_Waterfall : MZFormation
{
	public override float disableNextFormationTime
	{ get { return 3.0f; } }

	protected override int maxCreatedNumber
	{ get { return 3; } }

	float _createInterval = 3.0f;
	float _createtimeCount;
	Vector2 _initPosition;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		
		_initPosition = GetInitPosition( positionType );
		_createtimeCount = 0;
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 25;
		enemy.position = _initPosition;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove_LinearBy move = mode.AddMove<MZMove_LinearBy>( "move" );
		move.velocity = 100;
		move.isRunOnce = true;
		move.direction = ( positionType == MZFormation.PositionType.Left )? 0 : 180;

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270 );
//		AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270 + 30 );
//		AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270 - 30 );
	}

	protected override void UpdateWhenActive()
	{
		if( currentCreatedMemberCount >= maxCreatedNumber )
			return;

		_createtimeCount -= MZTime.deltaTime;

		if( _createtimeCount < 0 )
		{
			AddNewEnemy( MZCharacterType.EnemyAir, "EnemyM001", false );
			_createtimeCount += _createInterval;
		}
	}

	void AddCrossWayAttack(MZCharacterPart part, MZMode mode, float degree)
	{
		MZPartControl partControl = new MZPartControl( part );
		mode.AddPartControlUpdater().Add( partControl );

		MZAttack_OddWay odd = partControl.AddAttack<MZAttack_OddWay>();
		odd.numberOfWays = 3;
		odd.colddown = 0.1f;
		odd.duration = 1.0f;
		odd.bulletName = "EBDonuts";
		odd.initVelocity = 300;
		odd.additionalVelocity = 50;
		odd.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( odd.targetHelp as MZTargetHelp_AssignDirection ).direction = degree;
		float interval = 50;
		odd.offsetPosition.Add( new Vector2( 0, -interval ) );
		odd.offsetPosition.Add( new Vector2( -interval, 0 ) );
		odd.offsetPosition.Add( new Vector2( interval, 0 ) );

		MZAttack_Idle idle = partControl.AddAttack<MZAttack_Idle>();
		idle.duration = 0.6f;
	}

	Vector2 GetInitPosition(PositionType positionType)
	{
		float y = MZGameSetting.ENEMY_BOUNDLE_TOP - 120;
		float left = MZGameSetting.ENEMY_BOUNDLE_LEFT - 50;
		float right = MZGameSetting.ENEMY_BOUNDLE_RIGHT + 50;

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector2( left, y );

			case PositionType.Right:
				return new Vector2( right, y );

			default:
				MZDebug.AssertFalse( "not supprt" );
				return new Vector2( 0, y );
		}

	}
}
