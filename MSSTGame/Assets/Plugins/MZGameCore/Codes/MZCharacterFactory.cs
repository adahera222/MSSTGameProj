using System.Collections;

namespace MZGameCore
{
	public class CharacterFactory
	{
		public enum CharacterType
		{
			Player,
			PlayerBullet,
			EnemyAir,
			EnemyGround,
			EnemyBullet,
		}

		static CharacterFactory instance;

		static public CharacterFactory GetInstance()
		{
			if( instance == null )
				instance = new CharacterFactory();
			return instance;
		}

		// CreateCharacter by Type

		private CharacterFactory()
		{

		}
	}
}