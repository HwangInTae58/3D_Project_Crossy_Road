using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pCamera : MonoBehaviour
{
    public static pCamera instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void LateUpdate()
    {
        
    }
    public bool CameraView(Camera _camera, Transform _transform)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = _transform.position;
        foreach(var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}
