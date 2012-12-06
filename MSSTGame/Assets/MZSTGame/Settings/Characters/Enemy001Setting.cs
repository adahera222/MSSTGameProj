using UnityEngine;

public class Enemy001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart leftBody = character.AddPart( "LeftBody" );
		leftBody.SetFrame( "[Celestial]_Army_med2_normal0001" );
		leftBody.scale = 0.25f;
		leftBody.rotation = 270;
		leftBody.position = new Vector2( 100, -90 );
		leftBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacterPart rightBody = character.AddPart( "RightBody" );
		rightBody.SetFrame( "[Celestial]_Army_med2_normal0001" );
		rightBody.scale = 0.25f;
		rightBody.rotation = 270;
		rightBody.position = new Vector2( -100, -90 );
		rightBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "[Celestial]_Army_med3_normal0001" );
		mainBody.scale = 1.0f;
		mainBody.rotation = 270;
		mainBody.position = Vector2.zero;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZEnemy enemy = characterObject.GetComponent<MZEnemy>();
		enemy.healthPoint = 10;

		// mode 1
		MZMode mode1 = enemy.AddMode( "mode1" );
		mode1.duration = -1;

		MZMove_Base move1 = mode1.AddMove( "m1m1", "Linear" );
		move1.initVelocity = 25;
		move1.initMovingVector = new Vector2( 0, -1 );
		move1.duration = -1;

//		AddOddWay( mode1, leftBody );
		AddOddWay( mode1, rightBody );

//		MZMove_Base move2 = mode1.AddMove( "m1m2", "Linear" );
//		move2.initVelocity = 100;
//		move2.initMovingVector = new Vector2( -1, -1 );
//		move2.duration = 1.5f;

		// mode2
//		MZMode mode2 = enemy.AddMode( "mode2" );
//		mode2.duration = 3;
//
//		MZMove_Base move3 = mode2.AddMove( "m2m3", "Linear" );
//		move3.initVelocity = 250;
//		move3.initMovingVector = new Vector2( -1, 1 );
//		move3.duration = 1.5f;
//
//		MZMove_Base move4 = mode2.AddMove( "m2m4", "Linear" );
//		move4.initVelocity = 250;
//		move4.initMovingVector = new Vector2( 1, 1 );
//		move4.duration = 1.5f;
	}

	void AddOddWay(MZMode mode, MZCharacterPart characterPart)
	{
		MZPartControl partControl = new MZPartControl();
		partControl.controlTarget = characterPart;

		MZAttack_Base attack1 = partControl.AddAttack( "OddWay" );
		attack1.numberOfWays = 1;
		attack1.additionalWaysPerLaunch = 2;
		attack1.colddown = 0.2f;
		attack1.intervalDegrees = 15;
		attack1.initVelocity = 300;
		attack1.duration = 1.0f;

//		MZAttack_Base attack2 = partControl.AddAttack( "OddWay" );
//		attack2.numberOfWays = 7;
//		attack2.additionalWaysPerLaunch = -2;
//		attack2.colddown = 0.2f;
//		attack2.intervalDegrees = 45;
//		attack2.initVelocity = 800;
//		attack2.duration = 0.5f;

		MZAttack_Base attack3 = partControl.AddAttack( "Idle" );
		attack3.duration = 2;

		MZControlUpdate<MZPartControl> partControlTerm = mode.AddPartControlUpdater();
		partControlTerm.Add( partControl );
	}
}