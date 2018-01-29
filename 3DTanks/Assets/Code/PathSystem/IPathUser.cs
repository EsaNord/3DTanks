using UnityEngine;
using System.Collections;

namespace Tanks3D.WaypointSystem
{ 
	public interface IPathUser
	{
		Waypoint CurrentWaypoint { get; }
		Direction Direction { get; set; }
		Vector3 Position { get; set; }
	}
}
