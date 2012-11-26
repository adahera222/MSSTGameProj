using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public float interval = 2.5f;
	float cd;
	
	void Start()
	{
		cd = 0;
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
		}
	}
}
