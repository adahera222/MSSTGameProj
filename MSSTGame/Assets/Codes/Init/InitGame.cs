using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MZUnitySupport;
using MZGameCore;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public GameObject testBaseObject;

	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		spritesheetNames.Add( "Spritesheets/[test]enemies_atlas" );
		spritesheetNames.Add( "Spritesheets/[test]atlas2" );

		foreach( string spritesheetName in spritesheetNames )
		{
			MZOTAnimationsManager.GetInstance().AddContainter( spritesheetName );
		}

		testBaseObject.GetComponent<MZBaseObject>().position = new Vector2( 100, 100 );
		testBaseObject.GetComponent<MZBaseObject>().depth = -50;
		testBaseObject.GetComponent<MZBaseObject>().PlayAnimation( "Donut_normal" );
		testBaseObject.GetComponent<MZBaseObject>().AnimationSpeed = 0.1f;
	}

	void Update()
	{
		
	}
}
