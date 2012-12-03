using UnityEngine;
using System.Collections;

public class MZAttack_OddWay : MZAttack_Base
{
	protected override void UpdateWhenActive()
	{
		// target class read-go
//		Vector2 playerPosition = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().GetPlayer().GetComponent<MZCharacter>().position;
//		Vector2 selfPosition = controlTarget.position;
//
//		Vector2 movingVectorTpPlayer = MZMath.unitVectorFromP1ToP2( selfPosition, playerPosition );
//		GameObject bullet = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyBullet, "EnemyBullet" );
	}
}

