using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public float altitudeThreshold = 8f; // 버튼이 활성화될 고도 (단위: 미터)
    public GameObject buttonUI; // 버튼 UI 오브젝트

    void Start()
    {
        // 버튼 UI는 처음에 비활성화
        if (buttonUI != null)
        {
            buttonUI.SetActive(false);
        }
    }

    void Update()
    {
        // 헬리콥터의 현재 고도 계산 (Y 위치 사용)
        float currentAltitude = transform.position.y;

        // 고도가 일정 값 이하로 내려가면 버튼 활성화
        if (currentAltitude <= altitudeThreshold)
        {
            if (buttonUI != null)
            {
                buttonUI.SetActive(true); // 버튼 활성화
            }
        }
        else
        {
            if (buttonUI != null)
            {
                buttonUI.SetActive(false); // 버튼 비활성화
            }
        }
    }
}