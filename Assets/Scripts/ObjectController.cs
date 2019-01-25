using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            var v3 = Input.mousePosition;
            v3 = Camera.main.ScreenToWorldPoint(v3);

            Ray ray = new Ray(v3, Camera.main.transform.forward);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.red, 5);
            }
        }
    }
}
