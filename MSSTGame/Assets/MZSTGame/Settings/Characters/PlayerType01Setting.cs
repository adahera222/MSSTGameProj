using UnityEngine;
using System.Collections;

public class PlayerType01Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterFactory.MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacterPart mainBody = CreatePartGameObjectAndGetScript( characterObject );
		mainBody.PlayAnimation( "Ika_normal" );
		mainBody.name = "MainBody";
		mainBody.position = Vector2.zero;
		mainBody.rotation = 90;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 50 ) );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();
		character.AddPart( "MainBody", mainBody );
	}
}
