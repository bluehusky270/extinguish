using UnityEngine;

public class BambiBucket_Water : MonoBehaviour
{
    public ParticleSystem waterSpray;  // �� ���� ��ƼŬ �ý���
    public AudioSource waterSound;     // �� ���� ����
    public AudioClip waterSplashSound; // ���� �������� �Ҹ�
    public AudioClip waterFlowSound;   // ���� �帣�� �Ҹ� (�� ������� �Ҹ�)
    public float sprayForce = 10f;     // �� ������ ����
    public float sprayDuration = 5f;   // ���� ��� ���� �ð�
    private bool isSpraying = false;
    private float sprayTimeRemaining;

    public Transform helicopterTransform; // �︮������ Transform
    public Transform bucketTransform;     // ����Ŷ�� ��ġ
    public float bucketOffset = 2f;       // ����Ŷ�� �︮���� ������ �Ÿ�

    public ParticleSystem splashParticles; // ���� ƨ�� ���� ��ƼŬ �ý��� (�߰�)

    // �� ������ �����ϴ� �÷��׿� Ÿ�̸Ӹ� ����
    void Update()
    {
        // �� ���� ���̶�� ���� �ð� üũ
        if (isSpraying)
        {
            sprayTimeRemaining -= Time.deltaTime;
            if (sprayTimeRemaining <= 0)
            {
                StopSpraying();  // ������ �ð� �� �ڵ����� ���߱�
            }
        }

        // �︮���͸� �������� ����Ŷ ��ġ ������Ʈ
        Vector3 bucketPosition = helicopterTransform.position + helicopterTransform.forward * bucketOffset;
        bucketTransform.position = bucketPosition; // ����Ŷ�� ��ġ�� �︮���� �������� ����

        // ����Ŷ�� ȸ���� �︮���Ϳ� �Բ� ȸ���ϵ��� ����
        bucketTransform.rotation = helicopterTransform.rotation;
    }

    // �� ���� ����
    void StartSpraying()
    {
        if (!isSpraying)  // �̹� �� ���� ������ ������
        {
            isSpraying = true;
            sprayTimeRemaining = sprayDuration;  // ���� �ð��� ������ ���ߵ��� ����
            waterSpray.Play(); // �� ���� ����
            waterSound.clip = waterFlowSound;   // �� �帣�� �Ҹ� ����
            waterSound.loop = true;             // �ݺ��ؼ� �Ҹ� ���
            waterSound.Play();                  // �� ���� �Ҹ� ���

            // �� ���� ���� ����
            var main = waterSpray.main;
            main.startSpeed = sprayForce; // ���� ���� �ӵ� ����
            waterSpray.transform.rotation = bucketTransform.rotation; // ����Ŷ�� ȸ������ �� ���� ���⿡ �ݿ�
        }
    }

    // �� ���� ����
    void StopSpraying()
    {
        if (isSpraying)
        {
            isSpraying = false;
            waterSpray.Stop(); // �� ���� ����
            waterSound.Stop(); // �� ���� �Ҹ� ����
            // �� ������ ������ ��ƼŬ �ý��� ��Ȱ��ȭ
            waterSpray.gameObject.SetActive(false);
        }
    }

    // �� ƨ�� ȿ���� ���� Collision ����
    void OnCollisionEnter(Collision collision)
    {
        // ���� �ٸ� ��ü�� �浹�� �� ƨ��� �Ҹ� �Ǵ� ��ƼŬ �߰�
        if (collision.relativeVelocity.magnitude > 1f && splashParticles != null)
        {
            // ���� �������� ��ġ���� �Ҹ� ���
            AudioSource.PlayClipAtPoint(waterSplashSound, collision.contacts[0].point);

            // �� ƨ��� ��ƼŬ ����
            splashParticles.transform.position = collision.contacts[0].point; // �浹 ������ ��ƼŬ ����
            splashParticles.Play(); // ��ƼŬ ���
        }
    }

    // �︮���Ϳ� ȭ��ǥ�� �浹 ����
    void OnTriggerEnter(Collider other)
    {
        // �︮���Ϳ� ȭ��ǥ�� �浹�� �� �� ������ �����ϵ��� ����
        if (other.CompareTag("Arrow") && !isSpraying)  // ȭ��ǥ�� �±װ� "Arrow"�� ���
        {
            StartSpraying();  // �� ���� ����
        }
    }

    // �� ���� ���� �� ���� ����ȭ�� ���� ��ƼŬ �ý����� ��Ȱ��ȭ
    void OnDisable()
    {
        if (waterSpray != null)
        {
            waterSpray.gameObject.SetActive(false); // ��Ȱ��ȭ�Ͽ� ���� ����ȭ
        }
    }

    // �� ���� ���� ���� �� ���� ���⸦ �� �����ϰ� ������ �� �ִ� �Լ� �߰�
    public void AdjustSprayForce(float newForce)
    {
        sprayForce = Mathf.Clamp(newForce, 0f, 50f); // ����� 0���� 50���� ���� ����
        if (isSpraying)
        {
            var main = waterSpray.main;
            main.startSpeed = sprayForce;
        }
    }
}
