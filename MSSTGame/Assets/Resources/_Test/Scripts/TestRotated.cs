using UnityEngine;
using System.Collections;

public class TestRotated : MonoBehaviour
{
	public float angleVelocity = 30;
	public bool clockwise = true;
	float totalTime;

	void Start()
	{
		totalTime = 0;
	}

	void Update()
	{
		totalTime += Time.deltaTime;

		float preX = 0;//gameObject.transform.rotation.eulerAngles.x;
		float preZ = 0;//gameObject.transform.rotation.eulerAngles.z;
		gameObject.transform.rotation = Quaternion.Euler( preX, ( ( clockwise )? 1 : -1 )*angleVelocity*totalTime, preZ );
//			RotateAroundLocal( new Vector3( 0, 1, 0 ), angleVelocity*totalTime );
		//  = Quaternion.Euler( preX, ( ( clockwise )? 1 : -1 )*angleVelocity*totalTime, preZ );

		gameObject.transform.renderer.material.SetTextureOffset( "_MainTex", new Vector2( 0, totalTime*0.5f ) );
	}
}
