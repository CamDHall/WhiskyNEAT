using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour {

    public Image indicator;
    public Transform hammer;
    public Canvas worldCanvas;

    public void OverlayOn(List<GameObject> meleeTargets, List<GameObject> rangedTargets)
    {
        if(meleeTargets != null)
        {
            Debug.Log("Melee");
            foreach(GameObject target in meleeTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
            }

            foreach(GameObject target in rangedTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
            }
        }

        if(rangedTargets != null)
        {
            foreach(GameObject target in meleeTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
            }

            foreach(GameObject target in rangedTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
            }
        }
    }

    public void OverlayOff()
    {

    }
}
