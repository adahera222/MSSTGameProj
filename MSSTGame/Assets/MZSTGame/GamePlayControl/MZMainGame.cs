using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZMainGame : MonoBehaviour
{
	public MZTest test = new MZTest();

	//

	float _delayUpdate = 3;
	MZFormationsManager _formationsManager;
	MZRankControl _rankControl;

	//

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();

//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
//		Resources.UnloadUnusedAssets();

		MZCharacterObjectsLoad.Load();

		MZTime.instance.Reset();
		_formationsManager = new MZFormationsManager();
		_rankControl = new MZRankControl();

		MZGameComponents.instance.charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();
		MZGameComponents.instance.rankControl = _rankControl;

		InitPlayer();
	}

	void Update()
	{
		MZTime.instance.Update();

		_delayUpdate -= MZTime.deltaTime;

		if( _delayUpdate >= 0 )
			return;

		if( test.testType == MZTest.Type.None )
			UpdateNormal();
		else
			test.Update();
	}

	void UpdateNormal()
	{
		if( _formationsManager != null )
			_formationsManager.Update();

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
