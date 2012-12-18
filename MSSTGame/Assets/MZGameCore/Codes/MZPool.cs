using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZPool <T> where T : class
{
	public delegate T CreateNewObject();

	public delegate void OnGetValidItem(T item);

	public delegate void OnReturnItem(T item);
	//
	public string name;
	//
	public CreateNewObject createNewObjectHandle = null;
	public OnGetValidItem onGetValidItemHandle = null;
	public OnReturnItem onReturnItemHandle = null;
	//
	int _currentIndex = 0;
	int _number = 0;
	int _maxUseIndex = 0;
	List<T> _items;
	List<bool> _actives;

	public void CreateContent(int number)
	{
		_number = number;

		_items = new List<T>();
		_actives = new List<bool>();

		for( int i = 0; i < _number; i++ )
		{
			MZDebug.Assert( createNewObjectHandle != null, "createNewObjectHandle is null" );

			T item = createNewObjectHandle();
			MZDebug.Assert( item != null, "new object is null" );

			_items.Add( item );
			_actives.Add( false );
		}
	}

	public T GetValidItem()
	{
		if( _items == null )
			return null;

//		for( int  i = 0; i < _list.Count; i++ )
//		{
//			if( _actives[ i ] == false )
//			{
//				if( i > _maxUseIndex )
//					_maxUseIndex = i;
//
//				if( onGetHandle != null )
//					onGetHandle( _list[ i ] );
//
//				_actives[ i ] = true;
//				return _list[ i ];
//			}
//		}
//
//		MZDebug.AssertFalse( "no more vaild item" );
//		return null;

		int searchIndex = _currentIndex;
		int loopCount = 0;

		while(_actives[ searchIndex ] != false)
		{
			searchIndex++;
			loopCount++;

			if( searchIndex > _maxUseIndex )
				_maxUseIndex = searchIndex;

			if( searchIndex >= _items.Count )
				searchIndex = 0;

			if( loopCount >= _items.Count )
			{
				MZDebug.AssertFalse( "no more vaild item, item number=" + _items.Count );
				return null;
			}
		}

		_actives[ searchIndex ] = true;

		if( onGetValidItemHandle != null )
			onGetValidItemHandle( _items[ searchIndex ] );

		_currentIndex = ( ( searchIndex + 1 ) < _items.Count )? searchIndex + 1 : 0;

		return _items[ searchIndex ];
	}

	public void Return(T item)
	{
		MZDebug.Assert( _items.Contains( item ) == true, "this item is not my business :D, item: " + item.ToString() );

		if( onReturnItemHandle != null )
			onReturnItemHandle( item );

		int retunrIndex = _items.IndexOf( item );
		_actives[ retunrIndex ] = false;
	}
}