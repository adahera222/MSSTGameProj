using UnityEngine;
using System.Collections;

public class Enemy002Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.PlayAnimation( "[Celestial]_Army_med2_normal0001" );
		mainBody.scale = 1.0f;
		mainBody.rotation = 270;
		mainBody.position = Vector2.zero;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZEnemy enemy = characterObject.GetComponent<MZEnemy>();
		enemy.healthPoint = 5;

		MZMode mode1 = enemy.AddMode( "mode1" );

		MZMove_Base move1 = mode1.AddMove( "m1m1", "Linear" );
		move1.initMovingVector = new Vector2( 0, -1 );
		move1.initVelocity = 100;
		move1.duration = 2;

		MZMove_Base move2 = mode1.AddMove( "m1m2", "Linear" );
		move2.initMovingVector = new Vector2( 1, 0 );
		move2.initVelocity = 100;
		move2.duration = 2;
	}
}

