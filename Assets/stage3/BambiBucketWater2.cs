using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class BambiBucket_Water2 : MonoBehaviour
{
    public GameObject Player;            // �︮���� ������Ʈ
    public ParticleSystem waterSpray;    // �� ������ ���� ��ƼŬ �ý���
    public AudioClip fallSound;          // ��� ��Ŷ �� ������� �Ҹ�
    private AudioSource waterAudioSource; // ����� �ҽ�
    private bool isWaterSpraying = false; // �� ���� ������ Ȯ���ϴ� ����

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Helicopter");

        if (Player == null) Debug.LogError("Player (Helicopter) not found!");

        // ����� �ҽ� �ʱ�ȭ
        waterAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �۽�Ʈ ��ư�� ������ �� �� ���� ����
        if (IsButtonOnePressed() && !isWaterSpraying)
        {
            StartWaterSpraying();  // �� ���� ����
        }
        // �۽�Ʈ ��ư�� �������� �� �� ���� ����
        else if (!IsButtonOnePressed() && isWaterSpraying)
        {
            StopWaterSpraying();   // �� ���� ����
        }
    }

    // �۽�Ʈ ��ư�� ���ȴ��� Ȯ��
    bool IsButtonOnePressed()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.isValid)
        {
            if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
            {
                return isPressed;  // ��ư�� ���ȴٸ� true ��ȯ
            }
        }
        return false; // ��ư�� ������ �ʾ����� false ��ȯ
    }

    // �� ���� ����
    public void StartWaterSpraying()
    {
        isWaterSpraying = true;

        // �︮������ ��ġ�� �������� �� ���� ��ġ�� ����
        Vector3 helicopterPosition = Player.transform.position;

        // �� ���� ��ġ�� �︮���� �Ʒ��� ���� (��: Y�� -2��ŭ �Ʒ�)
        waterSpray.transform.position = helicopterPosition + new Vector3(0f, -2f, 0f);

        // �� ���� ��ƼŬ �ý��� ����
        waterSpray.Play();

        // �� ���� �Ҹ� ���
        if (waterAudioSource != null && fallSound != null && !waterAudioSource.isPlaying)
        {
            waterAudioSource.PlayOneShot(fallSound);
        }
    }

    // �� ���� ���߱�
    public void StopWaterSpraying()
    {
        isWaterSpraying = false;

        // �� ���� ��ƼŬ �ý��� ����
        waterSpray.Stop();
    }
}
