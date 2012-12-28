using UnityEngine;
using System.Collections;

public class MZRankControl : MZControlBase
{
	public int rank
	{ get { return _rank; } }

	//
	float rankUpTime = 5;

	//
	int _rank = 0;
	float _rankTimeCount = 0;

	//
	protected override void UpdateWhenActive()
	{
		_rankTimeCount += MZTime.deltaTime;

		if( _rankTimeCount >= rankUpTime )
		{
			_rank++;
			_rankTimeCount = 0;
			MZDebug.Log( "RANK UP: " + _rank.ToString() );
		}
	}
}
