using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public GameObject testGameObject;

	void Start()
	{
//		testGameObject.GetComponent<OTAnimatingSprite>().animation = MZOTAnimationsManager.GetInstance().otAnimation;
	}

	float cd = 0;

	void Update()
	{
		cd -= Time.deltaTime;

		if( cd <= 0 )
		{
			GameObject c = MZCharacterFactory.GetInstance()._test_create_character();
			GameObject mzGame = GameObject.Find( "MZCharacters" );
			c.transform.parent = mzGame.transform;

			cd += 1;
		}
	}
}
