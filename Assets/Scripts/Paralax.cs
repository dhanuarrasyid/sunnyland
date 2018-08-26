using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {
    Camera cam;
    [Range(0, 1f)] [SerializeField] public float paralax;

    private Vector3 lastPosition;

    private Renderer m_renderer;
	// Use this for initialization
	void Start () {
        m_renderer = GetComponent<Renderer>();
        cam = Camera.main;
	}

    private void FixedUpdate()
    {
        if(m_renderer.isVisible)
        {
            Vector3 camera_position = new Vector3(cam.transform.position.x,
                                                  cam.transform.position.y);
            if(lastPosition != null)
            {
                Vector3 delta = camera_position - lastPosition;
                if(Mathf.Abs(delta.x) < 1 && Mathf.Abs(delta.y) < 1)
                {
                    transform.position += new Vector3(delta.x * paralax,
                                                      delta.y * paralax);   
                }
            }
            lastPosition = camera_position;  
        }

    }

    //private bool IsVisible()
    //{
    //    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
    //    return GeometryUtility.TestPlanesAABB(planes, m_renderer.bounds);
    //}
}
