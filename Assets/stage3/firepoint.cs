using UnityEngine;
using UnityEngine.UI; // UI 관련
using TMPro; // TextMeshPro 사용 시 필요
using UnityEngine.XR;
using System.Collections.Generic; // VR 입력 관련

public class FirePointManager : MonoBehaviour
{
    public Text uiText; // Unity UI Text
    public GameObject waterButton; // 물뿌리기 버튼
    public Transform player; // 플레이어 위치
    public Transform[]  firePoint;
    public float activationDistance = 3f; // 버튼 활성화 거리
    public int firePointRemovedCount=0;
    private Transform currentfirepoint;
    void Start()
    {
        UpdateFirePointText();
        if (waterButton != null)
        {
            waterButton.SetActive(false);
        }
    }

    void Update()
    {
        currentfirepoint=GetClosestFirePoint();
        if (currentfirepoint != null)
        {
            float distanceToFirePoint = Vector3.Distance(player.position, currentfirepoint.position);
            if (distanceToFirePoint <= activationDistance)
            {
                if (waterButton != null && !waterButton.activeSelf)
                {
                    waterButton.SetActive(true);
                }

                // VR 컨트롤러의 Button.One 입력 확인
                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) // 테스트용 키 추가
                {
                    RemoveFirePoint(currentfirepoint);
                }
            }
            else
            {
                if (waterButton != null && waterButton.activeSelf)
                {
                    waterButton.SetActive(false);
                }
            }
        }
    }
    private Transform GetClosestFirePoint()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform firePoint in firePoint)
        {
            if (firePoint.gameObject.activeSelf) // 활성화된 Fire Point만 고려
            {
                float distance = Vector3.Distance(player.position, firePoint.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = firePoint;
                }
            }
        }

        return closest;
    }
    private void RemoveFirePoint(Transform firePoint)
    {
        firePoint.gameObject.SetActive(false);
        firePointRemovedCount++;
        UpdateFirePointText();

        // 버튼 비활성화
        if (waterButton != null)
        {
            waterButton.SetActive(false);
        }
    }
    public void UpdateFirePointText()
    {
        int totalfirepoints=firePoint.Length;
        string dynamicText = $"물 뿌린 곳 ({firePointRemovedCount}/{totalfirepoints})";
        if (uiText != null)
        {
            uiText.text = dynamicText;
        }
    }
}

