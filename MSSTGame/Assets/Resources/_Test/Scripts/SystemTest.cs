using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class SystemTest : MonoBehaviour
{
	public GameObject body;
	public GameObject wireframe;
//	public GameObject iwantMaterial;
	float interval = 1.5f;
	float cd = 5.0f;
//	bool setted = false;

	OTContainer container1;
	OTContainer container2;
	string frameName1 = "[Celestial]_Army_med2_normal0001";
	string frameName2 = "[Celestial]_Army_med2_normal0001";

	List<GameObject> spritesPoolForContainer1;
	List<GameObject> spritesPoolForContainer2;
	int currentIndexOfSpritesPool1 = 0;
	int currentIndexOfSpritesPool2 = 0;
	int depth1 = -20;
	int depth2 = -30;

	void Start()
	{
		container1 = (OTContainer)GameObject.Find( "[test]enemies_atlas" ).GetComponent<OTSpriteAtlasCocos2D>();  //MZOTFramesManager.GetInstance().GetFrameContainter( frameName1 );
		container2 = (OTContainer)GameObject.Find( "[test]atlas2" ).GetComponent<OTSpriteAtlasCocos2D>(); //MZOTFramesManager.GetInstance().GetFrameContainter( frameName2 );

		spritesPoolForContainer1 = new List<GameObject>();
		spritesPoolForContainer2 = new List<GameObject>();

		for( int i = 0; i < 1000; i++ )
		{
			GameObject s1 = MZResources.InstantiateOrthelloSprite( "Sprite" );
			GameObject s2 = MZResources.InstantiateOrthelloSprite( "Sprite" );

			s1.active = false;
			s2.active = false;

			s1.GetComponent<OTSprite>().spriteContainer = container1;
			s2.GetComponent<OTSprite>().spriteContainer = container2;

			s1.GetComponent<OTSprite>().depth = depth1;
			s2.GetComponent<OTSprite>().depth = depth2;

			Vector2 invaildPos = new Vector2( 0, 0 );
			s1.GetComponent<OTSprite>().position = invaildPos;
			s2.GetComponent<OTSprite>().position = invaildPos;

			spritesPoolForContainer1.Add( s1 );
			spritesPoolForContainer2.Add( s2 );
		}

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
		UpdateManySprites();

		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
//			CreateEnemy();
			CreateManySprites();
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

		if( manySpritesList == null )
			manySpritesList = new List<OTSprite>();

		for( int i = 0; i < 30; i++ )
		{
			int currentIndex = -1;
			List<GameObject> pool = null;
			string frameName = null;
			int depth;

			if( UnityEngine.Random.Range( 0, 2 ) == 0 )
			{
				pool = spritesPoolForContainer1;
				frameName = frameName1;
				currentIndex = currentIndexOfSpritesPool1;
				currentIndexOfSpritesPool1++;
				depth = depth1;
			}
			else
			{
				pool = spritesPoolForContainer2;
				frameName = frameName2;
				currentIndex = currentIndexOfSpritesPool2;
				currentIndexOfSpritesPool2++;
				depth = depth2;
			}

			GameObject s = pool[ currentIndex ];  //(GameObject)MZResources.InstantiateOrthelloSprite( "Sprite" );
			s.active = true;
			s.GetComponent<OTSprite>().position = new Vector2( UnityEngine.Random.Range( -rangeValue, rangeValue ), UnityEngine.Random.Range( -rangeValue, rangeValue ) );

			s.GetComponent<OTSprite>().frameName = frameName;
			s.GetComponent<OTSprite>().rotation = UnityEngine.Random.Range( 0, 360 );

			s.transform.parent = GameObject.Find( "MZEnemyBullets" ).transform;


			s.AddComponent<MZCharacterPart>();

			manySpritesList.Add( s.GetComponent<OTSprite>() );
		}
	}

	void UpdateManySprites()
	{
		if( manySpritesList == null )
			return;

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