using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathFunc
{
	public static Vector3 LinEuler(Vector3[] vectors, float t){
		Vector3 rotation = new Vector3(0, 0, 0);
		var section = (int)((vectors.Length - 1) * t);
		var alongLine = (vectors.Length - 1) * t - section;

		for (int i = section; i <= section; i++)
			rotation = Vector3.Lerp(vectors[section], vectors[section + 1], alongLine);
		return rotation;
	}

	public static Quaternion Slerp(Quaternion[] quaternions, float t){
		Quaternion rotation = Quaternion.identity;
		var section = (int)((quaternions.Length - 1) * t);
		var alongLine = (quaternions.Length - 1) * t - section;

		for (int i = section; i <= section; i++)
			rotation = SlerpNoInvert(quaternions[section], quaternions[section + 1], alongLine);
			//rotation = Quaternion.Slerp(quaternions[section], quaternions[section + 1], alongLine);
		return rotation;
	}

	public static Quaternion SlerpNoInvert(Quaternion fro, Quaternion to, float factor)	{
		float dot = Quaternion.Dot(fro, to);
		if (Mathf.Abs(dot) >= 1.0f)
			return fro;

		float theta = Mathf.Acos(dot);
		var sinT = 1.0f / Mathf.Sin(theta);
		var newFactor = Mathf.Sin(factor * theta) * sinT;
		var invFactor = Mathf.Sin((1.0f - factor) * theta) * sinT;

		return new Quaternion(invFactor * fro.x + newFactor * to.x, invFactor * fro.y + newFactor * to.y, invFactor * fro.z + newFactor * to.z, invFactor * fro.w + newFactor * to.w);
	}


	static public void Exp(ref Quaternion a){
		float angle = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);

		float sinAngle = Mathf.Sin(angle);
		a.w = Mathf.Cos(angle);

		if (Mathf.Abs(sinAngle) >= 1.0e-15){
			float coeff = sinAngle / angle;
			a.x *= coeff;
			a.y *= coeff;
			a.z *= coeff;
		}
	}

	static public Quaternion Add(Quaternion a, Quaternion b){
		var r = new Quaternion();
		r.w = a.w + b.w;
		r.x = a.x + b.x;
		r.y = a.y + b.y;
		r.z = a.z + b.z;
		return r;
	}

	static public void Scale(ref Quaternion a, float s){
		a.w *= s;
		a.x *= s;
		a.y *= s;
		a.z *= s;
	}

	static public void Log(ref Quaternion a){
		float a0 = a.w;
		a.w = 0.0f;
		if (Mathf.Abs(a0) < 1.0f){
			float angle = Mathf.Acos(a0);
			float sinAngle = Mathf.Sin(angle);
			if (Mathf.Abs(sinAngle) >= 1.0e-15){
				float coeff = angle / sinAngle;
				a.x *= coeff;
				a.y *= coeff;
				a.z *= coeff;
			}
		}
	}
}
