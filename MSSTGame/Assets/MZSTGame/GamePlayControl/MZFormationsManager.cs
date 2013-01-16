using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PositionType = MZFormation.PositionType;
using SizeType = MZFormation.SizeType;

public class MZFormationsManager : MZControlBase
{
	public bool enableUpdateState = true;

	public List<MZFormation> formations
	{
		get
		{
			if( _formationsBySizeDictionary == null )
				return null;

			List<MZFormation> formations = new List<MZFormation>();

			foreach( Dictionary<PositionType, List<MZFormation>> listByPos in _formationsBySizeDictionary.Values )
			{
				foreach( List<MZFormation> list in listByPos.Values )
					foreach( MZFormation f in list )
						formations.Add( f );
			}

			return formations;
		}
	}

	//

	float _nextCreateTimeCount;
	List<MZFormation> _currentFormationsList;
	int _currentPositionTypeOrderIndex;
	List<PositionType> _currentPositionTypeOrder;
	Dictionary<SizeType, Dictionary<PositionType, List<MZFormation>>> _formationsBySizeDictionary;
	int _currentFormationStatesIndex;
	List<MZFormationState> _formationStatesList;

	//

	MZFormationState _currentFormationState
	{
		get
		{
			MZDebug.Assert( _formationStatesList != null && _formationStatesList.Count > 0, "_formationStatesList not init" );
			MZDebug.Assert( 0 <= _currentFormationStatesIndex && _currentFormationStatesIndex < _formationStatesList.Count, "index error" );

			return _formationStatesList[ _currentFormationStatesIndex ];
		}
	}

	public MZFormationsManager()
	{
		MZFormationsLoader.SetFormationStates( this );
		MZFormationsLoader.SetFormations( this );

		_nextCreateTimeCount = 0;
		_currentFormationStatesIndex = 0;
		_currentFormationsList = new List<MZFormation>();

		ResetPositionTypeOrder();
	}

	public void ExecuteFormation(PositionType posType, SizeType sizeType, string name)
	{
		List<MZFormation> list = _formationsBySizeDictionary[ sizeType ][ posType ];
		foreach( MZFormation f in list )
		{
			if( f.GetType().ToString() == name && posType == f.positionType )
			{
				ExecuteFormation( f );
				break;
			}
		}
	}

	public MZFormationState AddFormationState(string name)
	{
		if( _formationStatesList == null )
			_formationStatesList = new List<MZFormationState>();

		MZFormationState state = new MZFormationState( name );
		_formationStatesList.Add( state );

		return state;
	}

	public void AddFormation(SizeType sizeType, PositionType positionType, MZFormation formation)
	{
		if( _formationsBySizeDictionary == null )
			_formationsBySizeDictionary = new Dictionary<SizeType, Dictionary<PositionType, List<MZFormation>>>();

		if( _formationsBySizeDictionary.ContainsKey( sizeType ) == false )
		{
			Dictionary<PositionType, List<MZFormation>> newDic = new Dictionary<PositionType, List<MZFormation>>();
			_formationsBySizeDictionary.Add( sizeType, newDic );
		}

		if( _formationsBySizeDictionary[ sizeType ].ContainsKey( positionType ) == false )
		{
			List<MZFormation> newList = new List<MZFormation>();
			_formationsBySizeDictionary[ sizeType ].Add( positionType, newList );
		}

		formation.sizeType = sizeType;
		formation.positionType = positionType;

		_formationsBySizeDictionary[ sizeType ][ positionType ].Add( formation );
	}

	//

	protected override void UpdateWhenActive()
	{
		if( enableUpdateState )
		{
			UpdateFormationState();
			UpdateFormationCreate();
		}

		UpdateCurrentFormations();
	}

	//

