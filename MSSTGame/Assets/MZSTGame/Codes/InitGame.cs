using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MZGameCore;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		InitAnimations();
	}

	void Update()
	{

	}

	void InitAnimations()
	{
//		spritesheetNames.Add( "Spritesheets/[test]enemies_atlas" );
//		spritesheetNames.Add( "Spritesheets/[test]atlas2" );

//		foreach( string spritesheetName in spritesheetNames )
//		{
//			MZOTAnimationsManager.GetInstance().AddContainter( spritesheetName );
//		}

		MZOTAnimationsManager.GetInstance().AddContainter( "" );
	}

	void InitPlayer()
	{

	}
}
