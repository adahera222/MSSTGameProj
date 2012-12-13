using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();
		Resources.UnloadUnusedAssets();

		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemy", MZCharacterType.EnemyAir, 400, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyAir ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]player", MZCharacterType.Player, 10, MZGameSetting.GetCharacterDepth( MZCharacterType.Player ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]playerBullet", MZCharacterType.PlayerBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.PlayerBullet ) );
		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemyBullet", MZCharacterType.EnemyBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyBullet ) );

		MZCharacterObjectsPoolManager.GetInstance().Init();
		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.EnemyAir, 200 );
		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.Player, 1 );
		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.PlayerBullet, 500 );
		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.EnemyBullet, 1000 );

		MZGameComponents.GetInstance().charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();

		MZOTSpritesPoolManager.GetInstance().SetInactive();
		InitPlayer();
	}

	void Update()
	{

	}

	void InitPlayer()
	{
		GameObject playerObject = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.Player, "Player", "PlayerType01Setting" );
		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );

		MZGameComponents.GetInstance().charactersManager.playerObject = playerObject;
		MZGameComponents.GetInstance().charactersManager.playerCharacter = playerObject.GetComponent<MZCharacter>();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
