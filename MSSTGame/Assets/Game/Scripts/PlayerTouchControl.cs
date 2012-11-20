using UnityEngine;
using System.Collections;

public class PlayerTouchControl : MonoBehaviour
{
	void Start()
	{
		Input.multiTouchEnabled = true;
	}

	void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -Camera.mainCamera.transform.position.z;

		Vector3 worldPosition = Camera.mainCamera.ScreenToWorldPoint( mousePosition );

		OTSprite ost = (OTSprite)gameObject.GetComponent( typeof( OTSprite ) );
		ost.position = new Vector2( worldPosition.x, worldPosition.y );
	}
}
