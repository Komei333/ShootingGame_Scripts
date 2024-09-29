using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Score score;

    void Start()
    {
        Score scoreScript = FindObjectOfType<Score>();
        score = scoreScript;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gun") return;

        if (collision.gameObject.tag != "Prize")
        {
            score.HitBonusReset();
        }

        Destroy(gameObject);
    }
}
