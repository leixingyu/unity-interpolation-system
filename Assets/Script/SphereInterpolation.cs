using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInterpolation : MonoBehaviour
{
	public enum Mode{
		SQUAD,
		SLERP,
		EULER,
	}

	private const int controlPtNum = 4;
	public Vector3[] controlPoints;
	Quaternion[] quanterionSplinePoint;
	Vector3[] eulerSplinePoint;
	public float[] length;

	public Mode mode;
	public bool loop = true;
	float alongLine;
	float timer = 0.0f;

	private void Start()
	{
		// initialize control point length, control point rotation
		length = new float[controlPtNum - 1]{1.0f, 2.0f, 1.0f};
		controlPoints = new Vector3[controlPtNum] {
			new Vector3(-40.0f, 90.0f, 0.0f), new Vector3(50.0f, -50.0f, 0.0f),
			new Vector3(-17.0f, -52.0f, 0.0f), new Vector3(56.0f, 62.0f, 0.0f)
		};
		eulerSplinePoint = new Vector3[controlPtNum];
		quanterionSplinePoint = new Quaternion[controlPtNum];

		// convert maya rotation value to unity
		for (int i = 0; i < controlPtNum; ++i) { 
			quanterionSplinePoint[i] = Utility.MayaToUnityQuater(controlPoints[i]);
			eulerSplinePoint[i] = Utility.MayaToUnityEuler(controlPoints[i]);
		}
	}

	void Update()
    {
		timer += Time.deltaTime;
		float seconds = timer % 60;

		// interpolation mode
		if (mode is Mode.SQUAD)
			transform.rotation = Squad.Spline(quanterionSplinePoint, alongLine);
		else if (mode is Mode.SLERP)
			transform.rotation = MathFunc.Slerp(quanterionSplinePoint, alongLine);
		else if (mode is Mode.EULER)
			transform.eulerAngles = MathFunc.LinEuler(eulerSplinePoint, alongLine);	

		// percentage parameter
		alongLine = percentAlongLine(seconds);

		// loop condition for reset timer	
		if (alongLine >= 0.99 && loop){
			alongLine = 0;
			timer = 0;
		}
	}

	/* Calculate the percentage along the line based on time */
	float percentAlongLine(float time)
	{
		float percent = 0;
		float segment = 1.0f / (controlPtNum-1);
		Vector3 segments = new Vector3();

		// initialize segments with time length
		segments[0] = length[0];
		for (int i = 1; i < controlPtNum-1; ++i)
			segments[i] = length[i] + segments[i - 1];

		// calcuation first segment and the rest
		if(time < segments[0]) { 
			percent = (0 + (time / length[0])) * segment;
			return percent;
		}
		for (int i = 1; i < controlPtNum-1; ++i){
			if (time >= segments[i-1] && time < segments[i]){
				percent = (i + ((time - segments[i - 1]) / length[i])) * segment;
				return percent;
			}
		}
		return percent;
	}
}
