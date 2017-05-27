using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Vector3 Move;
    private Transform cameraRotate;
    public float cameraSpeed;

	void Start () {
	
	}
	
	void Update () {
        Move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Mouse ScrollWheel")*-1, Input.GetAxis("Vertical"));
        transform.eulerAngles = new Vector3(20 + Input.GetAxis("Mouse ScrollWheel"), 0, 0);

        transform.position += Move * cameraSpeed * Time.deltaTime;
    }
}
