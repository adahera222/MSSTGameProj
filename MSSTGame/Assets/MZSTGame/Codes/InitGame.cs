using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();
//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer(); // disable anmation function
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]spritesheet3", MZCharacterType.Player, 10, MZGameSetting.GetCharacterDepth( MZCharacterType.Player ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]atlas2", MZCharacterType.EnemyAir, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyAir ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemies_atlas", MZCharacterType.EnemyBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyBullet ) );

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
