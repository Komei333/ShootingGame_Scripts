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

        //�i�i�����F�ɂȂ�m���̏���
        var rand = Random.Range(0, rainbowRand);

        //�i�i����F�ɂ���
        if (rand == 0)
        {
            //���F�̌i�i�������\���ǂ����𒲂ׂ�
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
            //�i�i�𗎂Ƃ�
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

            //�V�����i�i�̐���
            Invoke("GeneratePrize", generateTime);
            Destroy(gameObject, generateTime + 0.01f);

            //���F�̌i�i�̃C�x���g
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

