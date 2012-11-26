using UnityEngine;
using System.Collections;

public class MZEnemy : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		gameObject.GetComponent<MZCharacter>().position += new Vector2( 0, -Time.deltaTime*80 );
	}
}
