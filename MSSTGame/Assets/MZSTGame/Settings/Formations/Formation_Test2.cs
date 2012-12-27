using UnityEngine;
using System.Collections;

public class Formation_Test2 : MZFormation
{
	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		MZEnemy e1 = Add( MZCharacterType.EnemyAir, "EnemyType001" );
		e1.position = new Vector2( -150, 550 );

		MZEnemy e2 = Add( MZCharacterType.EnemyAir, "EnemyType001" );
		e2.position = new Vector2( 150, 550 );
	}

	protected override void UpdateWhenActive()
	{

	}
}
