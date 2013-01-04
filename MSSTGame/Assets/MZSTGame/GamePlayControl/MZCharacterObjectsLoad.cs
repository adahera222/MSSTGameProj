using UnityEngine;
using System.Collections;

public class MZCharacterObjectsLoad
{
	public static void Load()
	{
		MZCharacterObjectsFactory.instance.Init();
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyL000", 3 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM000", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyS000", 50 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM001", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.Player, "PlayerType01", 1 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.PlayerBullet, "PB000", 200 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBDonuts", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBBee", 500 );
	}
}
