using UnityEngine;
using System.Collections.Generic;

public class MZControlUpdate<T> where T : MZControlBase
{
	public List<T> controlsList
	{
		get
		{
			if( _controlsList == null )
				_controlsList = new List<T>();

			return _controlsList;
		}
	}

	public T currentControl
	{
		get{ return controlsList[ _currentIndex ]; }
	}

	int _currentIndex = -1;
	List<T> _controlsList = null;

	public MZControlUpdate()
	{

	}

	public void Clear()
	{
		MZDebug.Log( ">M<" );
		_currentIndex = -1;
		_controlsList.Clear();
		_controlsList = null;
	}

	public MZControlUpdate(List<T> controlsList)
	{
		_controlsList = controlsList;
	}

	public int Add(T control)
	{
		controlsList.Add( control );
		return controlsList.Count;
	}

	public void ResetAll()
	{
		_currentIndex = -1;

		foreach( T control in controlsList )
			control.Reset();
	}

	public void Update()
	{
		if( _controlsList == null || _controlsList.Count == 0 )
			return;

		if( _currentIndex == -1 )
		{
			_currentIndex = 0;
			_controlsList[ _currentIndex ].Reset();
		}

		_controlsList[ _currentIndex ].Update();

		if( _controlsList[ _currentIndex ].isActive == false )
		{
			// need support use pre-setting ...

			if( _controlsList[ _currentIndex ].isRunOnce == true )
			{
				_controlsList.RemoveAt( _currentIndex );
				_currentIndex--;
			}

			_currentIndex = ( _currentIndex >= _controlsList.Count - 1 )? 0 : _currentIndex + 1;
			_controlsList[ _currentIndex ].Reset();
		}
	}
}

