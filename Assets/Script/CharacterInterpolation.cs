using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class CharacterInterpolation : MonoBehaviour
{
	public enum Mode{
		SQUAD,
		SLERP,
		LINEULER,
	}

	const int controlPtNum = 4;
	public TextAsset file;
	XmlDocument xml = new XmlDocument();
	XmlNode root;
	List<GameObject> jntObjList = new List<GameObject>();
	public float[] length = new float[3] { 1, 1, 1};

	public Mode mode;
	public bool loop = true;
	float alongLine;
	float timer = 0.0f;

	void Awake()
    {
		xml.LoadXml(file.text);
		root = xml.FirstChild;
		getChildGameObj(ref jntObjList);   // get all joints under hierearchy
	}

	private void FixedUpdate()
	{
		timer += Time.deltaTime;

		int jointNum = root.ChildNodes.Count;
		for (int i = 0; i < jointNum; i++){
			XmlNode joint = root.ChildNodes.Item(i);
			foreach (GameObject obj in jntObjList) {
				// found matching joint based on Attributes["id"].Value
				if (obj.name == joint.Attributes["id"].Value){    
					jointValue(obj, joint);
					break;
				}
			}
		}
	}

	/* Perform rotation on gameobject based on xmlNode rotation value */
	private void jointValue(GameObject obj, XmlNode joint)
	{
		float seconds = timer % 60;
		Quaternion[] quanternionList = new Quaternion[controlPtNum];
		Vector3[] eulerList = new Vector3[controlPtNum];

		// get interpolation value sequence
		var nodeList = joint.SelectNodes("key");
		for(int i = 0; i < controlPtNum; i++){
			float x = float.Parse(nodeList[i].Attributes["valuex"].Value);
			float y = float.Parse(nodeList[i].Attributes["valuey"].Value);
			float z = float.Parse(nodeList[i].Attributes["valuez"].Value);
			//  get separate values for euler/quaternion rotation modality
			quanternionList[i] = Utility.MayaToUnityQuater(new Vector3(x, y, z)); 
			eulerList[i] = Utility.MayaToUnityEuler(new Vector3(x, y, z));
		}

		// mode selection
		if (mode is Mode.SQUAD)
			obj.transform.localRotation = Squad.Spline(quanternionList, alongLine);
		else if (mode is Mode.SLERP)
			obj.transform.localRotation = MathFunc.Slerp(quanternionList, alongLine);
		else if (mode is Mode.LINEULER)
			obj.transform.localEulerAngles = MathFunc.LinEuler(eulerList, alongLine);

		// percentage parameters
		alongLine = percentAlongLine(seconds);

		// loop condition for reseting timer
		if (alongLine >= 0.99 && loop){  
			alongLine = 0;
			timer = 0;
		}
	}

	/* Get children gameobject of the current game object */
	void getChildGameObj(ref List<GameObject> list){
		Transform[] jntTransList = GetComponentsInChildren<Transform>();
		foreach (Transform jntTrans in jntTransList)
			list.Add(jntTrans.gameObject);
	}

	/* Calculate the percentage along the line based on time */
	float percentAlongLine(float time)
	{
		float percent = 0;
		float segment = 1.0f / (controlPtNum - 1);
		Vector3 segments = new Vector3();

		// initialize segments with time length
		segments[0] = length[0];
		for (int i = 1; i < controlPtNum - 1; ++i)
			segments[i] = length[i] + segments[i - 1];

		// calcuation first segment and the rest
		if (time < segments[0]){
			percent = (0 + (time / length[0])) * segment;
			return percent;
		}
		for (int i = 1; i < controlPtNum - 1; ++i){
			if (time >= segments[i - 1] && time < segments[i]){
				percent = (i + ((time - segments[i - 1]) / length[i])) * segment;
				return percent;
			}
		}
		return percent;
	}
}
