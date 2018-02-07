using UnityEngine;

namespace Tanks3D
{
	public interface ICameraFollow
	{
		void SetAngle( float angle );
		void SetDistance( float distance );
		void SetTarget( Transform targetTransform );
	}
}
