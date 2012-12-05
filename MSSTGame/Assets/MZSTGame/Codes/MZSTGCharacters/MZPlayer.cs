using UnityEngine;
using System.Collections;

public class MZPlayer : MonoBehaviour
{
	float interval = 0.2f;
	float cd = 0;
	Rect playMovableBound = MZGameSetting.GetPlayerMovableBoundRect();
	float dragableRadius = 150;
	GameObject dragRange;
	Vector3 positonOnTouchBegan;
	Vector3 playerPositionOnTouchBegan;

	enum ControlState
	{
		None,
		Move,
		Teleport,
	}

	ControlState currentControlState = ControlState.None;

	void Start()
	{
		dragRange = GameObject.Find( "TestDragableRange" );
		if( dragRange != null )
			dragRange.transform.localScale = new Vector3( dragableRadius*2, 0, dragableRadius*2 );
	}

	void Update()
	{
		UpdateOnTouchBegan();
		UpdateOnTouchMoved();
		UpdateOnTouchEnded();
//		UpdateAttack();

		UpdateTest();
	}

	void UpdateOnTouchBegan()
	{
		if( !Input.GetMouseButtonDown( 0 ) )
			return;

		positonOnTouchBegan = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
		playerPositionOnTouchBegan = gameObject.gameObject.transform.position;
		currentControlState = ( MZMath.V3ToV2DistancePow2( positonOnTouchBegan, gameObject.transform.position ) > dragableRadius*dragableRadius )? ControlState.Teleport : ControlState.Move;
	}

	void UpdateOnTouchMoved()
	{
		if( !Input.GetMouseButton( 0 ) )
			return;

		Vector3 positonOnTouchMoved = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );

		if( currentControlState == ControlState.Move )
		{
			Vector3 nextPosition = playerPositionOnTouchBegan + ( positonOnTouchMoved - positonOnTouchBegan );

			nextPosition = GetModifyNextPositionInBound( nextPosition );
			gameObject.transform.position = nextPosition;
		}

		if( currentControlState == ControlState.Teleport )
		{
			gameObject.transform.position = GetModifyNextPositionInBound( positonOnTouchMoved );
		}
	}

	void UpdateOnTouchEnded()
	{
		if( !Input.GetMouseButtonUp( 0 ) )
			return;

		currentControlState = ControlState.None;
	}

	void UpdateAttack()
	{
		if( !Input.GetMouseButton( 0 ) )
			return;

		cd -= Time.deltaTime;
		if( cd <= 0 )
		{
			GameObject pb = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.PlayerBullet, "PlayerBullet", "PlayerBullet001Setting" );
			pb.GetComponent<MZCharacter>().position = gameObject.GetComponent<MZCharacter>().position;
			cd += interval;
		}
	}

	Vector3 GetModifyNextPositionInBound(Vector3 nextPosition)
	{
		if( nextPosition.x <= playMovableBound.x )
			nextPosition.x = playMovableBound.x;

		if( nextPosition.x >= playMovableBound.x + playMovableBound.width )
			nextPosition.x = playMovableBound.x + playMovableBound.width;

		if( nextPosition.y <= playMovableBound.y - playMovableBound.height )
			nextPosition.y = playMovableBound.y - playMovableBound.height;

		if( nextPosition.y >= playMovableBound.y )
			nextPosition.y = playMovableBound.y;

		nextPosition.z = MZGameSetting.GetCharacterDepth( MZCharacterType.Player );

		return nextPosition;
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
