using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFormationState
{
	public int switchToNext = 10;
	//
	int _switchToNextCount = 0;
	int _probabilitiesDenominator = 0;
	Dictionary<MZFormation.Type,int> _probabilitiesDictionary;
	//
	public bool hasSwitchToNext
	{ get { return ( _switchToNextCount >= switchToNext ); } }
	//
	public MZFormationState()
	{
		_probabilitiesDictionary = new Dictionary<MZFormation.Type,int>();
	}

	public void SetProbability(MZFormation.Type type, int probability)
	{
		MZDebug.Assert( _probabilitiesDictionary != null, "why _probabilitiesDictionary is null???" );

		if( _probabilitiesDictionary.ContainsKey( type ) == false )
			_probabilitiesDictionary.Add( type, 0 );

		_probabilitiesDictionary[ type ] = probability;

		_probabilitiesDenominator = 0;
		foreach( int p in _probabilitiesDictionary.Values )
			_probabilitiesDenominator += p;
	}

	public MZFormation.Type GetNewFormationType()
	{
		MZDebug.Assert( _probabilitiesDenominator > 0, "_probabilitiesDenominator = " + _probabilitiesDenominator.ToString() );

		if( _probabilitiesDenominator == 0 )
			return MZFormation.Type.Unknow;

		int i = MZMath.RandomFromRange( 1, _probabilitiesDenominator );

		foreach( MZFormation.Type type in _probabilitiesDictionary.Keys )
		{
			int p = _probabilitiesDictionary[ type ];
			int next = p - i;

			if( next <= 0 )
				return type;

			i = next;
		}

		MZDebug.Assert( false, "you cannot pass" );
		return MZFormation.Type.Unknow;
	}

	public void Reset()
	{
		_switchToNextCount = 0;
	}
}
