using System.Collections;
using System.Collections.Generic;
using Tanks3D;
using UnityEngine;

namespace Examples
{
	public class ExtensionTest : MonoBehaviour
	{
		[SerializeField]
		private Collider _collider;

		[SerializeField]
		private bool _includeInactive;

		public void Run()
		{
			_collider = gameObject.GetComponentInHierarchy< Collider >( _includeInactive );

			if(_collider != null)
			{
				Debug.Log( "Collider found!" );
			}
			else
			{
				Debug.Log( "Could not found the collider from the hierarchy!" );
			}
		}
	}
}
