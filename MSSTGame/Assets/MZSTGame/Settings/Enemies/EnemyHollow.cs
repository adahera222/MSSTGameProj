using UnityEngine;
using System.Collections;

public class EnemyHollow : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();
		healthPoint = 100;
	}

	protected override void InitMode()
	{
		base.InitMode();

		MZMode mode1 = AddMode( "mode1" );
		mode1.duration = -1;

		MZMove_Base move1 = mode1.AddMove( "m1m1", "Linear" );
		move1.initVelocity = 200;
		move1.initMovingVector = new Vector2( 1, -1 );
		move1.duration = 0.5f;

		MZMove_Base move2 = mode1.AddMove( "m1m2", "Linear" );
		move2.initVelocity = 200;
		move2.initMovingVector = new Vector2( -1, -1 );
		move2.duration = 0.5f;

		AddOddWay2( mode1, partsByNameDictionary[ "MainBody" ] );
	}

	void AddOddWay2(MZMode mode, MZCharacterPart characterPart)
	{
		MZPartControl partControl = new MZPartControl();
		partControl.controlTarget = characterPart;

		MZAttack_Base attack1 = partControl.AddAttack( "OddWay" );
		attack1.numberOfWays = 24;
		attack1.additionalVelocityPerLaunch = 50;
		attack1.colddown = 0.25f;
		attack1.intervalDegrees = 15;
		attack1.initVelocity = 100;
		attack1.duration = 2.5f;
		attack1.bulletName = "DonutsBullet";

		MZAttack_Base attack3 = partControl.AddAttack( "Idle" );
		attack3.duration = 5.0f;

		MZControlUpdate<MZPartControl> partControlTerm = mode.AddPartControlUpdater();
		partControlTerm.Add( partControl );
	}
}
