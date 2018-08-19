using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {
    public Camera cam;
    public float paralax;

    private Vector3 lastPosition;
	// Use this for initialization
	void Start () {
        lastPosition = cam.transform.position;
	}

    private void FixedUpdate()
    {
        Vector3 delta = cam.transform.position - lastPosition;
        transform.position += new Vector3(delta.x * paralax,
                                          delta.y * paralax);
        lastPosition = cam.transform.position;
    }
}
