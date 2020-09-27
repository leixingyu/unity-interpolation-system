using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static Quaternion MayaToUnityQuater(Vector3 rotation)
	{
		Vector3 flippedRotation = new Vector3(rotation.x, -rotation.y, -rotation.z); // flip Y and Z axis for right->left handed conversion; convert XYZ to ZYX
		Quaternion qx = Quaternion.AngleAxis(flippedRotation.x, Vector3.right);
		Quaternion qy = Quaternion.AngleAxis(flippedRotation.y, Vector3.up);
		Quaternion qz = Quaternion.AngleAxis(flippedRotation.z, Vector3.forward);
		Quaternion qq = qz * qy * qx; // this is the order
		return qq;
	}

	public static Vector3 MayaToUnityEuler(Vector3 rotation)
	{
		Vector3 rotOut = MayaToUnityQuater(rotation).eulerAngles;
		if (rotOut.x > 180) rotOut.x -= 360;
		if (rotOut.y > 180) rotOut.y -= 360;
		if (rotOut.z > 180) rotOut.z -= 360;
		return rotOut;
	}
}
