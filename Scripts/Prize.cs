using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Prize : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] Rainbow rainbow;

    [SerializeField] Material RainbowMaterial;

    private Material DefaultMaterial;
    private Renderer prizeRenderer;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool destroy = false;
    private float power = 350.0f;

    public bool rainbowEvent = false;
    public int rainbowRand = 6;
    public float generateTime = 2.0f;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        prizeRenderer = GetComponent<Renderer>();
        DefaultMaterial = prizeRenderer.material;

        //景品が虹色になる確率の処理
        var rand = Random.Range(0, rainbowRand);

        //景品を虹色にする
        if (rand == 0)
        {
            //虹色の景品が生成可能かどうかを調べる
            rainbowEvent = rainbow.CheckRainbowFlag();

            if(rainbowEvent)
            {
                prizeRenderer.material = RainbowMaterial;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //景品を落とす
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Vector3 prizeForce = -transform.forward * power;
                rb.AddForce(prizeForce);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GetArea") && destroy == false)
        {
            destroy = true;
            score.PrizeScore();

            //新しい景品の生成
            Invoke("GeneratePrize", generateTime);
            Destroy(gameObject, generateTime + 0.01f);

            //虹色の景品のイベント
            if (rainbowEvent)
            {
                rainbow.RandomEvent();
                score.RainbowSE();
            }
        }
    }

    void GeneratePrize()
    {
        prizeRenderer.material = DefaultMaterial;
        rainbowEvent = false;
        Instantiate(this, initialPosition, initialRotation);
    }

    public void GenerateEarly()
    {
        generateTime -= 0.75f;
    }

    public void ChanceRainbow()
    {
        rainbowRand -= 2;
    }
}

