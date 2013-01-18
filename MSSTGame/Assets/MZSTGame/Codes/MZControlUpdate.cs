using UnityEngine;
using System.Collections.Generic;

public class MZControlUpdate<T> : IMZBaseBehavoir
	where T : MZControlBase
{
	public List<T> controlsList
	{
		get
		{
			if( _originalControlsList == null )
			{
				_originalControlsList = new List<T>();
			}

			return _originalControlsList;
		}
	}

	public T currentControl
	{
		get
		{
			MZDebug.Assert( controlsList != null, "controlsList is null" );
			return _currentControl;
		}
	}

	//

	bool _isActive;
	List<T> _originalControlsList = null;
	List<T> _executingControlsList = null;
	T _currentControl = null;

	//

	public MZControlUpdate()
	{

	}

	public void Clear()
	{
		_originalControlsList.Clear();
		_originalControlsList = null;
	}

	public MZControlUpdate(List<T> controlsList)
	{
		_originalControlsList = controlsList;
	}

	public int Add(T control)
	{
		controlsList.Add( control );
		return controlsList.Count;
	}

	public void Enable()
	{
		_isActive = true;

		_currentControl = null;

		if( _executingControlsList == null )
		{
			_executingControlsList = new List<T>();
		}

		_executingControlsList.Clear();

		if( _originalControlsList != null )
		{
			foreach( T control in _originalControlsList )
			{
				_executingControlsList.Add( control );
			}

			_currentControl = NextControl();
			_currentControl.Enable();
		}
	}

	public void Disable()
	{
		if( _currentControl != null )
			_currentControl.Disable();
		_currentControl = null;
		_isActive = false;
	}

	public void Update()
	{
		if( _isActive == false )
			return;

		if( /*disableUpdate ||*/ _executingControlsList == null )
			return;

		if( IsNeedSwitchControl() )
		{
			CheckRunOceAndRemove( _currentControl );
			_currentControl = NextControl();

			if( _currentControl != null )
			{
				_currentControl.Enable();
			}
		}

		if( _currentControl != null )
			_currentControl.Update();
	}

	bool IsNeedSwitchControl()
	{
		return ( _currentControl == null || _currentControl.isActive == false );
	}

	void CheckRunOceAndRemove(T control)
	{
		if( control == null || !control.isRunOnce )
			return;

		_executingControlsList.Remove( control );
	}

	T NextControl()
	{
		if( _executingControlsList.Count == 0 )
			return null;

		if( _currentControl == null )
			return _executingControlsList[ 0 ];

		int currentIndex = _executingControlsList.IndexOf( _currentControl );
		int nextIndex = ( currentIndex >= _executingControlsList.Count - 1 )? 0 : currentIndex + 1;

		return _executingControlsList[ nextIndex ];
	}
}

