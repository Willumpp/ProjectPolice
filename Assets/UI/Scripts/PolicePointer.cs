using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePointer : MonoBehaviour
{
    public GameObject targetGameobject;
    public Vector3 targetPosition;
    public Camera mainCamera;
    private RectTransform pointerRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = mainCamera.WorldToScreenPoint(targetGameobject.transform.position);
        pointerRectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        pointerRectTransform.LookAt(dir);
        */
        var targetPosLocal = Camera.main.transform.InverseTransformPoint(targetPosition);
        var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
        pointerRectTransform.transform.eulerAngles = new Vector3(0, 0, targetAngle);


    }
}
