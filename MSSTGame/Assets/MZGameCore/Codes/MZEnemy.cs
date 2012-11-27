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
		gameObject.GetComponent<MZCharacter>().position += new Vector2( 0, -Time.deltaTime*80 );

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
}
