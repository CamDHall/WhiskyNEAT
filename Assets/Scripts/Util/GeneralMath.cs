using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMath {

	public static float DistanceForm(Vector3 firstObj, Vector3 secondObj)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((secondObj.x - firstObj.x), 2) + Mathf.Pow((secondObj.z - firstObj.z), 2));
        return distance;
    }
}
