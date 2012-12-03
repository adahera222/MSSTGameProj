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
		GameObject playerObject = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.Player, "Player", "PlayerType01Setting" );
		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
