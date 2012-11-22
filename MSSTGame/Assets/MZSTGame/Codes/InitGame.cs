using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlistCS;

public class InitGame : MonoBehaviour
{
	public List<string> spritesheetNames = new List<string>();

	void Start()
	{
		MZOTAnimationsManager.GetInstance().CreateAnimationsByExistedContainer();
	}

	void Update()
	{

	}

	void InitPlayer()
	{

	}
}
