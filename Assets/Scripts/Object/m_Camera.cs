using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_Camera : MonoBehaviour
{
    public Transform target;

    Vector3 offset;
    
    private void Start()
    {
        offset = transform.position - target.position;
    }
    private void Update()
    {
        CameraFollow();
    }
    private void CameraFollow()
    {
        Vector3 cameraPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraPos, 0.3f);
    }
}
