using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameCamera gameCamera;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gunMuzzle; //�e��

    private Vector3 hitPos;
    private float power = 250.0f;
    private bool canShot = true;

    void Start()
    {

    }

    void Update()
    {
        hitPos = gameCamera.ReturnHitPos();

        Vector3 shotDirection = hitPos - gunMuzzle.transform.position;
        Vector3 gunAngle = new Vector3(0f, shotDirection.z, shotDirection.y * -1) * 30f;
        transform.localEulerAngles = gunAngle;

        if (canShot)
        {
            //���N���b�N
            if (Input.GetMouseButtonDown(0))
            {
                //�e�𐶐�
                var bulletRotation = Quaternion.Euler(0f, 0f, 90f);
                GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.transform.position, bulletRotation) as GameObject;
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(shotDirection * power);

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
}