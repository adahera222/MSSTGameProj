using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_M000 : MZFormation
{
	public override float disableNextFormationTime
	{ get { return 3; } }

	protected override int maxEnemyCreatedNumber
	{ get { return 1; } }

	//

	int _enemyCount;
	Vector2 _initPosition;

	//

	protected override void InitValues()
	{
		enemyName = "EnemyM000";
	}
	
	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		AddNewEnemy( true );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{

	}

	protected override void UpdateWhenActive()
	{

	}

	protected override Vector2 GetEnemyStartPosition()
	{
		float y = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.y + 150;
		float left = MZGameSetting.PLAYER_MOVABLE_BOUND_DOWNLEFT.x/2;
		float right = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.x/2;

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector2( left, y );
			case PositionType.Mid:
				return new Vector2( 0, y );
			case PositionType.Right:
				return new Vector2( right, y );
			default:
				return new Vector2( 0, y );
		}

	}
}
