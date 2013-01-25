using UnityEngine;
using System.Collections;

public class Formation_M_Split : MZFormation
{

	public override float disableNextFormationTime
	{
		get
		{
			return 3.0f;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 9;
		}
	}

	float _showTime = 1.5f;
	float _splitTime = 0.2f;
	Vector2 _startPoisiton;

	protected override void InitValues()
	{
		enemyName = "EnemyM002";

		int sideOffset = 150;

		int x = MZMath.RandomFromRange( (int)MZGameSetting.ENEMY_BOUNDLE_LEFT + sideOffset, (int)MZGameSetting.ENEMY_BOUNDLE_RIGHT - sideOffset );
		_startPoisiton = new Vector2( x, MZGameSetting.ENEMY_BOUNDLE_TOP + 120 );
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		for( int i = 0; i < maxEnemyCreatedNumber; i++ )
		{
			AddNewEnemy( false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		if( currentEnemyCreatedCount == 0 )
		{
			SetMainEnemy( enemy );
		}
		else
		{
			SetSubEnemy( enemy );
		}
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		return _startPoisiton;
	}

	protected override void UpdateWhenActive()
	{

	}

	void SetMainEnemy(MZEnemy enemy)
	{
		enemy.healthPoint = 30;

		MZMode mode = enemy.AddMode( "m" );

		AddCommonMove( mode, "common1" ).duration = _showTime;
		mode.AddMove<MZMove_Idle>( "idle" ).duration = _splitTime;
		AddCommonMove( mode, "common2" );

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		partControl.AddAttack<MZAttack_Idle>().duration = _showTime;// _showTime + 0.5f; //

		MZAttack_OddWay splitOdd = partControl.AddAttack<MZAttack_OddWay>();
		splitOdd.bulletName = "EBDonutsLarge";
		splitOdd.numberOfWays = 18;
		splitOdd.initVelocity = 500;
		splitOdd.colddown = 0.1f;
		splitOdd.duration = 0.1f;
		splitOdd.intervalDegrees = 360/splitOdd.numberOfWays;
		splitOdd.isRunOnce = true;

		partControl.AddAttack<MZAttack_Idle>().duration = 0.5f;

		MZAttack_OddWay odd = partControl.AddAttack<MZAttack_OddWay>();
		odd.bulletName = "EBDonuts";
		odd.numberOfWays = 16;
		odd.initVelocity = 300;
		odd.colddown = 0.25f;
		odd.duration = 1;
		odd.additionalVelocity = 50;
		odd.intervalDegrees = 360/odd.numberOfWays;
		odd.targetHelp = new MZTargetHelp_AssignDirection();
		( odd.targetHelp as MZTargetHelp_AssignDirection ).direction = 45;
	}

	void SetSubEnemy(MZEnemy enemy)
	{
		enemy.healthPoint = 8;

		MZMode mode = enemy.AddMode( "m" );

		AddCommonMove( mode, "common1" ).duration = _showTime;
		AddSplitMoveToSubEmemy( mode );
		AddCommonMove( mode, "common2" );

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		partControl.AddAttack<MZAttack_Idle>().duration = _showTime + 0.8f;

		MZAttack_OddWay odd = partControl.AddAttack<MZAttack_OddWay>();
		odd.bulletName = "EBBee";
		odd.colddown = 1.0f;
		odd.duration = 1.0f;
		odd.initVelocity = 400;
		odd.numberOfWays = 1;
		odd.targetHelp = new MZTargetHelp_Target();
	}

	MZMove AddCommonMove(MZMode mode, string name)
	{
		MZMove_LinearBy linearTo = mode.AddMove<MZMove_LinearBy>( name );
		linearTo.velocity = 100;
		linearTo.direction = 270;

		return linearTo;
	}

	void AddSplitMoveToSubEmemy(MZMode mode)
	{
		MZMove_LinearTo splitMove = mode.AddMove<MZMove_LinearTo>( "s" );
		float moveDegrees = ( 360/8 )*currentEnemyCreatedCount;
		float movement = 200;
		splitMove.useRelativePosition = true;
		splitMove.destinationPosition = MZMath.UnitVectorFromDegrees( moveDegrees )*movement;
		splitMove.totalTime = _splitTime - 0.1f;
		splitMove.duration = _splitTime;
	}
}
