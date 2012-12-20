using UnityEngine;
using System.Collections;

public class Enemy002Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject)
	{
		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "[Celestial]_Army_med3_normal0001" );
		mainBody.scale = 1.0f;
		mainBody.rotation = 270;
		mainBody.position = Vector2.zero;
		mainBody.AddCollision().Set( new Vector2( 0, 0 ), 90 );

		MZEnemy enemy = characterObject.GetComponent<MZEnemy>();
		enemy.healthPoint = 5;

		MZMode mode1 = enemy.AddMode( "mode1" );

		MZMove move1 = mode1.AddMove( "m1m1", MZMove.Type.Linear );
		move1.initMovingVector = new Vector2( 0, -1 );
		move1.initVelocity = 100;
		move1.duration = 2;

		MZMove move2 = mode1.AddMove( "m1m2", MZMove.Type.Linear );
		move2.initMovingVector = new Vector2( 1, 0 );
		move2.initVelocity = 100;
		move2.duration = 2;
	}
}

