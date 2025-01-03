using UnityEngine;
using System.Collections;  // IEnumerator를 사용하기 위해 추가해야 합니다.

public class PlayAudioTwice : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioClip;  // 인스펙터에서 오디오 클립을 할당하세요.

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();

        // 오디오 두 번 재생
        StartCoroutine(PlayAudioTwiceCoroutine());
    }

    private IEnumerator PlayAudioTwiceCoroutine()
    {
        // 첫 번째 재생
        audioSource.Play();

        // 첫 번째 재생이 끝날 때까지 기다리기
        yield return new WaitForSeconds(audioSource.clip.length);

        // 두 번째 재생
        audioSource.Play();
    }
}
