using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZMainGame : MonoBehaviour
{
	public string playerRankInfo = "";
	public string enemyRankInfo = "";
	public MZTest test = new MZTest();

	//
	int _firstAndSecondUpateCount;
	float _delayUpdate = 4;
	MZFormationsManager _formationsManager;
	MZRankControl _rankControl;

	//

	void Awake()
	{
		Application.targetFrameRate = 30;
	}

	void Start()
	{
		MZOTFramesManager.GetInstance().CreateFramesByExistedContainer();

//		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
//		Resources.UnloadUnusedAssets();

		MZCharacterObjectsLoad.Load();

		MZTime.instance.Reset();

		_rankControl = new MZRankControl();
		_formationsManager = new MZFormationsManager();
		_formationsManager.rankControl = _rankControl;
		test.rankControl = _rankControl;

		if( test.testType != MZTest.Type.None )
		{
			_formationsManager.enableUpdateState = false;
		}

		test.SetForamtionsInfo( _formationsManager );

		MZGameComponents.instance.charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();
		MZGameComponents.instance.rankControl = _rankControl;

		InitPlayer();

		_firstAndSecondUpateCount = 0;

		_rankControl.Enable();
		_formationsManager.Enable();
	}

	void Update()
	{
		MZTime.instance.Update();

		if( _firstAndSecondUpateCount == 0 )
			FirstUpdate();
		else if( _firstAndSecondUpateCount == 1 )
				SecondUpdate();

		_delayUpdate -= MZTime.deltaTime;

		if( _delayUpdate >= 0 )
			return;

		if( test.testType != MZTest.Type.None )
			test.Update();

		if( _formationsManager != null )
			_formationsManager.Update();

		if( _rankControl != null && test.testType == MZTest.Type.None )
			_rankControl.Update();

		UpdateRankInfoToEditor();
	}

	void InitPlayer()
	{
		GameObject playerObject = MZCharacterObjectsFactory.instance.Get( MZCharacterType.Player, "PlayerType01" );
		playerObject.GetComponent<MZCharacter>().InitDefaultMode();
		playerObject.GetComponent<MZCharacter>().position = new Vector2( 0, -200 );

		MZGameComponents.instance.charactersManager.Add( MZCharacterType.Player, playerObject.GetComponent<MZCharacter>() );
	}

	void FirstUpdate()
	{
		_firstAndSecondUpateCount = 1;

		foreach( string name in MZCharacterObjectsFactory.instance.characterObjectNamesByType[MZCharacter.MZCharacterType.EnemyBullet] )
		{
			GameObject bullet = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyBullet, name );
			bullet.GetComponent<MZCharacter>().position = new Vector2( 0, 0 );

			MZGameComponents.instance.charactersManager.Add( MZCharacterType.EnemyBullet, bullet.GetComponent<MZCharacter>() );
		}
	}

	void SecondUpdate()
	{
		_firstAndSecondUpateCount = 2;
		MZGameComponents.instance.charactersManager.RemoveAllCharactersByType( MZCharacterType.EnemyBullet );
	}

	void UpdateRankInfoToEditor()
	{
		playerRankInfo = string.Format( "Lv: {0} {1}/{2}", _rankControl.playerRank, _rankControl.playerRankXp, _rankControl.playerNextRankUp );
		enemyRankInfo = string.Format( "Lv: {0} {1}/{2}", _rankControl.enemyRank, _rankControl.enemyRankXp, _rankControl.enemyNextRankUp );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube( MZGameSetting.PLAYER_MOVABLE_BOUND_V3CENTER, MZGameSetting.PLAYER_MOVABLE_BOUND_V3SIZE );
	}
}
