using UnityEngine;
using System.Collections;
using MZCharacterType = MZCharacterFactory.MZCharacterType;

public class MZEnemy : MonoBehaviour
{
	int hp;

	void Start()
	{
		hp = 4;
	}

	void Update()
	{
		UpdateMove();
		UpdateAttack();

		foreach( GameObject pb in GameObject.Find("MZCharactersManager").GetComponent<MZCharactersManager>().GetList( MZCharacterType.PlayerBullet ) )
		{
			if( pb.GetComponent<MZCharacter>().IsCollide( gameObject.GetComponent<MZCharacter>() ) )
			{
				hp -= 1;
				pb.GetComponent<MZCharacter>().Disable();
			}
		}

		if( hp <= 0 )
			gameObject.GetComponent<MZCharacter>().Disable();
	}

	void UpdateMove()
	{
		gameObject.GetComponent<MZCharacter>().position += new Vector2( 0, -Time.deltaTime*80 );
	}

	float cd = 0;
	float interval = 3.0f;

	void UpdateAttack()
	{
		cd -= Time.deltaTime;
		if( cd <= 0 )
		{
			GameObject player = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().GetPlayer();

//			Vector2 vectorToPlayer = ( player.GetComponent<MZCharacter>().position - gameObject.GetComponent<MZCharacter>().position );
//			vectorToPlayer.;
//			MZDebug.Log( vectorToPlayer.ToString() );

//			for( int i = 0; i < 3; i++ )
//			{
//
//			}

			GameObject eb = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyBullet, "EnemyBullet" );
			eb.GetComponent<MZCharacter>().position = gameObject.GetComponent<MZCharacter>().position;
			eb.GetComponent<MZEnemyBullet>().movingVector = new Vector2( 0, -1 );

			cd += interval;
		}
	}
}
