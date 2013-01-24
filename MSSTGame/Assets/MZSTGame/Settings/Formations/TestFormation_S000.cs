using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_S000 : MZFormation
{
	public override float disableNextFormationTime
	{ get { return 1; } }

	protected override int maxEnemyCreatedNumber
	{ get { return 4; } }

	//

	protected override void InitValues()
	{
		enemyName = "EnemyS000";
		enemyCreateTimeInterval = 0.8f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		if( UpdateAndCheckTimeToCreateEnemy() )
		{
			AddNewEnemy( false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.InitDefaultMode();
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		float xValue = 280;
		float yValue = 400;

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector3( -xValue, yValue );

			case PositionType.Right:
				return new Vector3( xValue, yValue );

			case PositionType.Mid:
			default:
				return new Vector3( 0, yValue + 90 );
		}
	}
}
