using UnityEngine;

public abstract class CharacterSettingBase
{
	public virtual void SetToCharacter(GameObject characterObject, MZCharacterType characterType)
	{
		characterObject.AddComponent( GetCharacterScriptNameByType( characterType ) );
	}

	protected string GetCharacterScriptNameByType(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return "MZPlayer";

			case MZCharacterType.PlayerBullet:
				return "MZPlayerBullet";

			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return "MZEnemy";

			case MZCharacterType.EnemyBullet:
				return "MZEnemyBullet";

			case MZCharacterType.Unknow:
			default:
				MZDebug.Assert( false, "Unknow type: " + type.ToString() );
				return null;
		}
	}
}
