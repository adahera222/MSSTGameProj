using UnityEngine;
using System.Collections;

public class MZPlayerBullet : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		gameObject.GetComponent<MZCharacter>().position += new Vector2( 0, 800*Time.deltaTime );

		if( gameObject.GetComponent<MZCharacter>().position.y >= 600 )
			DestroyObject( gameObject );
	}
}
