using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.Rotate (10 * Time.deltaTime, 0, 0);
	}
}
