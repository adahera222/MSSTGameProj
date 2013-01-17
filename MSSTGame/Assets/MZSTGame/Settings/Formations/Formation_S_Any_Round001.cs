using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_S_Any_Round001 : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 5;
		}
	}

	protected override int maxCreatedNumber
	{
		get
		{
			return 8;
		}
	}

	Vector2 _currentPosition;

	public Formation_S_Any_Round001(int constructCode) : base( constructCode )
	{

	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		float intervalDegrees = 360/maxCreatedNumber;
		float distance = 700;

		for( int i = 0; i < maxCreatedNumber; i++ )
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
		enemy.healthPoint = 10;
		enemy.position = _currentPosition;
		enemy.enableRemoveTime = 12;

		MZMode mode = enemy.AddMode( "mode" );

		switch( constructCode )
		{
			case 0:
				SetType1Move( mode, enemy );
				break;

			case 1:
				SetType2Move( mode, enemy );
				break;

			case 2:
				SetType3Move( mode, enemy );
				break;

			default:
				MZDebug.AssertFalse( "not supprt code=" + constructCode.ToString() );
				break;
		}

		// main attack
		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack attackIdle = mainPartControl.AddAttack( MZAttack.Type.Idle );
		attackIdle.duration = 0.8f;
		attackIdle.isRunOnce = true;

		MZAttack attack = mainPartControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 1;
		attack.initVelocity = 200;
		attack.additionalVelocity = 50;
		attack.bulletName = "EBDonuts";
		attack.colddown = 0.05f;
		attack.duration = 0.15f;
		attack.targetHelp = new MZTargetHelp_AssignPosition();
		( attack.targetHelp as MZTargetHelp_AssignPosition ).assignPosition = Vector2.zero;

		MZAttack attackIdle2 = mainPartControl.AddAttack( MZAttack.Type.Idle );
		attackIdle2.duration = 1.8f;
	}

	void SetType1Move(MZMode mode, MZEnemy enemy)
	{
		MZMove show = mode.AddMove( MZMove.Type.LinearBy );
		show.direction = MZMath.DegreesFromXAxisToVector( MZMath.UnitVectorFromP1ToP2( enemy.position, Vector2.zero ) );
		show.duration = 0.6f;
		show.velocity = 500;

		MZMove_Rotation rotShow = mode.AddMove( MZMove.Type.Rotation ) as MZMove_Rotation;
		rotShow.angularVelocity = 50;
		rotShow.variationOfRadians = -300;
		rotShow.radiansLimited = 400;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 10.0f;

		MZMove_Rotation rotOut = mode.AddMove( MZMove.Type.Rotation ) as MZMove_Rotation;
		rotOut.angularVelocity = 50;
		rotOut.variationOfRadians = 300;
		rotOut.targetHelp.assignPosition = Vector2.zero;
		rotOut.duration = 10;
	}

	void SetType2Move(MZMode mode, MZEnemy enemy)
	{
		MZMove_Rotation rotShow = mode.AddMove( MZMove.Type.Rotation ) as MZMove_Rotation;
		rotShow.angularVelocity = 50;
		rotShow.variationOfRadians = -100;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 20;
	}

	void SetType3Move(MZMode mode, MZEnemy enemy)
	{
		MZMove_Rotation rotShow = mode.AddMove( MZMove.Type.Rotation ) as MZMove_Rotation;
		rotShow.angularVelocity = 50;
		rotShow.variationOfRadians = -300;
		rotShow.radiansLimited = 400;
		rotShow.targetHelp.assignPosition = Vector2.zero;
		rotShow.duration = 1.5f;

		MZMove_Idle moveIdle = mode.AddMove( MZMove.Type.Idle ) as MZMove_Idle;
		moveIdle.duration = 5;

		MZMove_LinearTo moveOut = mode.AddMove( MZMove.Type.LinearTo ) as MZMove_LinearTo;
		moveOut.destationPosition = Vector2.zero;
		moveOut.totalTime = 0.5f;
		moveOut.notEndAtDestation = true;
	}
}