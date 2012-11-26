using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();
		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
		InitPlayer();
	}

	void Update()
	{

	}

	void InitPlayer()
	{
		MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterFactory.MZCharacterType.Player, "Player" );
//		GameObject player = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterFactory.MZCharacterType.Player, "Player" );
//		player.transform.rotation = Quaternion.Euler( 0, 0, 90 );
//		player.transform.localScale = player.transform.localScale*0.5f;
	}
}
