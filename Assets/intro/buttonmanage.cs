using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonmanage : MonoBehaviour
{
    // 조작법 및 종료 확인 창
    public GameObject controlPanel;
    public GameObject exitPanel;

    // 조작법 버튼 클릭 시 실행되는 함수
    public void OnControlButtonClick()
    {
        // 조작법 창 활성화
        controlPanel.SetActive(true);
        exitPanel.SetActive(false); // 다른 창은 비활성화
    }

    // 종료 버튼 클릭 시 실행되는 함수
    public void OnExitButtonClick()
    {
        // 종료 확인 창 활성화
        exitPanel.SetActive(true);
        controlPanel.SetActive(false); // 다른 창은 비활성화
    }

    // 창 닫기
    public void ClosePanels()
    {
        controlPanel.SetActive(false);
        exitPanel.SetActive(false);
    }
}
