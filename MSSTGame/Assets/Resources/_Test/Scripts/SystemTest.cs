using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SystemTest : MonoBehaviour
{
	float interval = 3.0f;
	float cd = 1.0f;
	Vector2 size = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE;
	Vector2 origin = new Vector2( MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.x - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x/2,
			MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.y - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.y/2 );

	void Start()
	{
		OT.PreFabricate( "Bullet", 100 );
	}

	void UpdateEveryFrame()
	{

	}

	void UpdateEveryCD()
	{
//		CreateEnemies();
	}

	bool hasCreated = false;

	void CreateEnemies()
	{
		if( hasCreated )
			return;

		int numberOfEnemies = 3;
		float intervalOfEnemies = 200;

		for( int i = 0; i < numberOfEnemies; i++ )
		{
			GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "E1", "Enemy001Setting" );
			enemy.GetComponent<MZCharacter>().position = new Vector2( -intervalOfEnemies*( numberOfEnemies - 1 )/2 + i*intervalOfEnemies, 500 );
		}

		hasCreated = true;
	}

	void Update()
	{
		cd -= Time.deltaTime;

		UpdateEveryFrame();

		if( cd <= 0 )
		{
			UpdateEveryCD();
			cd += interval;
		}
	}
}