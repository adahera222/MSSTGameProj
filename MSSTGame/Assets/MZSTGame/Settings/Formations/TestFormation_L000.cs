using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_L000 : MZFormation
{
	public override float disableNextFormationTime
	{
		get { return 15; }
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			return 1;
		}
	}

	protected override void InitValues()
	{
		enemyName = "EnemyL000";
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		AddNewEnemy( true );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{

	}

	protected override Vector2 GetEnemyStartPosition()
	{
		return new Vector2( 0, 500 );
	}

	protected override void UpdateWhenActive()
	{

	}
}
