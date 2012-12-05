using UnityEngine;
using System.Collections;

public class PlayerType01Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "Bow_normal0001" );
		mainBody.position = Vector2.zero;
		mainBody.rotation = 90;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 50 ) );
	}
}
