using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control2_Test : MonoBehaviour
{
    float speed = 5f;
    float minY = 8f;  // Y축 최소값
    float maxY = 100f;  // Y축 최대값

    // Update is called once per frame
    void Update()
    {
        // 버튼 One이 눌리면 아래 방향으로 이동
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            transform.position = transform.position + new Vector3(0, -speed * Time.deltaTime, 0);
        }

        // Y축 이동 제한 (최소 Y값 8, 최대 Y값 40)
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