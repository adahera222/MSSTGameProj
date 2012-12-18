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
//		InitOTPreFabricate();
	}

	void UpdateEveryFrame()
	{

	}

	void UpdateEveryCD()
	{
		CreateEnemies();
//		UpdateOTPreFabricate();
	}

	#region OT Prototype

	int otPrototypeUpdateCount = 0;
	int updateNumberPerTime = 10;
	int numberOfOTPreFab = 300;
	string prefabName = "EnemyHollow";//"DonutsBullet";
	List<GameObject> otPrefabsList;

	void InitOTPreFabricate()
	{
		OT.PreFabricate( prefabName, numberOfOTPreFab );

		otPrefabsList = new List<GameObject>();
		for( int i = 0; i < numberOfOTPreFab; i++ )
		{
			otPrefabsList.Add( OT.CreateObject( prefabName ) );
		}
	}

	void UpdateOTPreFabricate()
	{
		if( otPrototypeUpdateCount*updateNumberPerTime >= numberOfOTPreFab )
			return;

		for( int i = otPrototypeUpdateCount*updateNumberPerTime; i < (otPrototypeUpdateCount+1)*updateNumberPerTime && i < numberOfOTPreFab; i++ )
		{
			GameObject b = otPrefabsList[ i ];
			b.GetComponent<MZCharacter>().position = GetPositionV2Random();
		}

		otPrototypeUpdateCount++;
	}

	#endregion

	#region Create Enemy

	bool hasCreated = false;

	void CreateEnemies()
	{
		if( hasCreated )
			return;

		int numberOfEnemies = 3;
		float intervalOfEnemies = 200;

		for( int i = 0; i < numberOfEnemies; i++ )
		{
//			GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "E1", "Enemy001Setting" );
			GameObject enemy = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyAir, "EnemyHollow" );
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

	#endregion

	Vector2 GetPositionV2Random()
	{
		return new Vector2( origin.x + UnityEngine.Random.Range( 0, size.x ), origin.y + UnityEngine.Random.Range( 0, size.y ) );
	}
}