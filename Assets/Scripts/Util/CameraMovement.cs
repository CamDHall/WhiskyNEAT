using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform center;

    private Vector3 Move;
    private Transform cameraRotate;
    public float cameraSpeed;

	void Start () {
	
	}
	
	void FixedUpdate()
    {
        // transform.LookAt(center);

        if (Input.mousePosition.x > Screen.width * 0.95f)
        {
            transform.LookAt(center);
            transform.Translate(Vector3.right * (Time.deltaTime * cameraSpeed));
        }

        if (Input.mousePosition.x < Screen.width * 0.05f)
        {
            transform.LookAt(center);
            transform.Translate(Vector3.left * (Time.deltaTime * cameraSpeed));
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);
        }
    }
}
