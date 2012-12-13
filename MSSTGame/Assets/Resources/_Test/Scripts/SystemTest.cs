using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SystemTest : MonoBehaviour
{
	float interval = 5.0f;//0.03f; // 6.0f
	float cd = 1.0f;

	void Start()
	{
//		CreateManyEnemyBullet();
//		CreateFourEnemy();
//		CreateManySprites();
	}

	void Update()
	{
		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
//			CreateEnemy();
			CreateFourEnemy();
//			CreateManyEnemyBullet();
//			CreateManySprites();
//			UpdateManySprite();
			cd += interval;
		}

		TestSprite();
	}

	List<GameObject> otSpriteObjectsArray;
	int updateCount = 0;
	void CreateManySprites()
	{
		Vector2 size = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE;
		Vector2 origin = new Vector2( MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.x - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x/2,
			MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.y - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.y/2 );


		if( otSpriteObjectsArray == null )
			otSpriteObjectsArray = new List<GameObject>();

		for( int i = 0; i < 300; i++ )
		{
			GameObject otObject = MZResources.InstantiateOrthelloSprite( "Sprite" );
			OTSprite sprite = otObject.GetComponent<OTSprite>();

			sprite.name = "www";
			sprite.depth = -100;
			sprite.position = new Vector2( origin.x + UnityEngine.Random.Range( 0, size.x ), origin.y + UnityEngine.Random.Range( 0, size.y ) );

			sprite.spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainterByName( "[test]playerBullet" );
			sprite.frameName = "Goblet_normal0001";
			sprite.size *= 0.1f;
			sprite.rotation = UnityEngine.Random.Range( 0, 360 );

			otSpriteObjectsArray.Add( otObject );
		}
	}

	void UpdateManySprite()
	{
		bool _active = ( updateCount%2 == 0 );
		foreach( GameObject o in otSpriteObjectsArray )
		{
			o.active = _active;
		}

		updateCount++;
	}

	void CreateManyEnemyBullet()
	{
		Vector2 size = MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE;
		Vector2 origin = new Vector2( MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.x - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.x/2,
			MZGameSetting.PLAYER_MOVABLE_BOUND_CENTER.y - MZGameSetting.PLAYER_MOVABLE_BOUND_SIZE.y/2 );

		for( int i = 0; i < 50; i++ )
		{
			GameObject eb = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyBullet, "EB", "EnemyBullet001Setting" );
			eb.GetComponent<MZCharacter>().position = new Vector2( origin.x + UnityEngine.Random.Range( 0, size.x ), origin.y + UnityEngine.Random.Range( 0, size.y ) );
		}
	}

	void CreateFourEnemy()
	{
		for( int i = 0; i < 2; i++ )
		{
			string enemySettingName = ( UnityEngine.Random.Range( 0, 2 ) == 0 )? "Enemy001Setting" : "Enemy001Setting";

			GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "Enemy", enemySettingName );

			float x = -150 + i*300;
//			x = 0;
			float posY = ( cd != 0 )? 550 : 100;
			enemy.GetComponent<MZCharacter>().position = new Vector2( x, posY );
		}
	}

	void CreateEnemy()
	{
		string enemySettingName = ( UnityEngine.Random.Range( 0, 2 ) == 0 )? "Enemy001Setting" : "Enemy001Setting";

		GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "Enemy", enemySettingName );

		float x = UnityEngine.Random.Range( -100, 100 );
		enemy.GetComponent<MZCharacter>().position = new Vector2( x*3, 550 );
//		enemy.GetComponent<MZCharacter>().position = new Vector2( 0, 400 );
	}


//	float totalTime = 0.5f;
//	float existedTime = 0.5f;
//	bool dir = true;
//	float timeCount = 0;

	void TestSprite()
	{
//		wireframe.renderer.material.mainTextureScale = new Vector2( scaleX, scaleY );

		// mode A
//		timeCount += Time.deltaTime;
//		if( timeCount >= totalTime )
//		{
//			timeCount = 0;
//			dir = !dir;
//		}
//
//		float nextAlpha = ( dir )? timeCount/totalTime : 1 - ( timeCount/totalTime );
//
//		wireframe.GetComponent<MeshRenderer>().material.SetColor( "_TintColor", new Color( 0.5f, 0.5f, 0.5f, nextAlpha*0.25f ) );

		// mode B
//		timeCount += Time.deltaTime;
//
//		if( dir == true )
//		{
//			if( timeCount >= totalTime )
//			{
//				dir = false;
//				wireframe.GetComponent<MeshRenderer>().material.SetColor( "_TintColor", new Color( 0.5f, 0.5f, 0.5f, 0.5f ) );
//				timeCount = 0;
//			}
//		}
//		else
//		{
//			if( timeCount >= existedTime )
//			{
//				dir = true;
//				wireframe.GetComponent<MeshRenderer>().material.SetColor( "_TintColor", new Color( 0.5f, 0.5f, 0.5f, 0 ) );
//				timeCount = 0;
//			}
//		}

//		wireframe.renderer.material.SetTextureOffset( "_MainTex", new Vector2( 0.5f, 1 ) );
//		wireframe.GetComponent<MeshRenderer>().material.SetTextureScale( "_MainTex", new Vector2( 0.5f, 1 ) ); //SetTextureOffset( "_MainTex", new Vector2( 0.5f, 1 ) );

//		wireframe.GetComponent<MeshRenderer>().material.mainTexture.

//		Texture2D t2d = (Texture2D)wireframe.GetComponent<MeshRenderer>().material.mainTexture;
//
//		for( int i = 0; i < t2d.width; i++ )
//		{
//			for( int j = 0; j < t2d.height; j++ )
//			{
//				t2d.SetPixel( i, j, Color.black );
//			}
//		}
	}
}