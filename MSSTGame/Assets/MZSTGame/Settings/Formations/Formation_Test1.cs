/*
 * three mid enemy attack
 *
 **/

using UnityEngine;
using System.Collections;

public class Formation_Test1 : MZFormation
{
	public override bool ActiveCondition()
	{
		return base.ActiveCondition();
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		MZEnemy enemy = Add( MZCharacterType.EnemyAir, "EnemyHollow" );
		enemy.position = new Vector2( 0, 550 );
	}

	public override void Reset()
	{
		base.Reset();
	}

	protected override void UpdateWhenActive()
	{

	}
}
