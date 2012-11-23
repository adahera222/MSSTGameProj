using UnityEngine;
using System.Collections;

public class MZPlayer : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		if( Input.GetMouseButton( 0 ) )
		{
			Vector3 pos = Camera.mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
			gameObject.transform.position = new Vector3( pos.x, pos.y, -30 );
		}
	}
}
