using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_S_Round001 : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 8;
		}
	}

	Vector2 _currentPosition;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		duration = 3;

		int numEnemy = 12;
		float intervalDegrees = 360/numEnemy;
		float distance = 700;

		for( int i = 0; i < numEnemy; i++ )
		{
			Vector2 vector = MZMath.UnitVectorFromDegrees( intervalDegrees*i );
			_currentPosition = vector*distance;

			AddNewEnemy( MZCharacterType.EnemyAir, "EnemySYellow", false );
		}
	}

	protected override void UpdateWhenActive()
	{

	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.CreateNewModes();
		enemy.healthPoint = 10;
		enemy.position = _currentPosition;
		enemy.enableRemoveTime = 999;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove show = mode.AddMove( MZMove.Type.Linear );
		show.direction = MZMath.DegreesFromXAxisToVector( MZMath.UnitVectorFromP1ToP2( enemy.position, Vector2.zero ) );
		show.duration = 0.6f;
		show.velocity = 500;

		MZMove_Rotation rotShow = mode.AddMove( MZMove.Type.Rotation ) as MZMove_Rotation;
		rotShow.angularVelocity = 50;
		rotShow.targetHelp.assignPosition = Vector2.zero;


		// main attack
		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack attackIdle = mainPartControl.AddAttack( MZAttack.Type.Idle );
		attackIdle.duration = 0.8f;

		MZAttack attack = mainPartControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 1;
		attack.initVelocity = 200;
		attack.bulletName = "EBDonuts";
		attack.colddown = 0.05f;
		attack.duration = 0.25f;
		attack.targetHelp = new MZTargetHelp_AssignPosition();
		( attack.targetHelp as MZTargetHelp_AssignPosition ).assignPosition = Vector2.zero;
	}
}