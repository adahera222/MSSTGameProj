using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZCharacterObjectsLoad
{
	public static void Load()
	{
		MZCharacterObjectsFactory.instance.Init();
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyS000", 20 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyS001", 50 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyS002", 50 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemySYellow", 50 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemySGreen", 50 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemySRed", 50 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM000", 20 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM001", 20 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM002", 20 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM003", 20 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyL000", 3 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.Player, "PlayerType01", 1 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.PlayerBullet, "PB000", 200 );

		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBDonuts", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBDonutsSmall", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBDonutsLarge", 200 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBBee", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBBee2", 200 );
	}
}