	void ResetPositionTypeOrder()
	{
		List<PositionType> initValues = new List<PositionType>();
		initValues.Add( PositionType.Left );
		initValues.Add( PositionType.Mid );
		initValues.Add( PositionType.Right );

		if( _currentPositionTypeOrder == null )
			_currentPositionTypeOrder = new List<PositionType>();

		if( _currentPositionTypeOrder.Count > 0 )
			_currentPositionTypeOrder.Clear();

		while(initValues.Count > 0)
		{
			int choiceIndex = MZMath.RandomFromRange( 0, initValues.Count - 1 );
			_currentPositionTypeOrder.Add( initValues[ choiceIndex ] );
			initValues.RemoveAt( choiceIndex );
		}

		MZDebug.Log( _currentPositionTypeOrder[ 0 ].ToString() + ", " + _currentPositionTypeOrder[ 1 ].ToString() + ", " + _currentPositionTypeOrder[ 2 ].ToString());

		_currentPositionTypeOrderIndex = 0;
	}

	void UpdateFormationState()
	{
		if( _currentFormationState.hasExpToLimited == true )
		{
			_currentFormationStatesIndex++;
			if( _currentFormationStatesIndex >= _formationStatesList.Count )
				_currentFormationStatesIndex = 0;

			_currentFormationState.Reset();

			MZDebug.Log( "Formation State Level Up = " + _currentFormationState.name );
		}
	}

	void UpdateFormationCreate()
	{
		_nextCreateTimeCount -= MZTime.deltaTime;

		if( _nextCreateTimeCount <= 0 )
		{
			MZFormation.SizeType newCreateSizeType = _currentFormationState.GetNewFormationType();
			MZDebug.Assert( _formationsBySizeDictionary.ContainsKey( newCreateSizeType ), "not contain this type: " + newCreateSizeType.ToString() );

			bool anyOrPos = ( MZMath.RandomFromRange( 0, 1 ) == 0 );
			MZFormation newFormation = GetFormationByDecision( newCreateSizeType, anyOrPos );

			if( newFormation == null )
				return;

			if( newFormation.disableNextFormationTime >= _nextCreateTimeCount )
				_nextCreateTimeCount = newFormation.disableNextFormationTime;

			ExecuteFormation( newFormation );
		}
	}

	void UpdateCurrentFormations()
	{
		if( _currentFormationsList == null )
			return;

		foreach( MZFormation f in _currentFormationsList )
		{
			f.Update();
		}

		for( int i = 0; i < _currentFormationsList.Count; i++ )
		{
			if( _currentFormationsList[ i ].isActive == false )
			{
				_currentFormationState.exp += _currentFormationsList[ i ].stateExp;
				_currentFormationsList.RemoveAt( i );
			}
		}
	}

	void ExecuteFormation(MZFormation formation)
	{
		MZDebug.Assert( formation != null, "formation is null" );

		if( _currentFormationsList == null )
			_currentFormationsList = new List<MZFormation>();

		if( _currentFormationsList.Contains( formation ) == false )
		{
			_currentFormationsList.Add( formation );

			formation.Enable();
		}
	}

	MZFormation GetFormationByDecision(SizeType sizeType, bool isPositionAny)
	{
		MZDebug.Assert( _formationsBySizeDictionary != null, "_formationsBySizeDictionary is null" );
		MZDebug.Assert( _formationsBySizeDictionary.ContainsKey( sizeType ), "not contain size=" + sizeType.ToString() );

		Dictionary<PositionType, List<MZFormation>> listByPos = _formationsBySizeDictionary[ sizeType ];

//		if( isPositionAny )
//		{
//			return GetRandomInFormationList( listByPos[ PositionType.Any ] );
//		}
//		else
		{
			PositionType positionType = GetNextPositionType();
			return GetRandomInFormationList( listByPos[ positionType ] );
		}
	}

	MZFormation GetRandomInFormationList(List<MZFormation> list)
	{
		return ( list != null )? list[ MZMath.RandomFromRange( 0, list.Count - 1 ) ] : null;
	}

	PositionType GetNextPositionType()
	{
		if( _currentPositionTypeOrder == null )
			ResetPositionTypeOrder();

		PositionType nextType = _currentPositionTypeOrder[ _currentPositionTypeOrderIndex ];
		_currentPositionTypeOrderIndex =
			( _currentPositionTypeOrderIndex + 1 < _currentPositionTypeOrder.Count )? _currentPositionTypeOrderIndex + 1 : 0;

		return nextType;
	}
}
