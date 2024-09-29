using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameCamera gameCamera;

    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] GameObject gunMuzzle; //èeå˚

    private Vector3 rayWallHitPosition;
    private float shotPower = 250.0f;
    private bool canShot = true;
    public bool isBigBulletEvent = false;

    void Update()
    {
        rayWallHitPosition = gameCamera.ReturnRayWallHitPosition();

        Vector3 shotDirection = rayWallHitPosition - gunMuzzle.transform.position;
        Vector3 gunAngle = new Vector3(0f, shotDirection.z, shotDirection.y * -1) * 30f;
        transform.localEulerAngles = gunAngle;

        if (canShot)
        {
            //ç∂ÉNÉäÉbÉNÇ≈íeÇî≠éÀÇ∑ÇÈ
            if (Input.GetMouseButtonDown(0))
            {
                //íeÇê∂ê¨
                var bulletRotation = Quaternion.Euler(0f, 0f, 90f);

                GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.transform.position, bulletRotation) as GameObject;
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(shotDirection * shotPower);
                Vector3 bulletAngle = new Vector3(0f, gunAngle.y, gunAngle.z + 90);
                bullet.transform.localEulerAngles = bulletAngle;

                if (isBigBulletEvent)
                {
                    var newScale = bullet.transform.localScale;
                    newScale = newScale * 4;
                    bullet.transform.localScale = newScale;
                }
               
                musicManager.PlaySE2();
            }
        }
    }

    public void CanShot()
    {
        canShot = true;
    }

    public void StopShot()
    {
        canShot = false;
    }

    void EndBigBulletEvent()
    {
        isBigBulletEvent = false;
    }

    public void BigBulletEvent()
    {
        isBigBulletEvent = true;
        Invoke("EndBigBulletEvent", 10f);
    }
}