using UnityEngine;
using System.Collections;

public class Formation_EnemyM001 : MZFormation
{
	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		int rank = GetSelfRank();
		float width = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x;
		float interval = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x/( rank + 2 );

		for( int i = 0; i < rank + 1; i++ )
		{
			GameObject e = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyAir, "EnemyM000" );
			float x = MZGameSetting.PLAYER_MOVABLE_BOUND_DOWNLEFT.x + ( i + 1 )*interval;
			float y = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.y + 150;

			e.GetComponent<MZEnemy>().position = new Vector2( x, y );
		}
	}

	protected override void UpdateWhenActive()
	{

	}

	int GetSelfRank()
	{
		switch( MZGameComponents.instance.rank )
		{
			case 0:
			case 1:
				return 0;
			case 2:
			case 3:
				return 1;
			case 4:
			case 5:
				return 2;
			default:
				return 3;
		}
	}
}
