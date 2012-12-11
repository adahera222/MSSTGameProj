using UnityEngine;

public class PlayerBullet001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject)
	{
		MZCharacter character = characterObject.GetComponent<MZCharacter>();

		MZCharacterPart mainBody = character.AddPart( "MainBody" );
		mainBody.SetFrame( "Egg_normal00001" );
		mainBody.color = new Color( 1, 0, 0.5f );
		mainBody.rotation = 90;
		mainBody.scale = 0.3f;
		mainBody.position = Vector2.zero;
		mainBody.AddCollision().Set( new Vector2( 0, 0 ), 25 );
	}
}
