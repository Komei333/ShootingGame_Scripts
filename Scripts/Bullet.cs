using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Score score;

    void Start()
    {
        Score script = FindObjectOfType<Score>();
        score = script;
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Prize")
        {
            score.HitReset();
        }

        Destroy(gameObject);
    }
}
