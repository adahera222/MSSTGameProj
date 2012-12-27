using UnityEngine;
using System.Collections;

public interface IMZControl
{

}

public abstract class MZControlBase
{
	public string name = "";
	public bool isRunOnce = false;
	public float duration = -1;
	public IMZControl controlDelegate = null;

	public bool isActive
	{ get { return _isActive; } }

	public float lifeTimeCount
	{ get { return _lifeTimeCount; } }

	public void Update()
	{
		if( _lifeTimeCount == 0 )
			FirstUpdate();

		_lifeTimeCount += MZTime.deltaTime;
		_isActive = ActiveCondition();

		if( _isActive == false )
			return;

		UpdateWhenActive();
	}

	public virtual void Reset()
	{
		_isActive = true;
		_lifeTimeCount = 0;
	}

	public virtual bool ActiveCondition()
	{
		if( duration == -1 )
			return true;

		return ( _lifeTimeCount <= duration );
	}

	bool _isActive = true;
	float _lifeTimeCount = -1;

	protected abstract void UpdateWhenActive();

	protected virtual void FirstUpdate()
	{
	}
}