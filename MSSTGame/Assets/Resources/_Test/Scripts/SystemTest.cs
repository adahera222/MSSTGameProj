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
		MZPlayer p = (MZPlayer)MZObjectHelp.CreateClass( "MZPlayer" );
		MZDebug.Log( ( p != null )? "NOT NULL" : "NULL" );

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
			GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterFactory.MZCharacterType.EnemyAir, "Enemy" );

			float x = UnityEngine.Random.Range( -100, 100 );
			enemy.GetComponent<MZCharacter>().position = new Vector2( x*3, 650 );

//			cd += interval;
			cd = 9999999;
		}

		TestSprite();
	}

	float totalTime = 0.5f;
	float existedTime = 0.5f;
	bool dir = true;
	float timeCount = 0;

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

	Mesh GetMesh()
	{
		Mesh m = new Mesh();
		m.vertices = new Vector3[5];
		m.uv = new Vector2[5];
		m.normals = new Vector3[5];

		m.normals[ 0 ] = new Vector3( 0, 0, -1 );
		m.normals[ 1 ] = new Vector3( 0, 0, -1 );

		m.vertices[ 0 ].x = 0;
		m.vertices[ 0 ].y = 0;
		m.vertices[ 0 ].z = 0;
		m.uv[ 0 ] = new Vector2( 0, 0 );

		m.vertices[ 1 ].x = 1;
		m.vertices[ 1 ].y = 0;
		m.vertices[ 1 ].z = 0;
		m.uv[ 1 ] = new Vector2( 1, 0 );

		m.vertices[ 2 ].x = 0;
		m.vertices[ 2 ].y = -1;
		m.vertices[ 2 ].z = 0;
		m.uv[ 2 ] = new Vector2( 0, 1 );

		m.vertices[ 3 ].x = 1;
		m.vertices[ 3 ].y = -1;
		m.vertices[ 3 ].z = 0;
		m.uv[ 3 ] = new Vector2( 1, 1 );

		m.vertices[ 4 ].x = 0;
		m.vertices[ 4 ].y = 0;
		m.vertices[ 4 ].z = 0.3f;
		m.uv[ 4 ] = new Vector2( 1, 1 );

		m.triangles = new int[9];
		m.triangles[ 0 ] = 0;
		m.triangles[ 1 ] = 1;
		m.triangles[ 2 ] = 2;
		m.triangles[ 3 ] = 2;
		m.triangles[ 4 ] = 3;
		m.triangles[ 5 ] = 1;
		m.triangles[ 6 ] = 1;
		m.triangles[ 7 ] = 0;
		m.triangles[ 8 ] = 4;
		m.RecalculateBounds();

//		MeshFilter mf = (MeshFilter)transform.GetComponent( typeof( MeshFilter ) );
//		mf.mesh = m;
//		renderer.material = new Material( Shader.Find( "Diffuse" ) );
//		mf.renderer.material.mainTexture = tex;

		return m;
	}
}