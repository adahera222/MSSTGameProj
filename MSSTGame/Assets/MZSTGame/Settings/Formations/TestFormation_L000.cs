using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_L000 : MZFormation
{
	public override float disableNextFormationTime
	{
		get { return 15; }
	}

	public TestFormation_L000() : base()
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
