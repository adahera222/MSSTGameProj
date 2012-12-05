using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class SystemTest : MonoBehaviour
{
	float interval = 1.5f;
	float cd = 5.0f;

	void Start()
	{

	}

	void Update()
	{
		UpdateManySprites();

		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
			CreateEnemy();
//			CreateManySprites();
			cd += interval;
		}

		TestSprite();
	}

	void CreateEnemy()
	{
		string enemySettingName = ( UnityEngine.Random.Range( 0, 2 ) == 0 )? "Enemy001Setting" : "Enemy001Setting";

		GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "Enemy", enemySettingName );
		enemy.GetComponent<MZCharacter>().position = new Vector2( 0, 0 );

//		float x = UnityEngine.Random.Range( -100, 100 );
//		enemy.GetComponent<MZCharacter>().position = new Vector2( x*3, 650 );
		enemy.GetComponent<MZCharacter>().position = Vector2.zero;
	}

	List<OTSprite> manySpritesList;

	void CreateManySprites()
	{
		float rangeValue = 400;

		for( int i = 0; i < 30; i++ )
		{
			GameObject s = MZOTSpritesPoolManager.GetInstance().GetSpriteObject( MZCharacterType.EnemyAir );
//			s.active = true;
			s.GetComponent<OTSprite>().position = new Vector2( UnityEngine.Random.Range( -rangeValue, rangeValue ), UnityEngine.Random.Range( -rangeValue, rangeValue ) );

			s.GetComponent<OTSprite>().frameName = "[Celestial]_Army_med3_normal0001";
			s.GetComponent<OTSprite>().rotation = UnityEngine.Random.Range( 0, 360 );

			s.transform.parent = GameObject.Find( "MZEnemyBullets" ).transform;


//			s.AddComponent<MZCharacterPart>();
		}
	}

	void UpdateManySprites()
	{
//		if( manySpritesList == null )
//			return;

//		foreach( OTSprite s in manySpritesList )
//		{
//			s.position += new Vector2( 0, -50*Time.deltaTime );
//		}
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