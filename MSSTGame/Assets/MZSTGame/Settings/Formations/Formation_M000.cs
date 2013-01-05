using UnityEngine;
using System.Collections;

public class Formation_M000 : MZFormation
{
	public override float nextCreatedTime
	{ get { return 3; } }

	//

	int _enemyCount;

	//

	public Formation_M000() : base()
	{
		duration = 2.5f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_enemyCount = 0;
		int rank = GetSelfRank();

		for( int i = 0; i < rank + 1; i++ )
		{
			AddNewEnemy( MZCharacterType.EnemyAir, "EnemyM000", true );
			_enemyCount++;
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		int rank = GetSelfRank();
		float interval = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x/( rank + 2 );
		float x = MZGameSetting.PLAYER_MOVABLE_BOUND_DOWNLEFT.x + ( _enemyCount + 1 )*interval;
		float y = MZGameSetting.PLAYER_MOVABLE_BOUND_TOPRIGHT.y + 150;

		enemy.GetComponent<MZEnemy>().position = new Vector2( x, y );
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
