using UnityEngine;
using System.Collections;

public class EnemyBullet002Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject)
	{
		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "LittleBee_normal0001" );
		mainBody.scale = 0.5f;
		mainBody.rotation = 270;
		mainBody.AddCollision().Set( new Vector2( 0, 0 ), 10 );
		mainBody.position = Vector2.zero;
		mainBody.faceTo = new MZFaceTo_MovingVector();
	}
}

