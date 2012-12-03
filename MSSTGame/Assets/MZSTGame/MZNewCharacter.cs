using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZNewCharacter : MonoBehaviour, IMZMove, IMZRemove
{
	public bool isActive
	{ get { return _isActive; } }

	public MZCharacterType characterType
	{
		set{ _characterType = value; }
		get{ return _characterType; }
	}

	public Vector2 position
	{
		set{ gameObject.transform.position = new Vector3( value.x, value.y, gameObject.transform.position.z ); }
		get{ return new Vector2( gameObject.transform.position.x, gameObject.transform.position.y ); }
	}

	bool _isActive;
	float _lifeTimeCount = 0;

	MZCharacterType _characterType = MZCharacterType.Unknow;
	MZRemove_OutOfBound removeOutOfBound = null;

	#region IMZRemove implementation
	public void DoRemoveOutOfBound()
	{
		Disable();
	}

	public float lifeTimeCount
	{
		get
		{
			return _lifeTimeCount;
		}
	}

	public Vector2 frameSize
	{
		get
		{
			return new Vector2( 10, 10 );
		}
	}
	#endregion

	public void Disable()
	{
		_isActive = false;
	}

	void Start()
	{
		_isActive = true;
		_lifeTimeCount = 0;
		removeOutOfBound = new MZRemove_OutOfBound();
		removeOutOfBound.controlTarget = this;
	}

	void Update()
	{
		_lifeTimeCount += Time.deltaTime;
		removeOutOfBound.Update();
	}
}