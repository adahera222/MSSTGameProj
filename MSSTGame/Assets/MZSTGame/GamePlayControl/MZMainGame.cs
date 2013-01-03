using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZMainGame : MonoBehaviour
{
	float delayUpdate = 3;
	MZFormationsManager formationsManager;
	MZRankControl rankControl;

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();

//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
//		Resources.UnloadUnusedAssets();

		// extract out to other class ... 
		MZCharacterObjectsFactory.instance.Init();
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyL000", 3 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM000", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyS000", 50 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyM001", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.Player, "PlayerType01", 1 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.PlayerBullet, "PB000", 200 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBDonuts", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "EBBee", 500 );

		MZTime.instance.Reset();
		formationsManager = new MZFormationsManager();
		rankControl = new MZRankControl();

		MZGameComponents.instance.charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();
		MZGameComponents.instance.rankControl = rankControl;

		InitPlayer();
	}

	void Update()
	{
		MZTime.instance.Update();

		delayUpdate -= MZTime.deltaTime;

		if( delayUpdate >= 0 )
			return;

		if( formationsManager != null )
			formationsManager.Update();

//		if( rankControl != null )
//			rankControl.Update();
	}

	void InitPlayer()
	{
		GameObject playerObject = MZCharacterObjectsFactory.instance.Get( MZCharacterType.Player, "PlayerType01" );
		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
