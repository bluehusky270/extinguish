using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public float rotationSpeed = 100f;      // ȸ�� �ӵ�
    //public float maxVerticalAngle = 60f;    // ���� ȸ�� ���� (�ִ� 60��)
    //private float currentVerticalRotation = 0f;  // ���� ȸ�� ����
    private float currentHorizontalRotation = 0f; // �¿� ȸ�� ����

    private float smoothing = 0.1f; // ȸ�� �ε巴�� ����� ���� ���� ��
    //private float targetVerticalRotation = 0f;
    private float targetHorizontalRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        // ���̽�ƽ�� X�� �Է� (�¿� ȸ��)
        float horizontalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        //// ���̽�ƽ�� Y�� �Է� (���� ȸ��)
        //float verticalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;

        // �¿� ȸ�� (Yaw)
        targetHorizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;

        //// ���� ȸ�� (Pitch)
        //targetVerticalRotation -= verticalInput * rotationSpeed * Time.deltaTime;
        //// ���� ȸ�� ���� (-maxVerticalAngle ~ maxVerticalAngle)
        //targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // �ε巯�� ȸ�� ���� (Lerp�� ���� ����)
        currentHorizontalRotation = Mathf.Lerp(currentHorizontalRotation, targetHorizontalRotation, smoothing);
        //currentVerticalRotation = Mathf.Lerp(currentVerticalRotation, targetVerticalRotation, smoothing);

        // ī�޶� ȸ�� ���� (���� ȸ���� X��, �¿� ȸ���� Y��)
        transform.localRotation = Quaternion.Euler(/*currentVerticalRotation*/0, currentHorizontalRotation, 0);
    }
}
