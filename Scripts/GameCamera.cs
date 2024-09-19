using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public LayerMask rayWallLayer;

    private Vector3 initialAngle;
    private Vector3 currentAngle;
    private Vector3 rayWallHitPosition;

    private float xAngleLimit = 15f;
    private float yAngleLimit = 37.5f;

    private bool canMoveCamera = true;

    void Start()
    {
        initialAngle = this.gameObject.transform.localEulerAngles;
        currentAngle = this.gameObject.transform.localEulerAngles;
    }

    void Update()
    {
        if (canMoveCamera == false) return;


        // y²‰ñ“](ƒJƒƒ‰‚Ì¶‰E‰ñ“])
        currentAngle.y += Input.GetAxis("Mouse X");
        if (currentAngle.y <= initialAngle.y - yAngleLimit)
        {
            currentAngle.y = initialAngle.y - yAngleLimit;
        }
        else if (currentAngle.y >= initialAngle.y + yAngleLimit)
        {
            currentAngle.y = initialAngle.y + yAngleLimit;
        }

        // x²‰ñ“](ƒJƒƒ‰‚Ìã‰º‰ñ“])
        currentAngle.x -= Input.GetAxis("Mouse Y");
        if (currentAngle.x <= initialAngle.x - xAngleLimit)
        {
            currentAngle.x = initialAngle.x - xAngleLimit;
        }
        else if (currentAngle.x >= initialAngle.x + (xAngleLimit/2)) // ƒJƒƒ‰‚ª‰º‚ÉŒü‚«‚·‚¬‚È‚¢‚æ‚¤‚É2‚ÅŠ„‚Á‚Ä•â³‚ğ‚©‚¯‚é
        {
            currentAngle.x = initialAngle.x + (xAngleLimit/2);
        }

        transform.localEulerAngles = currentAngle;


        Ray ray = new Ray(transform.position, transform.forward);  //Ray‚ğ¶¬

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10.0f, rayWallLayer))
        {
            //ƒŒƒC‚ª•Ç‚ÆŒğ·‚µ‚½ê‡‚Ìˆ—
            rayWallHitPosition = hitInfo.point;
        }
    }

    public Vector3 ReturnRayWallHitPosition()
    {
        return rayWallHitPosition;
    }

    public void CanMoveCamera()
    {
        canMoveCamera = true;
    }

    public void StopMoveCamera()
    {
        canMoveCamera = false;
    }
}