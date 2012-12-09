using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		Application.targetFrameRate = 60;

		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();
//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer(); // disable anmation function
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]player", MZCharacterType.Player, 10, MZGameSetting.GetCharacterDepth( MZCharacterType.Player ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]playerBullet", MZCharacterType.PlayerBullet, 500, MZGameSetting.GetCharacterDepth( MZCharacterType.PlayerBullet ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemy", MZCharacterType.EnemyAir, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyAir ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemyBullet", MZCharacterType.EnemyBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyBullet ) );

		MZGameComponents.GetInstance().charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();

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
