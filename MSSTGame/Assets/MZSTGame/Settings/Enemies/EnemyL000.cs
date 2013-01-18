using UnityEngine;
using System.Collections;

public class EnemyL000 : MZEnemy
{
	protected override void InitValues()
	{
		base.InitValues();
		healthPoint = 190;
	}

	public override void InitDefaultMode()
	{
		base.InitDefaultMode();

		MZMode mode1 = AddMode( "m1" );

		MZMove_LinearBy m1show = mode1.AddMove<MZMove_LinearBy>( "l" );
		m1show.direction = 270;
		m1show.velocity = 150;
		m1show.duration = 1.0f;
		m1show.isRunOnce = true;

		MZMove_Rotation m1rot = mode1.AddMove<MZMove_Rotation>( "r" );
		m1rot.targetHelp.assignType = MZTargetHelp_AssignPosition.AssignType.Relative;
		m1rot.targetHelp.assignPosition = new Vector2( 0, -100 );
		m1rot.angularVelocity = 30;
		m1rot.duration = -1;

//		MZMove m1Idle = mode1.AddMove( MZMove.Type.Linear );
//		m1Idle.initVelocity = 0;
//		m1Idle.duration = 60;

//		MZMove m1Left = mode1.AddMove( MZMove.Type.Linear );
//		m1Left.initMovingVector = new Vector2( 0, -1 );
//		m1Left.initVelocity = 100;
//		m1Left.duration = -1;

		// ToPosition:: time ver
//		MZMove mToPosByTime = mode1.AddMove( MZMove.Type.ToPosition );
//		mToPosByTime.isRunOnce = true;
//		( mToPosByTime as MZMove_ToPosition ).totalMoveTime = 3;
//		( mToPosByTime as MZMove_ToPosition ).destinationPosition = Vector2.zero;

		// ToPosition ... velocity ver
//		MZMove m1ToPosByVelocity = mode1.AddMove( MZMove.Type.ToPosition );
//		m1ToPosByVelocity.isRunOnce = true;
//		( m1ToPosByVelocity as MZMove_ToPosition ).totalMoveTime = 3;
//		( m1ToPosByVelocity as MZMove_ToPosition ).destinationPosition = Vector2.zero;
//		m1ToPosByVelocity.initVelocity = 300;

		SetMainAttack( "CannonL", mode1 );
		SetMainAttack( "CannonR", mode1 );
		SetSubCannonAttack( "CannonM", mode1 );
		SetSubCannonAttack( "CannonL", mode1 );
		SetSubCannonAttack( "CannonR", mode1 );
	}

	void SetMainAttack(string partName, MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ partName ] );
		partControlUpdate.Add( partControl );

		MZAttack_Idle show = partControl.AddAttack<MZAttack_Idle>();
		show.duration = 1.0f;
		show.isRunOnce = true;

		MZAttack_OddWay nWay = partControl.AddAttack<MZAttack_OddWay>();
		nWay.bulletName = "EBBee";
		nWay.numberOfWays = 7;
		nWay.colddown = 0.25f;
		nWay.intervalDegrees = 10;
		nWay.initVelocity = 400;
		nWay.duration = 1.0f;
		nWay.additionalWaysPerLaunch = -2;
		nWay.targetHelp = new MZTargetHelp_Target();

		MZAttack_Idle cd = partControl.AddAttack<MZAttack_Idle>();
		cd.duration = 4.0f;
	}

	void SetSubCannonAttack(string partName, MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ partName ] );
		partControlUpdate.Add( partControl );

		MZAttack_Idle show = partControl.AddAttack<MZAttack_Idle>();
		show.duration = 1.0f;
		show.isRunOnce = true;

		MZAttack_Idle wait1 = partControl.AddAttack<MZAttack_Idle>();
		wait1.duration = 2.0f;

		MZAttack_OddWay attack = partControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 5;
		attack.intervalDegrees = 20;
		attack.bulletName = "EBDonuts";
		attack.initVelocity = 250;
		attack.targetHelp = new MZTargetHelp_Target();
		attack.colddown = 0.05f;
		attack.additionalVelocity = 50;
		attack.duration = 0.5f;

		MZAttack_Idle wait2 = partControl.AddAttack<MZAttack_Idle>();
		wait2.duration = 2.5f;
	}
}