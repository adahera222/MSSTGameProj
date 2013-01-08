using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFormationState
{
	public int exp = 0;
	public int expLimited = 10;
	//
	int _probabilitiesDenominator = 0;
	string _name;
	Dictionary<MZFormation.SizeType,int> _probabilitiesDictionary;
	//
	public bool hasExpToLimited
	{ get { return ( exp >= expLimited ); } }

	public string name
	{ get { return _name; } }
	//
	public MZFormationState(string name)
	{
		_name = name;
		_probabilitiesDictionary = new Dictionary<MZFormation.SizeType,int>();
	}

	public void SetProbability(MZFormation.SizeType type, int probability)
	{
		MZDebug.Assert( _probabilitiesDictionary != null, "why _probabilitiesDictionary is null???" );

		if( _probabilitiesDictionary.ContainsKey( type ) == false )
			_probabilitiesDictionary.Add( type, 0 );

		_probabilitiesDictionary[ type ] = probability;

		_probabilitiesDenominator = 0;
		foreach( int p in _probabilitiesDictionary.Values )
			_probabilitiesDenominator += p;
	}

	public MZFormation.SizeType GetNewFormationType()
	{
		MZDebug.Assert( _probabilitiesDenominator > 0, "_probabilitiesDenominator = " + _probabilitiesDenominator.ToString() );

		if( _probabilitiesDenominator == 0 )
			return MZFormation.SizeType.Unknow;

		int i = MZMath.RandomFromRange( 1, _probabilitiesDenominator );

		foreach( MZFormation.SizeType type in _probabilitiesDictionary.Keys )
		{
			int p = _probabilitiesDictionary[ type ];
			int next = i - p;

			if( next <= 0 )
				return type;

			i = next;
		}

		foreach( MZFormation.SizeType t in _probabilitiesDictionary.Keys )
		{
			MZDebug.Log( t.ToString() );
		}

		MZDebug.Assert( false, "you cannot pass, i/d=" + i.ToString() + "/" + _probabilitiesDenominator );
		return MZFormation.SizeType.Unknow;
	}

	public void Reset()
	{
		exp = 0;
	}
}
