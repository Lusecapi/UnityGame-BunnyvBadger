using UnityEngine;
using System.Collections;

public class CameraFix : MonoBehaviour {

    public int divider = 70;
    public float multiplier = 0.4f;
    Camera myCamera;

    public float myDesiredHorizontalFov = 50;


    // Use this for initialization
    void Start () {

        myCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        myCamera.fieldOfView = myDesiredHorizontalFov / ((float)myCamera.pixelWidth / myCamera.pixelHeight);
        //myCamera.orthographicSize = (Screen.width / divider) * multiplier;
	}
}
