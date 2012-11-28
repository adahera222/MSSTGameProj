using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
