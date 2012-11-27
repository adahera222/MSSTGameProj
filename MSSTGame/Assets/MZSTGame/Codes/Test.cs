using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
	public float interval = 2.5f;

	public GameObject body;
	public GameObject wireframe;

	float cd;
	bool setted = false;

	void Start()
	{
		cd = 0;

//		body.GetComponent<OTSprite>().image = (Texture)Resources.Load( "Textures/test_body", typeof( Texture ) );
//		wireframe.GetComponent<OTSprite>().image = (Texture)Resources.Load( "Textures/test_wireframe", typeof( Texture ) );

		wireframe.GetComponent<MeshRenderer>().material.shader = Shader.Find( "Particles/Additive" );
	}

	void Update()
	{
		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
			GameObject enemy = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterFactory.MZCharacterType.EnemyAir, "Enemy" );

			float x = Random.Range( -100, 100 );
			enemy.GetComponent<MZCharacter>().position = new Vector2( x*3, 630 );

			cd += interval;

//			GameObject t = (GameObject)GameObject.Find( "MZEnemiesAir" );
//			MZDebug.Log( t.GetComponentsInChildren<MZEnemy>().Length.ToString() );
		}

		TestSprite();
	}

	float totalTime = 1.0f;
	bool dir = true;
	float timeCount = 0;

	void TestSprite()
	{
		timeCount += Time.deltaTime;
		if( timeCount >= totalTime )
		{
			timeCount = 0;
			dir = !dir;
		}

		float nextAlpha = ( dir )? timeCount/totalTime : 1 - ( timeCount/totalTime );

//		body.GetComponent<MeshRenderer>().material.color.a = 0.5f;
//		Debug.Log( wireframe.GetComponent<MeshRenderer>().materials.Length.ToString() );

		wireframe.GetComponent<MeshRenderer>().material.SetColor( "_TintColor", new Color( 0.5f, 0.5f, 0.5f, nextAlpha*0.25f ) );
	}
}
