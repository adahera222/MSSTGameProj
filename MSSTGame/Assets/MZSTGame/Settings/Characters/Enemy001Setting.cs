using UnityEngine;

public class Enemy001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject)
	{
		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart leftBody = character.AddPart( "LeftBody" );
		leftBody.SetFrame( "[Celestial]_Army_med2_normal0001" );
		leftBody.scale = 0.4f;
		leftBody.rotation = 270;
		leftBody.position = new Vector2( 100, -90 );
		leftBody.faceTo = new MZFaceTo_Target();
		leftBody.AddCollision().Set( new Vector2( 0, 0 ), 40 );

		MZCharacterPart rightBody = character.AddPart( "RightBody" );
		rightBody.SetFrame( "[Celestial]_Army_med2_normal0001" );
		rightBody.scale = 0.4f;
		rightBody.rotation = 270;
		rightBody.position = new Vector2( -100, -90 );
		rightBody.faceTo = new MZFaceTo_Target();
		rightBody.AddCollision().Set( new Vector2( 0, 0 ), 40 );

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "[Celestial]_Army_med6_normal0001" );
		mainBody.scale = 1.0f;
		mainBody.rotation = 270;
		mainBody.position = Vector2.zero;
		mainBody.AddCollision().Set( new Vector2( 0, 0 ), 90 );

		MZEnemy enemy = characterObject.GetComponent<MZEnemy>();
		enemy.healthPoint = 10;

		// mode 1
		MZMode mode1 = enemy.AddMode( "mode1" );
		mode1.duration = -1;

//		MZMove_Base move1 = mode1.AddMove( "m1m1", "Linear" );
//		move1.initVelocity = 100;
//		move1.initMovingVector = new Vector2( 0, -1 );
//		move1.duration = -1;

		AddOddWay( mode1, leftBody );
		AddOddWay( mode1, rightBody );
		AddOddWay2( mode1, leftBody );
	}

	void AddOddWay(MZMode mode, MZCharacterPart characterPart)
	{
		MZPartControl partControl = new MZPartControl();
		partControl.controlTarget = characterPart;

		MZAttack_Base attack1 = partControl.AddAttack( "OddWay" );
		attack1.numberOfWays = 1;
		attack1.additionalWaysPerLaunch = 2;
		attack1.additionalVelocityPerLaunch = 50;
		attack1.colddown = 0.05f;//0.025f;//;
		attack1.intervalDegrees = 2.5f;
		attack1.initVelocity = 300; //300
		attack1.duration = 0.25f;
		attack1.bulletName = "EnemyBullet002Setting";
		attack1.SetTargetHelp( new MZTargetHelp_Target() );

		MZAttack_Base attackIdle = partControl.AddAttack( "Idle" );
		attackIdle.duration = 3;

		MZAttack_Base attack2 = partControl.AddAttack( "OddWay" );
		attack2.numberOfWays = 9;
		attack2.additionalVelocityPerLaunch = 100;
		attack2.additionalWaysPerLaunch = -2;
		attack2.colddown = 0.2f;
		attack2.intervalDegrees = 15;
		attack2.initVelocity = 300;
		attack2.duration = 0.8f;
		attack2.bulletName = "EnemyBullet002Setting";
		attack2.SetTargetHelp( new MZTargetHelp_Target() );

		MZAttack_Base attack3 = partControl.AddAttack( "Idle" );
		attack3.duration = 2;

		MZControlUpdate<MZPartControl> partControlTerm = mode.AddPartControlUpdater();
		partControlTerm.Add( partControl );
	}

	void AddOddWay2(MZMode mode, MZCharacterPart characterPart)
	{
		MZPartControl partControl = new MZPartControl();
		partControl.controlTarget = characterPart;

		MZAttack_Base attack1 = partControl.AddAttack( "OddWay" );
		attack1.numberOfWays = 12;
		attack1.additionalVelocityPerLaunch = 50;
		attack1.colddown = 0.5f;
		attack1.intervalDegrees = 30;
		attack1.initVelocity = 100;
		attack1.duration = 2.5f;
		attack1.bulletName = "EnemyBullet002Setting";

		MZAttack_Base attack3 = partControl.AddAttack( "Idle" );
		attack3.duration = 5.0f;

		MZControlUpdate<MZPartControl> partControlTerm = mode.AddPartControlUpdater();
		partControlTerm.Add( partControl );
	}
}