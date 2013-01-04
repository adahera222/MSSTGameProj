using UnityEngine;
using System.Collections;

public class EnemyL000 : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();
		healthPoint = 190;
	}

	protected override void InitMode()
	{
		base.InitMode();

		MZMode mode1 = AddMode( "m1" );

//		MZMove m1show = mode1.AddMove( MZMove.Type.Linear );
//		m1show.initMovingVector = new Vector2( 0, -1 );
//		m1show.initVelocity = 150;
//		m1show.duration = 1.0f;

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

		SetMainBodyAttack( mode1 );
		SetSubCannonAttack( "CannonM", mode1 );
		SetSubCannonAttack( "CannonL", mode1 );
		SetSubCannonAttack( "CannonR", mode1 );
	}

	void SetMainBodyAttack(MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "MainBody" ] );
		partControlUpdate.Add( partControl );

		MZAttack show = partControl.AddAttack( MZAttack.Type.Idle );
		show.duration = 1.0f;
		show.isRunOnce = true;

		MZAttack nWay = partControl.AddAttack( MZAttack.Type.OddWay );
		nWay.bulletName = "EBBee";
		nWay.numberOfWays = 7;
		nWay.colddown = 0.25f;
		nWay.intervalDegrees = 10;
		nWay.initVelocity = 400;
		nWay.duration = 1.0f;
		nWay.additionalWaysPerLaunch = -2;
		nWay.targetHelp = new MZTargetHelp_Target();

		MZAttack cd = partControl.AddAttack( MZAttack.Type.Idle );
		cd.duration = 4.0f;
	}

	void SetSubCannonAttack(string cannonName, MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ cannonName ] );
		partControlUpdate.Add( partControl );

		MZAttack show = partControl.AddAttack( MZAttack.Type.Idle );
		show.duration = 1.0f;
		show.isRunOnce = true;

		MZAttack wait1 = partControl.AddAttack( MZAttack.Type.Idle );
		wait1.duration = 2.0f;

		MZAttack attack = partControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 1;
		attack.bulletName = "EBDonuts";
		attack.initVelocity = 250;
		attack.targetHelp = new MZTargetHelp_Target();
		attack.colddown = 0.05f;
		attack.additionalVelocityPerLaunch = 50;
		attack.duration = 0.5f;

		MZAttack wait2 = partControl.AddAttack( MZAttack.Type.Idle );
		wait2.duration = 2.5f;
	}
}