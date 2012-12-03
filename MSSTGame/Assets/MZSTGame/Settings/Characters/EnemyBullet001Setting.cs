using UnityEngine;
using System.Collections;

public class EnemyBullet001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "Donut_normal0001" );
		mainBody.scale = 0.5f;
		mainBody.rotation = 270;
		mainBody.color = Color.green/2;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 30 ) );
		mainBody.position = Vector2.zero;
	}
}
