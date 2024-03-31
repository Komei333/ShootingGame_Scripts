using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public LayerMask rayWallLayer;

    private Vector3 primaryAngle;
    private Vector3 currentAngle;
    private Vector3 wallHitPos;

    private bool canMove = true;

    void Start()
    {
        primaryAngle = this.gameObject.transform.localEulerAngles;
        currentAngle = this.gameObject.transform.localEulerAngles;
    }

    void Update()
    {
        if (canMove)
        {
            currentAngle.y += Input.GetAxis("Mouse X");

            if (currentAngle.y <= primaryAngle.y - 37.5f)
            {
                currentAngle.y = primaryAngle.y - 37.5f;
            }
            if (currentAngle.y >= primaryAngle.y + 37.5f)
            {
                currentAngle.y = primaryAngle.y + 37.5f;
            }

            currentAngle.x -= Input.GetAxis("Mouse Y");

            if (currentAngle.x <= primaryAngle.x - 15f)
            {
                currentAngle.x = primaryAngle.x - 15f;
            }
            if (currentAngle.x >= primaryAngle.x + 7.5f)
            {
                currentAngle.x = primaryAngle.x + 7.5f;
            }

            transform.localEulerAngles = currentAngle;


            Ray ray = new Ray(transform.position, transform.forward);  //RayÇê∂ê¨

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 10.0f, rayWallLayer))
            {
                //ÉåÉCÇ™ï«Ç∆åç∑ÇµÇΩèÍçáÇÃèàóù
                wallHitPos = hitInfo.point;
            }
        }    
    }

    public Vector3 ReturnHitPos()
    {
        return wallHitPos;
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void StopMove()
    {
        canMove = false;
    }
}