using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class BambiBucket_Water2 : MonoBehaviour
{
    public GameObject Player;            // 헬리콥터 오브젝트
    public ParticleSystem waterSpray;    // 물 방출을 위한 파티클 시스템
    public AudioClip fallSound;          // 밤비 버킷 물 쏟아지는 소리
    private AudioSource waterAudioSource; // 오디오 소스
    private bool isWaterSpraying = false; // 물 방출 중인지 확인하는 변수

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Helicopter");

        if (Player == null) Debug.LogError("Player (Helicopter) not found!");

        // 오디오 소스 초기화
        waterAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 퍼스트 버튼이 눌렸을 때 물 방출 시작
        if (IsButtonOnePressed() && !isWaterSpraying)
        {
            StartWaterSpraying();  // 물 방출 시작
        }
        // 퍼스트 버튼이 떼어졌을 때 물 방출 멈춤
        else if (!IsButtonOnePressed() && isWaterSpraying)
        {
            StopWaterSpraying();   // 물 방출 멈춤
        }
    }

    // 퍼스트 버튼이 눌렸는지 확인
    bool IsButtonOnePressed()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.isValid)
        {
            if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
            {
                return isPressed;  // 버튼이 눌렸다면 true 반환
            }
        }
        return false; // 버튼이 눌리지 않았으면 false 반환
    }

    // 물 방출 시작
    public void StartWaterSpraying()
    {
        isWaterSpraying = true;

        // 헬리콥터의 위치를 기준으로 물 방출 위치를 설정
        Vector3 helicopterPosition = Player.transform.position;

        // 물 방출 위치를 헬리콥터 아래로 설정 (예: Y축 -2만큼 아래)
        waterSpray.transform.position = helicopterPosition + new Vector3(0f, -2f, 0f);

        // 물 방출 파티클 시스템 시작
        waterSpray.Play();

        // 물 방출 소리 재생
        if (waterAudioSource != null && fallSound != null && !waterAudioSource.isPlaying)
        {
            waterAudioSource.PlayOneShot(fallSound);
        }
    }

    // 물 방출 멈추기
    public void StopWaterSpraying()
    {
        isWaterSpraying = false;

        // 물 방출 파티클 시스템 중지
        waterSpray.Stop();
    }
}
