using UnityEngine;
using System.Collections;

public class EnemyType001 : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();

		healthPoint = 25;
	}

	protected override void InitMode()
	{
		base.InitMode();

		MZMode mode1 = AddMode( "m1" );

		MZMove m1move = mode1.AddMove( "m1move1", MZMove.Type.Linear );
		m1move.initMovingVector = new Vector2( 0, -1 );
		m1move.initVelocity = 50;

//		SetMainBodyAttack( mode1 );
//		SetCannonAttack( mode1 );
	}

	void SetMainBodyAttack(MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "MainBody" ] );
		partControlControlUpdate.Add( partControl );

		MZAttack attack1 = partControl.AddAttack( MZAttack.Type.OddWay );
		attack1.bulletName = "BeeBullet";
		attack1.numberOfWays = 3;
		attack1.colddown = 0.6f;
		attack1.intervalDegrees = 25;
		attack1.initVelocity = 200;
		attack1.targetHelp = new MZTargetHelp_Target();
	}

	void SetCannonAttack(MZMode mode)
	{
		MZControlUpdate<MZPartControl> partControlControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "Cannon" ] );
		partControlControlUpdate.Add( partControl );

		MZAttack attack1 = partControl.AddAttack( MZAttack.Type.OddWay );
		attack1.bulletName = "DonutsBullet";
		attack1.numberOfWays = 1;
		attack1.colddown = 1.0f;
		attack1.initVelocity = 100;
	}
}
