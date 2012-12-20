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
		m1move.initVelocity = 300;

		MZControlUpdate<MZPartControl> partControlControlUpdate = mode1.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "MainBody" ] );
		partControlControlUpdate.Add( partControl );

		MZAttack attack1 = partControl.AddAttack( MZAttack.Type.OddWay );
		attack1.bulletName = "BeeBullet";
		attack1.numberOfWays = 3;
		attack1.colddown = 0.8f;
		attack1.intervalDegrees = 25;
		attack1.initVelocity = 50;

//		MZAttack attack1 =
//		"BeeBullet"

//		MZControlUpdate<MZPartControl> partControl = mode1.AddPartControlUpdater();
//		partControl.Add(  )

	}
}
