using UnityEngine;
using System.Collections;

public class Formation_S_Rocket : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 1.0f;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 3 + ( ( rank - 1 )/2 )*2;
		}
	}

	protected override void InitValues()
	{
		enemyName = "EnemyM002";
		enemyCreateTimeInterval = 0.2f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		if( UpdateAndCheckTimeToCreateEnemy() )
		{
			AddNewEnemy( false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 5;
		enemy.partsByNameDictionary[ "MainBody" ].faceTo = MZFaceTo.Create<MZFaceTo_MovingDirection>( null );

		AddShowMode( enemy );
		AddRocketMode( enemy );
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		float prepharePosOffset = 150;
		float sideOffset = 50;
		float posIntervl = GetPositionInterval( sideOffset );

		float x;
		float y;

		switch( positionType )
		{
			case PositionType.Mid:
				x = MZGameSetting.ENEMY_BOUNDLE_LEFT + sideOffset + ( posIntervl/2 ) + posIntervl*currentEnemyCreatedCount;
				y = MZGameSetting.ENEMY_BOUNDLE_TOP + prepharePosOffset;
				break;

			case PositionType.Left:
				x = MZGameSetting.ENEMY_BOUNDLE_LEFT - 150;
				y = MZGameSetting.ENEMY_BOUNDLE_TOP + sideOffset - ( ( posIntervl/2 ) + posIntervl*currentEnemyCreatedCount );
				break;

			case PositionType.Right:
				x = MZGameSetting.ENEMY_BOUNDLE_RIGHT + 150;
				y = MZGameSetting.ENEMY_BOUNDLE_TOP + sideOffset - ( ( posIntervl/2 ) + posIntervl*currentEnemyCreatedCount );
				break;

			default:
				MZDebug.AssertFalse( "not supprt: " + positionType.ToString() );
				return Vector2.zero;
		}

		return new Vector2( x, y );
	}

	void AddShowMode(MZEnemy enemy)
	{
		float duration = maxEnemyCreatedNumber*enemyCreateTimeInterval + 0.15f;

		MZMode mode = enemy.AddMode( "show" );
		mode.duration = duration;

		MZMove_LinearTo move = mode.AddMove<MZMove_LinearTo>( "move" );
		move.useRelativePosition = true;
		move.destinationPosition = GetShowMoveDestPosition();
		move.totalTime = duration/3;
		move.duration = duration;
	}

	void AddRocketMode(MZEnemy enemy)
	{
		MZMode mode = enemy.AddMode( "rocket" );

		MZMove_ToTarget move = mode.AddMove<MZMove_ToTarget>( "move" );
		move.velocity = 600;
		move.target.calcuteEveryTime = false;

		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack_OddWay attack = mainPartControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 1;
		attack.initVelocity = 300;
		attack.additionalVelocity = 50;
		attack.bulletName = "EBDonuts";
		attack.colddown = 0.05f;
		attack.duration = 0.3f;
		attack.targetHelp = new MZTargetHelp_Target();
		attack.targetHelp.calcuteEveryTime = false;

		mainPartControl.AddAttack<MZAttack_Idle>().duration = -1;
	}

	Vector2 GetShowMoveDestPosition()
	{
		switch( positionType )
		{
			case PositionType.Mid:
				return new Vector2( 0, -200 );

			case PositionType.Left:
				return new Vector2( 200, 0 );

			case PositionType.Right:
				return new Vector2( -200, 0 );

			default:
				MZDebug.AssertFalse( "not supprt: " + positionType.ToString() );
				return Vector2.zero;
		}
	}

	float GetPositionInterval(float sideOffset)
	{
		if( positionType == MZFormation.PositionType.Mid )
			return ( ( MZGameSetting.ENEMY_BOUNDLE_WIDTH - sideOffset*2 )/maxEnemyCreatedNumber );

		if( positionType == MZFormation.PositionType.Left || positionType == MZFormation.PositionType.Right )
		{
			float enemyLine = MZGameSetting.ENEMY_BOUNDLE_HEIGHT/3*2;
			return enemyLine/maxEnemyCreatedNumber;
		}

		MZDebug.AssertFalse( "not support type" + positionType.ToString() );
		return 0;
	}
}
