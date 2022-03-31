using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGizmoColor : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    Camera cam;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(cam.pixelWidth/42.5f, cam.pixelHeight/42.5f, 0));
    }
}
