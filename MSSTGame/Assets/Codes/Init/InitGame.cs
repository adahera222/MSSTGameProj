using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MZUnitySupport;
using MZGameCore;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string> ();

	void Start ()
	{
		spritesheetNames.Add ("Spritesheets/[test]enemies_atlas");
		spritesheetNames.Add ("Spritesheets/[test]atlas2");
		
		foreach (string spritesheetName in spritesheetNames) {
			OTAnimationsManager.GetInstance ().AddContainter (spritesheetName);
		}
	}

	void Update ()
	{
		
	}
}
