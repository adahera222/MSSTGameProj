using UnityEngine;
using System.Collections;

public class MZGameComponents : MZSingleton<MZGameComponents>
{
	MZCharactersManager _charactersManager = null;
	MZRankControl _rankControl = null;

	//
	public MZCharactersManager charactersManager
	{
		set{ _charactersManager = value; }
		get{ return _charactersManager; }
	}

	public int rank
	{ get { return ( _rankControl != null )? _rankControl.rank : 0; } }

	public MZRankControl rankControl
	{
		set{ _rankControl = value; }
		get{ return _rankControl; }
	}
}
