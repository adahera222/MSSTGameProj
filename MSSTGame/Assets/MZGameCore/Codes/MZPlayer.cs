using UnityEngine;
using System.Collections;

public class MZPlayer : MonoBehaviour
{
	float interval = 0.2f;
	float cd = 0;

	void Start()
	{

	}

	void Update()
	{
		if( Input.GetMouseButton( 0 ) )
		{
			Vector3 pos = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
			gameObject.transform.position = new Vector3( pos.x, pos.y, -30 );

			cd -= Time.deltaTime;
			if( cd <= 0 )
			{
				MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterFactory.MZCharacterType.PlayerBullet, "PB" );
				cd += interval;
			}
		}
	}
}
