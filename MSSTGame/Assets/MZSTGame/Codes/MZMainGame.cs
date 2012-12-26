using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZMainGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();

//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
//		Resources.UnloadUnusedAssets();

		MZCharacterObjectsFactory.instance.Init();
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyType001", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyAir, "EnemyHollow", 10 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.Player, "PlayerType01", 1 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.PlayerBullet, "PlayerMainBullet", 200 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "DonutsBullet", 500 );
		MZCharacterObjectsFactory.instance.Add( MZCharacterType.EnemyBullet, "BeeBullet", 500 );

		MZGameComponents.GetInstance().charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();

		InitPlayer();

		MZTime.instance.Reset();
	}

	void Update()
	{
		MZTime.instance.Update();
	}

	void InitPlayer()
	{
		GameObject playerObject = MZCharacterObjectsFactory.instance.Get( MZCharacterType.Player, "PlayerType01" );
		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );

//		MZGameComponents.GetInstance().charactersManager.playerObject = playerObject;
//		MZGameComponents.GetInstance().charactersManager.playerCharacter = playerObject.GetComponent<MZCharacter>();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
