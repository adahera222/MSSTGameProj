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

	protected override int maxCreatedNumber
	{
		get
		{
			return 9;
		}
	}

	float _showTime = 1.0f;
	float _splitTime = 0.1f;
	Vector2 _startPosition;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_startPosition = GetStartPosition();


		for( int i = 0; i < maxCreatedNumber; i++ )
		{
			AddNewEnemy( MZCharacter.MZCharacterType.EnemyAir, "EnemyM002", false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.position = _startPosition;

		if( currentCreatedMemberCount == 0 )
		{
			SetMainEnemy( enemy );
		}
		else
		{
			SetSubEnemy( enemy );
		}
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

		partControl.AddAttack<MZAttack_Idle>().duration = _showTime - 0.1f;// _showTime + 0.5f; //

		MZAttack_OddWay splitOdd = partControl.AddAttack<MZAttack_OddWay>();
		splitOdd.bulletName = "EBDonuts";
		splitOdd.numberOfWays = 36;
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
		odd.colddown = 0.3f;
		odd.duration = 1;
		odd.initVelocity = 350;
		odd.numberOfWays = 1;
		odd.targetHelp = new MZTargetHelp_Target();
		odd.targetHelp.calcuteEveryTime = true;
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
		MZMove_LinearBy linearToSplit = mode.AddMove<MZMove_LinearBy>( "s" );
		linearToSplit.velocity = 1500;
		linearToSplit.duration = _splitTime;
		linearToSplit.direction = ( 360/8 )*currentCreatedMemberCount;
	}

	Vector2 GetStartPosition()
	{
		return new Vector2( MZMath.RandomFromRange( (int)MZGameSetting.ENEMY_BOUNDLE_LEFT + 100, (int)(MZGameSetting.ENEMY_BOUNDLE_RIGHT - 100 ) ), MZGameSetting.ENEMY_BOUNDLE_TOP + 120 );
	}
}
