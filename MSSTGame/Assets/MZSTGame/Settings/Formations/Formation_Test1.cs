using UnityEngine;
using System.Collections;

public class Formation_Test1 : MZFormation
{
	public override float nextCreatedTime
	{ get { return 10; } }

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
