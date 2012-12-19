using UnityEngine;
using System.Collections;

public class TestMove : MonoBehaviour
{
	int dir = 1;
	float lifeTime = 0;

	void Start()
	{
	
	}

	void Update()
	{
		lifeTime += Time.deltaTime;
		float x = gameObject.transform.position.x;

		if( ( x >= 500 && dir == 1 ) || ( x <= -500 && dir == -1 ) )
			dir = -dir;

		gameObject.transform.position = gameObject.transform.position + new Vector3( dir*100*Time.deltaTime, 0, 0 );
		gameObject.transform.rotation = Quaternion.Euler( lifeTime*100, lifeTime*100, 0 );
	}
}
