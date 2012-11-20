using UnityEngine;
using System.Collections;

public class ActAutoRotated : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		gameObject.transform.Rotate( 0, 45*Time.deltaTime, 0 );
	}
}