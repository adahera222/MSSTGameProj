using UnityEngine;
using System.Collections;

public class EnemyM000 : MZEnemy
{
	protected override void InitValues()
	{
		base.InitValues();
		healthPoint = 25;
	}

	public override void InitDefaultMode()
	{
		base.InitDefaultMode();

		MZMode mode1 = AddMode( "m1" );

		MZMove m1show = mode1.AddMove( MZMove.Type.LinearBy );
		m1show.direction = 270;
		m1show.velocity = 700;
		m1show.duration = 0.2f;

		MZMove m1Idle = mode1.AddMove( MZMove.Type.LinearBy );
		m1Idle.velocity = 0;
		m1Idle.duration = 5;

		MZMove m1Left = mode1.AddMove( MZMove.Type.LinearBy );
		m1Left.direction = 270;
		m1Left.velocity = 100;
		m1Left.duration = -1;

		SetMainBodyAttack( mode1 );
		SetSubCannonAttack( "Cannon", mode1 );
		SetSubCannonAttack( "CannonL", mode1 );
		SetSubCannonAttack( "CannonR", mode1 );
	}

	void SetMainBodyAttack(MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "MainBody" ] );
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
		nWay.targetHelp.calcuteEveryTime = false;

		nWay.offsetPosition.Add( new Vector2( 0, 0 ) );
		for( int i = 0; i < nWay.numberOfWays; i++ )
		{
			nWay.offsetPosition.Add( new Vector2( -40*i, 0 ) );
			nWay.offsetPosition.Add( new Vector2( 40*i, 0 ) );
		}

		MZAttack_Idle cd = partControl.AddAttack<MZAttack_Idle>();
		cd.duration = 4.0f;
	}

	void SetSubCannonAttack(string cannonName, MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ cannonName ] );
		partControlUpdate.Add( partControl );

		MZAttack_Idle show = partControl.AddAttack<MZAttack_Idle>();
		show.duration = 1.0f;
		show.isRunOnce = true;

		MZAttack_Idle wait1 = partControl.AddAttack<MZAttack_Idle>();
		wait1.duration = 2.0f;

		MZAttack_OddWay attack = partControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 1;
		attack.bulletName = "EBDonuts";
		attack.initVelocity = 250;
		attack.targetHelp = new MZTargetHelp_Target();
		attack.targetHelp.calcuteEveryTime = true;
		attack.colddown = 0.05f;
		attack.additionalVelocity = 50;
		attack.duration = 0.5f;

		MZAttack_Idle wait2 = partControl.AddAttack<MZAttack_Idle>();
		wait2.duration = 2.5f;
	}
}
