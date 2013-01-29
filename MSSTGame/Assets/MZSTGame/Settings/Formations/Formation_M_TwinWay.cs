using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation_M_TwinWay : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 3;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 5;
		}
	}

	int _row;
	bool _isLeftRoRight;

	protected override void InitValues()
	{
		enemyName = "EnemyM003";
		enemyCreateTimeInterval = 1.8f;

		_row = 3;
		_isLeftRoRight = ( MZMath.RandomFromRange( 0, 1 ) == 0 );
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		if( UpdateAndCheckTimeToCreateEnemy() == false )
			return;

		AddNewEnemy( false );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 20;

		float commonMoveVelocity = 80;

		MZMode mode = enemy.AddMode( "m1" );
		mode.duration = 3.5f;

		MZMove_LinearBy move1 = mode.AddMove<MZMove_LinearBy>( "mm" );
		move1.direction = 270;
		move1.velocity = commonMoveVelocity;

		MZPartControl twinLeftPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( twinLeftPartControl );
		SetTwinWay( twinLeftPartControl, new Vector2( -45, -80 ), true );

		MZPartControl twinRightPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( twinRightPartControl );
		SetTwinWay( twinRightPartControl, new Vector2( 45, -80 ), false );

		if( rank >= 5 )
		{
			MZPartControl fanWayLeftPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
			mode.AddPartControlUpdater().Add( fanWayLeftPartControl );
			SetFanWay( fanWayLeftPartControl, new Vector2( -45, 80 ), 1.2f );

			MZPartControl fanWayRightPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
			mode.AddPartControlUpdater().Add( fanWayRightPartControl );
			SetFanWay( fanWayRightPartControl, new Vector2( 45, 80 ), 1.4f );
		}

		MZMode modeReset = enemy.AddMode( "m2" );
		modeReset.duration = 0.5f;

		MZMove_LinearBy move2 = modeReset.AddMove<MZMove_LinearBy>( "mm" );
		move2.direction = 270;
		move2.velocity = commonMoveVelocity;
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		float sideOffset = 20;
		float posInterval = ( MZGameSetting.ENEMY_BOUNDLE_WIDTH - sideOffset*2 )/_row;

		int dir = ( currentEnemyCreatedCount/_row > 0 )? -1 : 1;
		int posCount = currentEnemyCreatedCount%_row;

		Vector2 initPos = GetRowInitPosition( dir, sideOffset, posInterval );

		return initPos + new Vector2( posInterval*posCount*dir*( ( _isLeftRoRight )? 1 : -1 ), 0 );
	}

	Vector2 GetRowInitPosition(int dirCode, float sideOffset, float posInterval)
	{
		float x;

		if( _isLeftRoRight )
		{
			x = ( dirCode == 1 )?
			( MZGameSetting.ENEMY_BOUNDLE_LEFT + ( sideOffset + posInterval/2.0f ) ) :
				( MZGameSetting.ENEMY_BOUNDLE_RIGHT - ( sideOffset + posInterval ) );
		}
		else
		{
			x = ( dirCode == 1 )?
			( MZGameSetting.ENEMY_BOUNDLE_RIGHT - ( sideOffset + posInterval/2.0f ) ) :
				( MZGameSetting.ENEMY_BOUNDLE_LEFT + ( sideOffset + posInterval ) );
		}

		float y = MZGameSetting.ENEMY_BOUNDLE_TOP + 120;

		return new Vector2( x, y );
	}

	void SetTwinWay(MZPartControl partControl, Vector2 position, bool isLeft)
	{
		MZAttack_OddWay twin = partControl.AddAttack<MZAttack_OddWay>();
		twin.initVelocity = 450;
		twin.numberOfWays = ( rank <= 4 )? 1 : ( rank >= 8 )? 3 : 2;

		if( rank >= 5 )
		{
			float interval = ( rank >= 8 )? 20 : 10;
			twin.offsetPositionsList.Add( position + new Vector2( interval*( ( isLeft )? -1 : 1 ), 0 ) );
			twin.offsetPositionsList.Add( position + new Vector2( interval*( ( isLeft )? 1 : -1 ), -20 ) );
		}

		if( rank >= 8 || rank <= 4 )
		{
			twin.offsetPositionsList.Add( position + new Vector2( 0, -10 ) );
		}

		twin.colddown = 0.2f;
		twin.duration = 1.0f - ( ( rank <= 4 )? 0.5f : 0 );
		twin.bulletName = "EBBee";
		twin.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( twin.targetHelp as MZTargetHelp_AssignDirection ).direction = 270;

		partControl.AddAttack<MZAttack_Idle>().duration = 3.0f;
	}

	void SetFanWay(MZPartControl partControl, Vector2 position, float idleTime)
	{
		partControl.AddAttack<MZAttack_Idle>().duration = idleTime;

		MZAttack_EvenWay even = partControl.AddAttack<MZAttack_EvenWay>();
		even.numberOfWays = 6;
		even.intervalDegrees = 12;
		even.initVelocity = 400;
		even.offsetPositionsList.Add( position );
		even.colddown = 100.0f;
		even.duration = 0.1f;
		even.bulletName = "EBDonuts";
		even.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( even.targetHelp as MZTargetHelp_AssignDirection ).direction = 270;

		partControl.AddAttack<MZAttack_Idle>().duration = -1;
	}
}