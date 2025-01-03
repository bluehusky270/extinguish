using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control2_Test : MonoBehaviour
{
    float speed = 5f;
    float minY = 8f;  // Y�� �ּҰ�
    float maxY = 100f;  // Y�� �ִ밪

    // Update is called once per frame
    void Update()
    {
        // ��ư One�� ������ �Ʒ� �������� �̵�
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            transform.position = transform.position + new Vector3(0, -speed * Time.deltaTime, 0);
        }

        // Y�� �̵� ���� (�ּ� Y�� 8, �ִ� Y�� 40)
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }
    }