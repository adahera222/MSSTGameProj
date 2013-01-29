using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PositionType = MZFormation.PositionType;
using SizeType = MZFormation.SizeType;

public class MZFormationsManager : MZControlBase
{
	public bool enableUpdateState = true;
	public MZRankControl rankControl = null;

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
		MZFormationsLoad.SetFormationStates( this );
		MZFormationsLoad.SetFormations( this );

		_nextCreateTimeCount = 0;
		_currentFormationStatesIndex = 0;
		_currentFormationsList = new List<MZFormation>();

		ResetPositionTypeOrder();
	}

	public void ExecuteFormation(PositionType posType, SizeType sizeType, int constructCode, string name)
	{
		List<MZFormation> list = _formationsBySizeDictionary[ sizeType ][ posType ];
		foreach( MZFormation f in list )
		{
			if( f.GetType().ToString() == name && posType == f.positionType && f.constructCode == constructCode )
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
		MZDebug.Assert( rankControl != null, "rankControl is null" );

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

		if( MZGameSetting.SHOW_FORMATION_LOG )
			MZDebug.Log( _currentPositionTypeOrder[ 0 ].ToString() + ", " + _currentPositionTypeOrder[ 1 ].GetType().ToString() + ", " + _currentPositionTypeOrder[ 2 ].ToString() );

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

			MZFormation newFormation = GetFormationByDecision( newCreateSizeType );

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
				if( MZGameSetting.SHOW_FORMATION_LOG )
					MZDebug.Log( "remove: name={0}, xp={1}, pos={2}, size={3}", _currentFormationsList[ i ].GetType().ToString(), _currentFormationsList[ i ].stateExp,
						_currentFormationsList[ i ].positionType, _currentFormationsList[ i ].sizeType );

				rankControl.playerRankXp += ( _currentFormationsList[ i ].isAllMembersTakenDownByPlayer )? 1 : 0;
				_currentFormationsList.RemoveAt( i );
			}
		}
	}

	void ExecuteFormation(MZFormation formation)
	{
		MZDebug.Assert( formation != null, "formation is null" );

		if( _currentFormationsList == null )
		{
			_currentFormationsList = new List<MZFormation>();
		}

		if( _currentFormationsList.Contains( formation ) == false )
		{
			if( formation.positionType == PositionType.Any )
				ResetPositionTypeOrder();

			formation.rank = rankControl.enemyRank;
			formation.Enable();
			_currentFormationsList.Add( formation );

			if( MZGameSetting.SHOW_FORMATION_LOG )
				MZDebug.Log( "add: name={0}, xp={1}, pos={2}, size={3}", formation.ToString(), formation.stateExp, formation.positionType, formation.sizeType );

			_currentFormationState.exp += formation.stateExp;
			rankControl.enemyRankXp += 1;
		}
		else
		{
			ResetPositionTypeOrder();
		}
	}

	MZFormation GetFormationByDecision(SizeType sizeType)
	{
		MZDebug.Assert( _formationsBySizeDictionary != null, "_formationsBySizeDictionary is null" );
		MZDebug.Assert( _formationsBySizeDictionary.ContainsKey( sizeType ), "not contain size=" + sizeType.ToString() );

		bool isPosAny = ( MZMath.RandomFromRange( 0, 4 ) == 0 );

		Dictionary<PositionType, List<MZFormation>> listByPos = _formationsBySizeDictionary[ sizeType ];

		PositionType choicePosType = PositionType.Unknow;

		if( isPosAny && listByPos.ContainsKey( PositionType.Any ) )
		{
			choicePosType = PositionType.Any;
		}
		else
		{
			int selectCount = 0;
			while(selectCount < _currentPositionTypeOrder.Count)
			{
				PositionType tryToChoicePosType = GetNextPositionType();
				if( listByPos.ContainsKey( tryToChoicePosType ) == true )
				{
					choicePosType = tryToChoicePosType;
					break;
				}
				selectCount++;
			}

			if( choicePosType == PositionType.Unknow )
				choicePosType = ( listByPos.ContainsKey( PositionType.Any ) )? PositionType.Any : PositionType.Unknow;
		}

		return ( choicePosType != PositionType.Unknow )? GetRandomInFormationList( listByPos[ choicePosType ] ) : null;
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
