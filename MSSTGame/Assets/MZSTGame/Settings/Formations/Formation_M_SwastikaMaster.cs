using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation_M_SwastikaMaster : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 8;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 1;
		}
	}

	float _showTime;
	PositionType _innerPositionType;

	protected override void InitValues()
	{
		enemyName = "EnemyM001";

		int posChoice = MZMath.RandomFromRange( 0, 2 );
		_innerPositionType = ( posChoice == 0 )? PositionType.Mid : ( posChoice == 1 )? PositionType.Right : PositionType.Left;
		_showTime = 0.5f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		AddNewEnemy( false );
	}

	protected override void UpdateWhenActive()
	{

	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 80;

		MZMode mode = enemy.AddMode( "m" );

		SetMove( mode );

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );
		AddRingAttack( partControl );

		float degreesInterval = 2.0f;

		for( int centerDegrees = 0; centerDegrees < 360; centerDegrees += 90 )
		{
			SetNewPartToMultiVortexAttack( mode, enemy, centerDegrees + degreesInterval );
			SetNewPartToMultiVortexAttack( mode, enemy, centerDegrees - degreesInterval );
		}
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		switch( _innerPositionType )
		{
			case PositionType.Mid:
				return new Vector2( 0, MZGameSetting.ENEMY_BOUNDLE_TOP + 100 );

			case PositionType.Left:
				return new Vector2( MZGameSetting.ENEMY_BOUNDLE_LEFT - 100, MZGameSetting.ENEMY_BOUNDLE_TOP/2 + 100 );

			case PositionType.Right:
				return new Vector2( MZGameSetting.ENEMY_BOUNDLE_RIGHT + 100, MZGameSetting.ENEMY_BOUNDLE_TOP/2 + 100 );

			default:
				return Vector2.zero;
		}
	}

	void SetMove(MZMode mode)
	{
		MZMove_LinearTo to = mode.AddMove<MZMove_LinearTo>( "move" );
		to.destinationPosition = new Vector2( 0, MZGameSetting.ENEMY_BOUNDLE_TOP/3*2 + 40 );
		to.totalTime = 4.0f;
		to.isRunOnce = true;
		to.duration = 12;

		MZMove_LinearTo left = mode.AddMove<MZMove_LinearTo>( "left" );
		left.destinationPosition = new Vector2( 0, MZGameSetting.ENEMY_BOUNDLE_TOP + 300 );
		left.totalTime = 10.0f;
	}

	void AddRingAttack(MZPartControl partControl)
	{
		int ways = 15;
		float velocity = 300;
		float intervalDegrees = 360.0f/ways;

//		List<Vector2> outPosList = new List<Vector2>();
//		float offset = 30.0f;
//		for( int i = 0; i < ways; i++ )
//			outPosList.Add( MZMath.UnitVectorFromDegrees( 270 + i*intervalDegrees )*offset );

		MZAttack_Idle show = partControl.AddAttack<MZAttack_Idle>();
		show.duration = _showTime;
		show.isRunOnce = true;

		partControl.AddAttack<MZAttack_Idle>().duration = 2.0f;

		MZAttack_OddWay outRing = partControl.AddAttack<MZAttack_OddWay>();
		outRing.numberOfWays = ways;
		outRing.intervalDegrees = intervalDegrees;
		outRing.initVelocity = velocity;
		outRing.bulletName = "EBDonutsLarge";
		outRing.colddown = 10.0f;
		outRing.duration = 0.15f;
		outRing.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( outRing.targetHelp as MZTargetHelp_AssignDirection ).direction = 270;
//		outRing.offsetPosition = outPosList;

		MZAttack_OddWay innerRing = partControl.AddAttack<MZAttack_OddWay>();
		innerRing.numberOfWays = ways*2;
		innerRing.intervalDegrees = intervalDegrees/2.0f;
		innerRing.initVelocity = velocity;
		innerRing.bulletName = "EBDonuts";
		innerRing.colddown = 10.0f;
		innerRing.duration = 0.15f;
		innerRing.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( innerRing.targetHelp as MZTargetHelp_AssignDirection ).direction = 270 + intervalDegrees/4.0f;

		MZAttack_OddWay thirdRing = partControl.AddAttack<MZAttack_OddWay>();
		thirdRing.numberOfWays = ways;
		thirdRing.intervalDegrees = intervalDegrees;
		thirdRing.initVelocity = velocity;
		thirdRing.bulletName = "EBDonutsSmall";
		thirdRing.colddown = 10.0f;
		thirdRing.duration = 0.15f;
		thirdRing.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( thirdRing.targetHelp as MZTargetHelp_AssignDirection ).direction = 270 + intervalDegrees/2.0f;
	}

	void SetNewPartToMultiVortexAttack(MZMode mode, MZEnemy enemy, float degrees)
	{
		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );
		AddMultiVortex( partControl, degrees );
	}

	void AddMultiVortex(MZPartControl partControl, float degrees)
	{
		MZAttack_Idle show = partControl.AddAttack<MZAttack_Idle>();
		show.duration = _showTime;
		show.isRunOnce = true;

		float oneLoopTime = 3.0f;

		float intervalDegrees = 4.0f*( ( _innerPositionType == MZFormation.PositionType.Right )? -1 : 1 );

		MZAttack_Vortex vortex = partControl.AddAttack<MZAttack_Vortex>();
		vortex.bulletName = "EBBee";
		vortex.colddown = 0.2f;
		vortex.initVelocity = 300;
		vortex.intervalDegrees = intervalDegrees;
//		vortex.timePerWave = 0.66f;
//		vortex.resetTime = 0.33f;
		vortex.duration = oneLoopTime;
		vortex.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( vortex.targetHelp as MZTargetHelp_AssignDirection ).direction = degrees;

		partControl.AddAttack<MZAttack_Idle>().duration = 0.33f;

		MZAttack_Vortex vortex2 = partControl.AddAttack<MZAttack_Vortex>();
		vortex2.bulletName = "EBBee";
		vortex2.colddown = 0.2f;
		vortex2.initVelocity = 300;
		vortex2.intervalDegrees = -intervalDegrees;
		vortex2.duration = oneLoopTime;
		vortex2.targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>();
		( vortex2.targetHelp as MZTargetHelp_AssignDirection ).direction = degrees;

		partControl.AddAttack<MZAttack_Idle>().duration = 0.33f;
	}
}
