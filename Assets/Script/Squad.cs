using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.gamasutra.com/blogs/VegardMyklebust/20150911/253461/Spherical_Spline_Quaternions_For_Dummies.php
public class Squad {

	// Returns a smoothed quaternion along the set of quaternions making up the spline, each quaternion is along an equidistant value in t
	public static Quaternion Spline(Quaternion[] quaternions , float t )
    {
        var section = (int)((quaternions.Length - 1) * t);
        var alongLine = (quaternions.Length - 1) * t - section;
        if (section == 0)
            return SplineSegment(quaternions[section], quaternions[section], quaternions[section + 1], quaternions[section + 2], alongLine);

        else if( section == quaternions.Length - 2 && section > 0)
            return SplineSegment(quaternions[section - 1], quaternions[section], quaternions[section + 1], quaternions[section + 1], alongLine);

        else if (  section >= 1 && section < quaternions.Length -2)
            return SplineSegment(quaternions[section - 1], quaternions[section], quaternions[section + 1], quaternions[section + 2], alongLine);

        Debug.LogError("???");
        return Quaternion.identity;
    }

    // Returns a quaternion between q1 and q2 as part of a smooth SQUAD segment
    public static Quaternion SplineSegment(Quaternion q0, Quaternion q1, Quaternion q2, Quaternion q3, float t)
    {
        Quaternion qa = Intermediate(q0, q1, q2);
        Quaternion qb = Intermediate(q1, q2, q3);
        return SQUAD(q1, qa, qb, q2, t);
    }

    // Tries to compute sensible tangent values for the quaternion  -> equivlent to si
    static Quaternion Intermediate(Quaternion q0, Quaternion q1, Quaternion q2 )
    {
        Quaternion q1inv  = Quaternion.Inverse(q1);
        Quaternion c1 = q1inv * q2;
        Quaternion c2 = q1inv * q0;
		MathFunc.Log(ref c1);
		MathFunc.Log(ref c2);
		Quaternion c3 = MathFunc.Add(c2, c1);// c2 + c1;
		MathFunc.Scale(ref c3, -0.25f);// c3.Scale(-0.25f);
        MathFunc.Exp(ref c3);// c3.Exp();
        Quaternion r  = q1 * c3;
        r.Normalize();
        return r;
    }

	// Returns a smooth approximation between q1 and q2 using t1 and t2 as 'tangents'
	static Quaternion SQUAD(Quaternion q1, Quaternion t1, Quaternion t2, Quaternion q2, float t)
	{
		float slerpT = 2.0f * t * (1.0f - t);
		Quaternion slerp1 = MathFunc.SlerpNoInvert(q1, q2, t);
		Quaternion slerp2 = MathFunc.SlerpNoInvert(t1, t2, t);
		return MathFunc.SlerpNoInvert(slerp1, slerp2, slerpT);
	}

}
