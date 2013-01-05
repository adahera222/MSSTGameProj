using UnityEngine;
using System.Collections;

public class EnemyS000 : MZEnemy
{
	public override void InitValues()
	{
		base.InitValues();
		healthPoint = 1;
	}

	public override void InitDefaultMode()
	{
		base.InitDefaultMode();

		MZMode mode = AddMode( "Mode" );

//		AddMove_Linear( mode );
//		AddMove_ToTarget( mode );
		AddMove_Rotation( mode );

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

	//

	void AddMove_Linear(MZMode mode)
	{
		MZMove move = mode.AddMove( "move", MZMove.Type.Linear );
		move.initMovingVector = ( position.x > 0 )? new Vector2( -1, -0.5f ) : ( position.x < 0 )? new Vector2( 1, -0.5f ) : new Vector2( 0, -1 );
		move.initVelocity = 300;
	}

	void AddMove_ToTarget(MZMode mode)
	{
		MZMove move = mode.AddMove( "move", MZMove.Type.ToTarget );
		move.initVelocity = 300;
	}

	void AddMove_Rotation(MZMode mode)
	{
		MZMove_Rotation move = mode.AddMove( "move", MZMove.Type.Rotation ) as MZMove_Rotation;
		move.angularVelocity = -100;
		move.variationOfRadians = -10;
		move.radiansLimited = 100;
		move.targetHelp.assignPosition = new Vector2( 0, 0 );
	}
}
