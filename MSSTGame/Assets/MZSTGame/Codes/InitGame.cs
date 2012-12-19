using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();

//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
//		Resources.UnloadUnusedAssets();

		MZCharacterObjectsFactory.instance.Init();
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyHollow", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "DonutsBullet", 1000 );

		// it's suck ... = =||||
//		MZCharacterPartsListInEditorManager.instance.CreateListByOTContainer( "[test]enemyBullet", "ebDonut", "Donut_normal0001", MZCharacterType.EnemyBullet );
//
//		MZCharacterPartsListInEditorManager.instance.CreateListByOTContainer( "[test]enemy", "EnemyThin", "[Celestial]_Army_med2_normal0001", MZCharacterType.EnemyAir );
//		MZCharacterPartsListInEditorManager.instance.CreateListByOTContainer( "[test]enemy", "EnemyHollow", "[Celestial]_Army_med3_normal0001", MZCharacterType.EnemyAir );
//		MZCharacterPartsListInEditorManager.instance.CreateListByOTContainer( "[test]enemy", "EnemyFat", "[Celestial]_Army_med6_normal0001", MZCharacterType.EnemyAir );

//		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemy", MZCharacterType.EnemyAir, 400, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyAir ) );
//		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]player", MZCharacterType.Player, 10, MZGameSetting.GetCharacterDepth( MZCharacterType.Player ) );
//		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]playerBullet", MZCharacterType.PlayerBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.PlayerBullet ) );
//		MZOTSpritesPoolManager.GetInstance().AddPool( "[test]enemyBullet", MZCharacterType.EnemyBullet, 1000, MZGameSetting.GetCharacterDepth( MZCharacterType.EnemyBullet ) );
//
//		MZCharacterObjectsPoolManager.GetInstance().Init();
//		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.EnemyAir, 200 );
//		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.Player, 1 );
//		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.PlayerBullet, 500 );
//		MZCharacterObjectsPoolManager.GetInstance().SetGameObjectsArray( MZCharacterType.EnemyBullet, 1000 );
//
//		// test
//		GameObject otPrototypes = GameObject.Find( "Prototypes" );
//		MZDebug.Alert( otPrototypes == null, "otPrototypes is null" );
//
//		GameObject objectToPrototype = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyAir, "Enemy001", "Enemy001Setting" );
//		objectToPrototype.transform.parent = otPrototypes.transform;
////		test.GetComponent<OTSprite>().spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainterByName( "[test]enemy" );
//
//		for( int i = 0; i < objectToPrototype.transform.GetChildCount(); i++ )
//		{
//			OTSprite childOTSprite = objectToPrototype.transform.GetChild( i ).gameObject.GetComponent<OTSprite>();
//			childOTSprite.spriteContainer = MZOTFramesManager.GetInstance().GetFrameContainterByName( "[test]enemy" );
//		}
//
//		objectToPrototype.active = false;
//
//		// use this to create object
//		OT.PreFabricate( "Enemy001", 100 );
//
//		// test end
//
//
		MZGameComponents.GetInstance().charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();
//
//		MZOTSpritesPoolManager.GetInstance().SetInactive();
//		InitPlayer();
	}

	void Update()
	{

	}

	void InitPlayer()
	{
//		GameObject playerObject = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.Player, "Player", "PlayerType01Setting" );
//		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );
//
//		MZGameComponents.GetInstance().charactersManager.playerObject = playerObject;
//		MZGameComponents.GetInstance().charactersManager.playerCharacter = playerObject.GetComponent<MZCharacter>();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
