using System;

interface IMZBaseBehavoir
{
	void Clear();
//	void Reset();
//	void SetBehavoir();
	void Enable();
	void Disable();
//	void Update(); <-- Conflict with mono (mono is protected)
}
