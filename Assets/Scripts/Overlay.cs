using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour {

    public Image indicator;
    public Transform hammer;
    public Canvas worldCanvas;

    List<Image> container;

    void Start()
    {
        container = new List<Image>();
    }

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
                container.Add(img);
            }

            foreach(GameObject target in rangedTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                container.Add(img);
            }
        }

        if(rangedTargets != null)
        {
            foreach(GameObject target in meleeTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                container.Add(img);
            }

            foreach(GameObject target in rangedTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                container.Add(img);
            }
        }
    }

    public void OverlayOff()
    {
        foreach(Image img in container)
        {
            Destroy(img);
        }

        container.Clear();
    }
}
