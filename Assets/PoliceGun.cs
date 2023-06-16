using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceGun : MonoBehaviour
{
    PlayerController target;
    [SerializeField] float rotationOffset;

    private Camera mainCamera;
    private RectTransform rectTransform;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>();
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private bool IsInGameView()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(rectTransform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }
    private void Update()
    {
      

        LookAtTarget();
        if (IsInGameView())
        {
            print("balls");
        }
    }


    void LookAtTarget()
    {
        var dir = target.transform.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }



}
