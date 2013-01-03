using UnityEngine;
using System.Collections;

public class EnemyS000 : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();
		healthPoint = 3;
	}

	protected override void InitMode()
	{
		base.InitMode();

		MZMode mode = AddMode( "Mode" );

		MZMove move = mode.AddMove( "move", MZMove.Type.Linear );
		move.initMovingVector = ( position.x > 0 )? new Vector2( -1, -0.5f ) : ( position.x < 0 )? new Vector2( 1, -0.5f ) : new Vector2( 0, -1 );
		move.initVelocity = 300;

		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		MZPartControl partControl = new MZPartControl( partsByNameDictionary[ "MainBody" ] );
		partControlUpdate.Add( partControl );

		MZAttack attack = partControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 1;
		attack.colddown = 0.3f;
		attack.duration = 0.1f;
		attack.initVelocity = 500;
		attack.bulletName = "EBDonuts";
		attack.targetHelp = new MZTargetHelp_Target();

		MZAttack idle = partControl.AddAttack( MZAttack.Type.Idle );
		idle.duration = 3;
	}
}
