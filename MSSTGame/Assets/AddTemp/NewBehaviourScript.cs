using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		gameObject.transform.Rotate( 0, 0, Time.deltaTime*45 );
	}
}
