using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceGun : MonoBehaviour
{
    PlayerController target;
    [SerializeField] float rotationOffset;
    [SerializeField] GameObject bulletPrefab;

    private Camera mainCamera;
    private RectTransform rectTransform;
    private bool isFiring = false;
    [SerializeField] float bulletTimer;

    [SerializeField] Transform bulletPoint;

  public  bool canShoot = true;
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
        if (IsInGameView() && !isFiring)
        {
            StartCoroutine(SpawnBullet());
        }
    }


    void LookAtTarget()
    {
        var dir = target.transform.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private IEnumerator SpawnBullet()
    {
        isFiring= true;
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, null, false);
            bullet.transform.position = bulletPoint.transform.position;
        }
      
        yield return new WaitForSeconds(bulletTimer);
        isFiring = false;
    }
}
