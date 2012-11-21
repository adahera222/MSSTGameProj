using UnityEngine;
using System.Collections;
using MZGameCore;
using MZUnitySupport;

public class MZBaseObject : MonoBehaviour
{
	void Start()
	{
		GameObject cloneSprite = MZResources.InstantiateOrthelloSprite( "AnimatingSprite" );
		cloneSprite.transform.parent = gameObject.transform;
		cloneSprite.GetComponent<OTAnimatingSprite>().name = "aaa"; // set name via OTAnimatingSprite
		cloneSprite.GetComponent<OTAnimatingSprite>().animation = OTAnimationsManager.GetInstance().otAnimation;
		cloneSprite.GetComponent<OTAnimatingSprite>().Play( "Donut_normal" );
		MZDebug.Log( cloneSprite.name );
	}

	void Update()
	{

	}
}
