using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_M_Waterfall : MZFormation
{
	public override float disableNextFormationTime
	{ get { return 3.0f; } }

	protected override int maxEnemyCreatedNumber
	{ get { return ( rank >= 5 )? 3 : 2; } }

	protected override void InitValues()
	{
		enemyCreateTimeInterval = 3.0f;
		enemyName = "EnemyM001";
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 25;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove_LinearBy move = mode.AddMove<MZMove_LinearBy>( "move" );
		move.velocity = 100;
		move.isRunOnce = true;
		move.direction = ( positionType == MZFormation.PositionType.Left )? 0 : 180;

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		int mainWay = ( rank < 5 )? 1 : 3;
		AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270, mainWay );

		if( rank >= 7 )
		{
			int extraWay = 1 + ( rank - 7 )/2;

			AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270 + 40, extraWay );
			AddCrossWayAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, 270 - 40, extraWay );
		}
	}

	protected override Vector2 GetEnemyStartPosition()
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

	protected override void UpdateWhenActive()
	{
		if( currentEnemyCreatedCount >= maxEnemyCreatedNumber )
			return;

		if( UpdateAndCheckTimeToCreateEnemy() )
			AddNewEnemy( false );
	}

	void AddCrossWayAttack(MZCharacterPart part, MZMode mode, float degree, int way)
	{
		MZPartControl partControl = new MZPartControl( part );
		mode.AddPartControlUpdater().Add( partControl );

		float rankIdle = ( ( rank < 3 )? 0.5f : 0 );

		MZAttack_OddWay odd = partControl.AddAttack<MZAttack_OddWay>();
		odd.numberOfWays = way;
		odd.colddown = 0.1f;
		odd.duration = 1.0f - rankIdle + ( ( rank >= 7 )? -0.6f : 0 );
		odd.bulletName = "EBDonuts";
		odd.initVelocity = 300;
		odd.additionalVelocity = 80;
		odd.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( odd.targetHelp as MZTargetHelp_AssignDirection ).direction = degree;
		float interval = 50;
		odd.offsetPositionsList.Add( new Vector2( 0, -interval ) );
		odd.offsetPositionsList.Add( new Vector2( -interval, 0 ) );
		odd.offsetPositionsList.Add( new Vector2( interval, 0 ) );

		MZAttack_Idle idle = partControl.AddAttack<MZAttack_Idle>();
		idle.duration = 0.6f + rankIdle;
	}
}
