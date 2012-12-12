using UnityEngine;
using System.Collections;

public class MZPlayer : MZCharacter
{
	Rect playMovableBound = MZGameSetting.GetPlayerMovableBoundRect();
	float dragableRadius = 150;
	GameObject dragRange;
	Vector3 positonOnTouchBegan;
	Vector3 playerPositionOnTouchBegan;
	MZAttack_Base attackTemp = null;

	enum ControlState
	{
		None,
		Move,
		Teleport,
	}

	ControlState currentControlState = ControlState.None;

	public override void Enable()
	{
		base.Enable();

		dragRange = GameObject.Find( "TestDragableRange" );
		if( dragRange != null )
			dragRange.transform.localScale = new Vector3( dragableRadius*2, 0, dragableRadius*2 );

		attackTemp = new MZAttack_OddWay();
		attackTemp.numberOfWays = 5;
		attackTemp.initVelocity = 1000;
		attackTemp.strength = 1;
		attackTemp.intervalDegrees = 5;
		attackTemp.colddown = 0.25f;
		attackTemp.duration = -1;
		attackTemp.bulletSettingName = "PlayerBullet001Setting";
		attackTemp.enable = false;
//		attackTemp.controlTarget = partsByNameDictionary[ "MainBody" ];
		attackTemp.SetTargetHelp( new MZTargetHelp_AssignMovingVector( new Vector2( 0, 1 ) ) );
	}

	public override void Clear()
	{
		base.Clear();
	}

	protected override void Update()
	{
		base.Update();

		UpdateOnTouchBegan();
		UpdateOnTouchMoved();
		UpdateOnTouchEnded();
		attackTemp.Update();

		UpdateTest();
	}

	void UpdateOnTouchBegan()
	{
		if( !Input.GetMouseButtonDown( 0 ) )
			return;

		positonOnTouchBegan = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
		playerPositionOnTouchBegan = gameObject.gameObject.transform.position;
		currentControlState = ( MZMath.V3ToV2DistancePow2( positonOnTouchBegan, gameObject.transform.position ) > dragableRadius*dragableRadius )? ControlState.Teleport : ControlState.Move;

		if( attackTemp.controlTarget == null )
			attackTemp.controlTarget = partsByNameDictionary[ "MainBody" ];

		attackTemp.enable = true;
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
		attackTemp.enable = false;
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

	void UpdateTest()
	{
		if( dragRange == null )
			return;

		dragRange.transform.Rotate( 0, 45*Time.deltaTime, 0 );
		dragRange.transform.position = gameObject.transform.position;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere( gameObject.transform.position, dragableRadius );
	}
}