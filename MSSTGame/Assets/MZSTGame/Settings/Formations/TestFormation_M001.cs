using UnityEngine;
using System.Collections;

public class TestFormation_M001 : MZFormation
{
	Vector2 _initPosition;

	public override float disableNextFormationTime
	{
		get
		{
			return 3;
		}
	}

	public TestFormation_M001() :base()
	{
		duration = 3.0f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_initPosition = GetInitPosition( positionType );
		AddNewEnemy( MZCharacterType.EnemyAir, "EnemyM001", false );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.CreateNewModes();

		enemy.healthPoint = 25;
		enemy.position = _initPosition;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove move = mode.AddMove( "GoDie", MZMove.Type.ToTarget );
		move.velocity = 200;
		move.isRunOnce = true;
		move.duration = 3;

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		AddVortexAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, true );
		AddVortexAttack( enemy.partsByNameDictionary[ "MainBody" ], mode, false );
	}

	protected override void UpdateWhenActive()
	{
		
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
//		attack.additionalVelocity = 50;
		attack.bulletName = "EBDonuts";
		attack.intervalDegrees = 12*( ( flag )? 1 : -1 );

		// target test 1
//		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.Target );

		// target test 2
//		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.AssignPosition );
//		( attack.targetHelp as MZTargetHelp_AssignPosition ).assignPosition = new Vector2( -20, 400 );

//		target test 3
		attack.targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.AssignDirection );
		( attack.targetHelp as MZTargetHelp_AssignDirection ).direction = 90;


//		attack.resetAdditionalVelocityPerWave = true;
//		attack.timePerWave = 0.3f;
//		attack.resetTime = 0.15f;

		MZAttack idle = partControl.AddAttack( MZAttack.Type.Idle );
		idle.duration = 3;
	}

	Vector2 GetInitPosition(PositionType positionType)
	{
		float y = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.y + 150;
		float left = MZGameSetting.PLAYER_MOVABLE_BOUND_DOWNLEFT.x/2;
		float right = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.x/2;

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector2( left, y );
			case PositionType.Mid:
				return new Vector2( 0, y );
			case PositionType.Right:
				return new Vector2( right, y );
			default:
				return new Vector2( 0, y );
		}

	}
}
