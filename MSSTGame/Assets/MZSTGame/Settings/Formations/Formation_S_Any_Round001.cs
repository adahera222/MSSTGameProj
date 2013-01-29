using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_S_Any_Round001 : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 5;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 4 + ( ( rank - 1 )/2 )*2;
		}
	}

	int _constructCode;
	int _angularVelocityDirection;
	float _intervalDegrees;
	float _distance;

	protected override void InitValues()
	{
		enemyName = "EnemySYellow";

		_constructCode = MZMath.RandomFromRange( 0, 2 );
		_angularVelocityDirection = ( MZMath.RandomFromRange( 0, 1 ) == 0 )? 1 : -1;
		_intervalDegrees = 360/maxEnemyCreatedNumber;
		_distance = 700;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		for( int i = 0; i < maxEnemyCreatedNumber; i++ )
		{
			AddNewEnemy( false );
		}
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		return MZMath.UnitVectorFromDegrees( _intervalDegrees*currentEnemyCreatedCount )*_distance;
	}

	protected override void UpdateWhenActive()
	{

	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 10;
		enemy.enableRemoveTime = 12;

		MZMode mode = enemy.AddMode( "mode" );

		switch( _constructCode )
		{
			case 0:
				SetType1Move( mode, enemy );
				break;

			case 1:
				SetType2Move( mode, enemy );
				break;

			case 2:
				SetType3Move( mode, enemy );
				break;

			default:
				MZDebug.AssertFalse( "not supprt code=" + _constructCode.ToString() );
				break;
		}

		// main attack
		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );
		mainPartControl.getFaceTo = new MZPartControl.GetFaceTo( testFaceTo );

		MZAttack_Idle attackIdle = mainPartControl.AddAttack<MZAttack_Idle>();
		attackIdle.duration = 0.8f;
		attackIdle.isRunOnce = true;

		MZAttack_OddWay attack = mainPartControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 1;
		attack.initVelocity = 200;
		attack.additionalVelocity = 50;
		attack.bulletName = "EBBee2";
		attack.colddown = 0.05f;
		attack.duration = 0.15f;
		attack.targetHelp = new MZTargetHelp_AssignPosition();
		( attack.targetHelp as MZTargetHelp_AssignPosition ).assignPosition = Vector2.zero;

		MZAttack_Idle attackIdle2 = mainPartControl.AddAttack<MZAttack_Idle>();
		attackIdle2.duration = 1.8f;
	}

	void SetType1Move(MZMode mode, MZEnemy enemy)
	{
		MZMove_LinearBy show = mode.AddMove<MZMove_LinearBy>( "l" );
		show.direction = MZMath.DegreesFromXAxisToVector( MZMath.UnitVectorFromP1ToP2( enemy.position, Vector2.zero ) );
		show.duration = 0.6f;
		show.velocity = 500;

		MZMove_Rotation rotShow = mode.AddMove< MZMove_Rotation>( "r1" );
		rotShow.angularVelocity = 50*_angularVelocityDirection;
		rotShow.variationOfRadians = -300;
		rotShow.radiansLimited = 400;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 10.0f;

		MZMove_Rotation rotOut = mode.AddMove< MZMove_Rotation>( "r2" );
		rotOut.angularVelocity = 50*_angularVelocityDirection;
		rotOut.variationOfRadians = 300;
		rotOut.targetHelp.assignPosition = Vector2.zero;
		rotOut.duration = 10;
	}

	void SetType2Move(MZMode mode, MZEnemy enemy)
	{
		MZMove_Rotation rotShow = mode.AddMove<MZMove_Rotation>( "r" );
		rotShow.angularVelocity = 50*_angularVelocityDirection;
		rotShow.variationOfRadians = -100;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 20;
	}

	void SetType3Move(MZMode mode, MZEnemy enemy)
	{
		MZMove_Rotation rotShow = mode.AddMove<MZMove_Rotation>( "r" );
		rotShow.angularVelocity = 50*_angularVelocityDirection;
		rotShow.variationOfRadians = -300;
		rotShow.radiansLimited = 400;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 1.5f;

		MZMove_Idle moveIdle = mode.AddMove<MZMove_Idle>( "i" );
		moveIdle.duration = 5;

		MZMove_LinearTo moveOut = mode.AddMove<MZMove_LinearTo>( "l" );
		moveOut.destinationPosition = Vector2.zero;
		moveOut.totalTime = 0.5f;
		moveOut.notEndAtDestation = true;
	}

	MZFaceTo testFaceTo()
	{
		MZTargetHelp_AssignPosition target = MZTargetHelp.Create<MZTargetHelp_AssignPosition>();
		target.assignPosition = new Vector2( 0, 0 );
		MZFaceTo_Target faceTo = MZFaceTo.Create<MZFaceTo_Target>( null );
		faceTo.target = target;

		return faceTo;
	}
}