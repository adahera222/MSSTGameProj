using UnityEngine;

public abstract class CharacterSettingBase
{
	public virtual void SetToCharacter(GameObject characterObject, MZCharacterFactory.MZCharacterType characterType)
	{
		characterObject.AddComponent( GetCharacterScriptNameByType( characterType ) );
	}

	protected MZCharacterPart CreatePartGameObjectAndGetScript(GameObject parentObject)
	{
		GameObject go = new GameObject();
		go.transform.parent = parentObject.transform;
		go.AddComponent<MZCharacterPart>();

		return go.GetComponent<MZCharacterPart>();
	}

	protected string GetCharacterScriptNameByType(MZCharacterFactory.MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterFactory.MZCharacterType.Player:
				return "MZPlayer";

			case MZCharacterFactory.MZCharacterType.PlayerBullet:
				return "MZPlayerBullet";

			case MZCharacterFactory.MZCharacterType.EnemyAir:
			case MZCharacterFactory.MZCharacterType.EnemyGround:
				return "MZEnemy";

			case MZCharacterFactory.MZCharacterType.EnemyBullet:
				return "MZEnemyBullet";

			case MZCharacterFactory.MZCharacterType.Unknow:
			default:
				MZDebug.Assert( false, "Unknow type: " + type.ToString() );
				return null;
		}
	}
}
