using UnityEngine;
using System.Collections;

public class EnemyS000 : MZEnemy
{
	protected override void InitValues()
	{
		base.InitValues();
		
		healthPoint = 1;
	}

	public override void InitDefaultMode()
	{
		base.InitDefaultMode();

		MZMode mode = AddMode( "Mode" );

		AddMove_Linear( mode );
//		AddMove_ToTarget( mode );
//		AddMove_Rotation( mode );

		AddOddWayAttack( partsByNameDictionary[ "MainBody" ], mode );
//		AddVortexAttack( partsByNameDictionary[ "MainBody" ], mode, true );
//		AddVortexAttack( partsByNameDictionary[ "MainBody" ], mode, false );
	}

	//

	void AddMove_Linear(MZMode mode)
	{
		MZMove_LinearBy move = mode.AddMove< MZMove_LinearBy>( "move" );
		move.direction = ( position.x > 0 )? 225 : ( position.x < 0 )? 315 : 270;
		move.velocity = 100;
	}

	void AddMove_ToTarget(MZMode mode)
	{
		MZMove_ToTarget move = mode.AddMove<MZMove_ToTarget>( "move" );
		move.velocity = 300;
	}

	void AddMove_Rotation(MZMode mode)
	{
		MZMove_Rotation move = mode.AddMove<MZMove_Rotation>( "move" );
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

		MZAttack_OddWay attack = partControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 1;
		attack.colddown = 0.3f;
		attack.duration = 0.1f;
		attack.initVelocity = 500;
		attack.bulletName = "EBDonuts";
		attack.targetHelp = new MZTargetHelp_Target();

		MZAttack_Idle idle = partControl.AddAttack<MZAttack_Idle>();
		idle.duration = 3;
	}
}
