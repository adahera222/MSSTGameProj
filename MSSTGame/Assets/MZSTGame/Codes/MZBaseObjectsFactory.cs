using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZBaseObjectsFactory : MZSingleton<MZBaseObjectsFactory>
{
	public GameObject Create(MZCharacterType characterType)
	{
		GameObject otSprite = MZResources.InstantiateOrthelloSprite( "Sprite" );
		otSprite.AddComponent( GetBaseObjectScript( characterType ) );

		return otSprite;
	}

	string GetBaseObjectScript(MZCharacterType characterType)
	{
		switch( characterType )
		{
			case MZCharacterType.Player:
			case MZCharacterType.PlayerBullet:
			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyBullet:
			case MZCharacterType.EnemyGround:
				return "MZCharacterPart";

			default:
				MZDebug.Assert( false, "Not support type=" + characterType.ToString() );
				return null;
		}
	}
}
