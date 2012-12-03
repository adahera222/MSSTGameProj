using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class SystemTest : MonoBehaviour
{
	public float interval = 2.5f;
	public GameObject body;
	public GameObject wireframe;
//	public GameObject iwantMaterial;
	float cd;
//	bool setted = false;

	void Start()
	{
//		GameObject setting = (GameObject)System.Activator.CreateInstance( Type.GetType( "UnityEngine.GameObject" ) );
//		setting.name = "aaaaa";
//		MZDebug.Log( setting.name );


//		GameObject go = (GameObject)System.Activator.CreateInstance( Type.GetType( "GameObject" ) );
//		go.name = "AAAAA";

//		Material m = GetMaterial();
//		iwantMaterial.AddComponent<

//		if( iwantMaterial != null )
//		{
//			Mesh m = GetMesh();
//			MeshFilter mf = iwantMaterial.AddComponent<MeshFilter>();
//			mf.mesh = m;
//			
//			iwantMaterial.AddComponent<MeshRenderer>().renderer.material = new Material( Shader.Find( "Particles/AlphaBlended" ) );
//		}

		cd = 0;

//		body.GetComponent<OTSprite>().image = (Texture)Resources.Load( "Textures/test_body", typeof( Texture ) );
//		wireframe.GetComponent<OTSprite>().image = (Texture)Resources.Load( "Textures/test_wireframe", typeof( Texture ) );

		wireframe.GetComponent<MeshRenderer>().material.shader = Shader.Find( "Particles/Additive" );
		wireframe.GetComponent<MeshRenderer>().material.SetColor( "_TintColor", new Color( 0.5f, 0.5f, 0.5f, 0.5f ) );
		wireframe.transform.localScale = new Vector3( 128, 256, 1 );
		wireframe.renderer.material.mainTextureScale = new Vector2( 0.5f, 1 );

//		TestSprite();
	}

	void Update()
	{
		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
//			CreateEnemy();
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

	void CreateManySprites()
	{
		for( int i = 0; i < 15; i++ )
		{
			GameObject s = (GameObject)MZResources.InstantiateOrthelloSprite( "Sprite" );
			s.GetComponent<OTSprite>().size = new Vector2( 30, 30 );
			s.GetComponent<OTSprite>().depth = -30;
			s.GetComponent<OTSprite>().position = new Vector2( UnityEngine.Random.Range( -300, 300 ), UnityEngine.Random.Range( -300, 300 ) );

			s.GetComponent<OTSprite>().spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainter( "[Celestial]_Army_med2_normal0005" );
			s.GetComponent<OTSprite>().frameName = "[Celestial]_Army_med2_normal0005";
			s.GetComponent<OTSprite>().size = new Vector2( 10, 10 );
			s.GetComponent<OTSprite>().rotation = UnityEngine.Random.Range( 0, 360 );

			s.transform.parent = GameObject.Find( "MZEnemyBullets" ).transform;
		}
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