using UnityEngine;
using System.Collections;

public class Formation_L000 : MZFormation
{
	public override float nextCreatedTime
	{
		get { return 15; }
	}

	public Formation_L000() : base()
	{
		duration = 9;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		AddNewEnemy( MZCharacterType.EnemyAir, "EnemyL000", true );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.GetComponent<MZEnemy>().position = new Vector2( 0, 500 );
	}

	protected override void UpdateWhenActive()
	{

	}
}
