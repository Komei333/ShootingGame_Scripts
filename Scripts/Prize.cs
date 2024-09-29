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

    private bool isDestroyed = false;
    private float prizePushPower = 350.0f;

    public bool isHappenedRainbowEvent = false;
    public int rainbowRandomValue = 6;
    public float generatePrizeTime = 2.0f;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        prizeRenderer = GetComponent<Renderer>();
        DefaultMaterial = prizeRenderer.material;

        var rand = Random.Range(0, rainbowRandomValue);

        //�����̒l��0�̏ꍇ�͌i�i����F�ɂ���
        if (rand == 0)
        {
            //���F�̌i�i�������\���ǂ����𒲂ׂ�
            isHappenedRainbowEvent = rainbow.CheckHappenedRainbowEvent();

            if(isHappenedRainbowEvent)
            {
                prizeRenderer.material = RainbowMaterial;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            //�i�i�𗎂Ƃ�
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Vector3 prizePushForce = -transform.forward * prizePushPower;
                rb.AddForce(prizePushForce);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GetArea") && isDestroyed == false)
        {
            isDestroyed = true;
            score.PrizeScore();

            //�V�����i�i�̐���
            Invoke("GeneratePrize", generatePrizeTime);
            Destroy(gameObject, generatePrizeTime + 0.01f);

            //���F�̌i�i�̃C�x���g�𔭐�������
            if (isHappenedRainbowEvent)
            {
                rainbow.RandomEvent();
                score.RainbowPrizeSE();
            }
        }
    }

    void GeneratePrize()
    {
        prizeRenderer.material = DefaultMaterial;
        isHappenedRainbowEvent = false;
        Instantiate(this, initialPosition, initialRotation);
    }

    public void GeneratePrizeEarly()
    {
        generatePrizeTime -= 0.5f;
    }

    public void ChanceRainbow()
    {
        rainbowRandomValue--;
    }
}

