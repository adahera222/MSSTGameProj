using UnityEngine;
using System.Collections;

public class MZRankControl : MZControlBase
{
	public int enemyRank;
	public int enemyRankXp;
	public int enemyNextRankUp;
	public int playerRank;
	public int playerRankXp;
	public int playerNextRankUp;

	//

	public override void Enable()
	{
		base.Enable();

		enemyRank = 5;
		enemyRankXp = 0;
		enemyNextRankUp = 20;

		playerRank = 1;
		playerRankXp = 0;
		playerNextRankUp = 20;
	}

	protected override void UpdateWhenActive()
	{
		if( enemyRankXp >= enemyNextRankUp )
		{
			enemyRank++;
			enemyRankXp = 0;
		}

		if( playerRankXp >= playerNextRankUp )
		{
			playerRank++;
			playerRankXp = 0;
		}
	}
}
