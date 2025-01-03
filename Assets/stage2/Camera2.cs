using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public float rotationSpeed = 100f;      // 회전 속도
    //public float maxVerticalAngle = 60f;    // 상하 회전 제한 (최대 60도)
    //private float currentVerticalRotation = 0f;  // 상하 회전 각도
    private float currentHorizontalRotation = 0f; // 좌우 회전 각도

    private float smoothing = 0.1f; // 회전 부드럽게 만들기 위한 보간 값
    //private float targetVerticalRotation = 0f;
    private float targetHorizontalRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        // 조이스틱의 X축 입력 (좌우 회전)
        float horizontalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        //// 조이스틱의 Y축 입력 (상하 회전)
        //float verticalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;

        // 좌우 회전 (Yaw)
        targetHorizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;

        //// 상하 회전 (Pitch)
        //targetVerticalRotation -= verticalInput * rotationSpeed * Time.deltaTime;
        //// 상하 회전 제한 (-maxVerticalAngle ~ maxVerticalAngle)
        //targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // 부드러운 회전 적용 (Lerp로 선형 보간)
        currentHorizontalRotation = Mathf.Lerp(currentHorizontalRotation, targetHorizontalRotation, smoothing);
        //currentVerticalRotation = Mathf.Lerp(currentVerticalRotation, targetVerticalRotation, smoothing);

        // 카메라 회전 적용 (상하 회전은 X축, 좌우 회전은 Y축)
        transform.localRotation = Quaternion.Euler(/*currentVerticalRotation*/0, currentHorizontalRotation, 0);
    }
}
