using UnityEngine;
using System.Collections;

public class EnemyM001 : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();
		healthPoint = 30;
	}

	public override void InitDefaultMode()
	{
		base.InitDefaultMode();

		MZMode mode1 = AddMode( "mode1" );
		mode1.duration = -1;

		MZMove move1 = mode1.AddMove( "m1m1", MZMove.Type.Linear );
		move1.initVelocity = 200;
		move1.initMovingVector = new Vector2( 1, -1 );
		move1.duration = 0.5f;

		MZMove move2 = mode1.AddMove( "m1m2", MZMove.Type.Linear );
		move2.initVelocity = 200;
		move2.initMovingVector = new Vector2( -1, -1 );
		move2.duration = 0.5f;

		AddOddWay2( mode1, partsByNameDictionary[ "MainBody" ] );
	}

	void AddOddWay2(MZMode mode, MZCharacterPart characterPart)
	{
		MZPartControl partControl = new MZPartControl();
		partControl.controlDelegate = characterPart;

		MZAttack attack1 = partControl.AddAttack( "OddWay" );
		attack1.numberOfWays = 24;
		attack1.additionalVelocity = 50;
		attack1.colddown = 0.25f;
		attack1.intervalDegrees = 15;
		attack1.initVelocity = 50;
		attack1.duration = 2.5f;
		attack1.bulletName = "EBDonuts";

		MZAttack attack3 = partControl.AddAttack( "Idle" );
		attack3.duration = 5.0f;

		MZControlUpdate<MZPartControl> partControlTerm = mode.AddPartControlUpdater();
		partControlTerm.Add( partControl );
	}
}
