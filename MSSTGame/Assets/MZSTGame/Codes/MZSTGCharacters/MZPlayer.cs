using UnityEngine;
using System.Collections;

public class MZPlayer : MZCharacter
{
	enum ControlState
	{
		None,
		Move,
		Teleport,
	}

	Rect playMovableBound = MZGameSetting.GetPlayerMovableBoundRect();
	float dragableRadius = 150;
	GameObject dragRange;
	Vector3 positonOnTouchBegan;
	Vector3 playerPositionOnTouchBegan;
	ControlState currentControlState = ControlState.None;
	MZAttack[] _sideAttacks = new MZAttack[6];
	MZMove sideMoveL;
	MZMove sideMoveR;

	//

	public override void Enable()
	{
		base.Enable();

		dragRange = GameObject.Find( "TestDragableRange" );
		if( dragRange != null )
			dragRange.transform.localScale = new Vector3( dragableRadius*2, 0, dragableRadius*2 );
	}

	public override void OnRemoving()
	{
		base.OnRemoving();
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		_sideAttacks[ 0 ] = GetAttackToPart( "Option1", 90, 1 );
		_sideAttacks[ 1 ] = GetAttackToPart( "Option2", 90, 1 );

		_sideAttacks[ 2 ] = GetAttackToPart( "Option3", 120, 1 );
		_sideAttacks[ 3 ] = GetAttackToPart( "Option4", 130, 1 );
		_sideAttacks[ 4 ] = GetAttackToPart( "Option5", 50, 1 );
		_sideAttacks[ 5 ] = GetAttackToPart( "Option6", 60, 1 );

		sideMoveL = GetMoveToPart( "Option1", true );
		sideMoveR = GetMoveToPart( "Option2", false );
	}

	protected override void Update()
	{
		base.Update();

		UpdateOnTouchBegan();
		UpdateOnTouchMoved();
		UpdateOnTouchEnded();

		sideMoveL.Update();
		sideMoveR.Update();

		for( int i = 0; i < 6; i++ )
			_sideAttacks[ i ].Update();

		UpdateTest();
	}

	void UpdateOnTouchBegan()
	{
		if( !Input.GetMouseButtonDown( 0 ) )
			return;

		positonOnTouchBegan = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
		playerPositionOnTouchBegan = gameObject.gameObject.transform.position;
		currentControlState = ( MZMath.V3ToV2DistancePow2( positonOnTouchBegan, gameObject.transform.position ) > dragableRadius*dragableRadius )? ControlState.Teleport : ControlState.Move;

		for( int i = 0; i < 6; i++ )
			_sideAttacks[ i ].enable = true;
	}

	void UpdateOnTouchMoved()
	{
		if( !Input.GetMouseButton( 0 ) )
			return;

		Vector3 positonOnTouchMoved = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );

		if( currentControlState == ControlState.Move )
		{
			Vector3 nextPosition = playerPositionOnTouchBegan + ( positonOnTouchMoved - positonOnTouchBegan );
			position = GetModifyNextPositionInBound( nextPosition );
		}

		if( currentControlState == ControlState.Teleport )
		{
			position = GetModifyNextPositionInBound( positonOnTouchMoved );
		}
	}

	void UpdateOnTouchEnded()
	{
		if( !Input.GetMouseButtonUp( 0 ) )
			return;

		currentControlState = ControlState.None;
		for( int i = 0; i < 6; i++ )
			_sideAttacks[ i ].enable = false;
	}

	Vector2 GetModifyNextPositionInBound(Vector3 nextPosition)
	{
		if( nextPosition.x <= playMovableBound.x )
			nextPosition.x = playMovableBound.x;

		if( nextPosition.x >= playMovableBound.x + playMovableBound.width )
			nextPosition.x = playMovableBound.x + playMovableBound.width;

		if( nextPosition.y <= playMovableBound.y - playMovableBound.height )
			nextPosition.y = playMovableBound.y - playMovableBound.height;

		if( nextPosition.y >= playMovableBound.y )
			nextPosition.y = playMovableBound.y;

		return new Vector2( nextPosition.x, nextPosition.y );
	}

	MZAttack GetAttackToPart(string partName, float dir, int way)
	{
		MZAttack_OddWay attack = MZAttack.Create<MZAttack_OddWay>( "test", partsByNameDictionary[ partName ] );
		attack.disableRankEffect = true;
		attack.numberOfWays = way;
		attack.initVelocity = 1500;
		attack.strength = 1;
		attack.intervalDegrees = 15.0f;
		attack.colddown = 0.1f;
		attack.duration = -1;
		attack.bulletName = "PB000";
		attack.enable = false;
		attack.targetHelp = new MZTargetHelp_AssignDirection();
		( attack.targetHelp as MZTargetHelp_AssignDirection ).direction = dir;

		return attack;
	}

	MZMove GetMoveToPart(string partName, bool angularVelocityFlag)
	{
		MZCharacterPart part = partsByNameDictionary[ partName ];

		float angularVelocityBase = 270;
		MZMove_Rotation move = MZMove.Create<MZMove_Rotation>( "r", part );
		move.targetHelp.assignPosition = Vector2.zero;
		move.angularVelocity = angularVelocityBase*( ( angularVelocityFlag )? -1 : 1 );

		return move;
	}

	void UpdateTest()
	{
		if( dragRange == null )
			return;

		dragRange.transform.Rotate( 0, 45*MZTime.deltaTime, 0 );
		dragRange.transform.position = gameObject.transform.position;
	}

	void OnDrawGizmos()
	{
		if( MZGameSetting.SHOW_GIZMOS == false )
			return;
		
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere( gameObject.transform.position, dragableRadius );
	}
}