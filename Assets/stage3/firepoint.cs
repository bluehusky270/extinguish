using UnityEngine;
using UnityEngine.UI; // UI ����
using TMPro; // TextMeshPro ��� �� �ʿ�
using UnityEngine.XR;
using System.Collections.Generic; // VR �Է� ����

public class FirePointManager : MonoBehaviour
{
    public Text uiText; // Unity UI Text
    public GameObject waterButton; // ���Ѹ��� ��ư
    public Transform player; // �÷��̾� ��ġ
    public Transform[]  firePoint;
    public float activationDistance = 3f; // ��ư Ȱ��ȭ �Ÿ�
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

                // VR ��Ʈ�ѷ��� Button.One �Է� Ȯ��
                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) // �׽�Ʈ�� Ű �߰�
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
            if (firePoint.gameObject.activeSelf) // Ȱ��ȭ�� Fire Point�� ���
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

        // ��ư ��Ȱ��ȭ
        if (waterButton != null)
        {
            waterButton.SetActive(false);
        }
    }
    public void UpdateFirePointText()
    {
        int totalfirepoints=firePoint.Length;
        string dynamicText = $"�� �Ѹ� �� ({firePointRemovedCount}/{totalfirepoints})";
        if (uiText != null)
        {
            uiText.text = dynamicText;
        }
    }
}

