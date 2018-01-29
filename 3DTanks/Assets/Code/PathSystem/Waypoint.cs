using UnityEngine;
using System.Collections;

namespace Tanks3D.WaypointSystem
{
	public class Waypoint : MonoBehaviour
	{
		public Vector3 Position
		{
			get { return transform.position; }
		}
	}
}
