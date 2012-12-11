using UnityEngine;
using System.Collections;

public class EnemyBullet001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject)
	{
		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "Donut_normal0001" );
		mainBody.scale = 0.4f;
		mainBody.rotation = 270;
//		mainBody.color = Color.green/2;
		mainBody.AddCollision().Set( new Vector2( 0, 0 ), 10 );
		mainBody.position = Vector2.zero;
		mainBody.faceTo = new MZFaceTo_MovingVector();
	}
}
