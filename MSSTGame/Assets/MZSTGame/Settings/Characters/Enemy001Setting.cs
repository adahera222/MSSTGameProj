using UnityEngine;

public class Enemy001Setting : CharacterSettingBase
{
	public override void SetToCharacter(GameObject characterObject, MZCharacterFactory.MZCharacterType characterType)
	{
		base.SetToCharacter( characterObject, characterType );

		MZCharacterPart leftBody = CreatePartGameObjectAndGetScript( characterObject );
		leftBody.PlayAnimation( "Goblet_normal" );
		leftBody.name = "L";
		leftBody.rotation = 270;
		leftBody.position = new Vector2( 100, -90 );
		leftBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacterPart rightBody = CreatePartGameObjectAndGetScript( characterObject );
		rightBody.PlayAnimation( "Goblet_normal" );
		rightBody.name = "R";
		rightBody.rotation = 270;
		rightBody.position = new Vector2( -100, -90 );
		rightBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacterPart mainBody = CreatePartGameObjectAndGetScript( characterObject );
		mainBody.PlayAnimation( "[Celestial]_Army_med3_normal" );
		mainBody.name = "M";
		mainBody.scale = 1.0f;
		mainBody.rotation = 270;
		mainBody.position = Vector2.zero;
		mainBody.collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacter character = characterObject.GetComponent<MZCharacter>();
		character.AddPart( "L", leftBody );
		character.AddPart( "R", rightBody );
		character.AddPart( "M", mainBody );
	}
}