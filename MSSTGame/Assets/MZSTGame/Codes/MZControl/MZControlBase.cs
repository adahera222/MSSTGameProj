using UnityEngine;
using System.Collections;

public interface IMZControl
{

}

public abstract class MZControlBase : IMZBaseBehavoir
{
	public string name = "";
	public bool isRunOnce = false;
	public float duration = -1;
	public IMZControl controlDelegate = null;

	//

	bool _isActive;
	float _lifeTimeCount;

	public bool isActive
	{ get { return _isActive; } }

	public float lifeTimeCount
	{ get { return _lifeTimeCount; } }

	//

	public virtual void Clear()
	{
		_isActive = false;
		_lifeTimeCount = 0;
		isRunOnce = false;
		duration = -1;
		controlDelegate = null;
	}

	public virtual void Enable()
	{
		_isActive = true;
		_lifeTimeCount = 0;
	}

	public virtual void Disable()
	{

	}

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

	public virtual bool ActiveCondition()
	{
		if( duration == -1 )
			return true;

		return ( _lifeTimeCount <= duration );
	}

	//

	protected abstract void UpdateWhenActive();

	protected virtual void FirstUpdate()
	{
	}
}