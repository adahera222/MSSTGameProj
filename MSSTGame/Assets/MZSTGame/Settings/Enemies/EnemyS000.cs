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
//		AddMove_Rotation( mode );

//		AddOddWayAttack( partsByNameDictionary[ "MainBody" ], mode );
		AddVortexAttack( partsByNameDictionary[ "MainBody" ], mode, true );
		AddVortexAttack( partsByNameDictionary[ "MainBody" ], mode, false );
	}

	//

	void AddMove_Linear(MZMode mode)
	{
		MZMove move = mode.AddMove( "move", MZMove.Type.Linear );
		move.initMovingVector = ( position.x > 0 )? new Vector2( -1, -0.5f ) : ( position.x < 0 )? new Vector2( 1, -0.5f ) : new Vector2( 0, -1 );
		move.initVelocity = 100;
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

	void AddOddWayAttack(MZCharacterPart part, MZMode mode)
	{
		MZPartControl partControl = new MZPartControl( part );
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
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

	void AddVortexAttack(MZCharacterPart part, MZMode mode, bool flag)
	{
		MZPartControl partControl = new MZPartControl( part );
		MZControlUpdate<MZPartControl> partControlUpdate = mode.AddPartControlUpdater();
		partControlUpdate.Add( partControl );

		MZAttack_Vortex attack = partControl.AddAttack( MZAttack.Type.Vortex ) as MZAttack_Vortex;
		attack.colddown = 0.05f;
		attack.duration = -1;
		attack.initVelocity = 200;
		attack.additionalVelocity = 50;
		attack.bulletName = "EBDonuts";
		attack.intervalDegrees = 12*( ( flag )? 1 : -1 );

		// target test 1
		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.Target );

		// target test 2
//		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.AssignPosition );
//		( attack.targetHelp as MZTargetHelp_AssignPosition ).assignPosition = new Vector2( -20, 400 );

//		target test 3
//		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.AssignMovingVector );
//		( attack.targetHelp as MZTargetHelp_AssignMovingVector ).movingVector = new Vector2( 0, 1 );


		attack.resetAdditionalVelocityPerWave = true;
		attack.timePerWave = 0.3f;
		attack.resetTime = 0.15f;

		MZAttack idle = partControl.AddAttack( MZAttack.Type.Idle );
		idle.duration = 3;
	}
}
