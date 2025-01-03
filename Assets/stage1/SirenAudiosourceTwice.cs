using UnityEngine;
using System.Collections;  // IEnumerator�� ����ϱ� ���� �߰��ؾ� �մϴ�.

public class PlayAudioTwice : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioClip;  // �ν����Ϳ��� ����� Ŭ���� �Ҵ��ϼ���.

    void Start()
    {
        // AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();

        // ����� �� �� ���
        StartCoroutine(PlayAudioTwiceCoroutine());
    }

    private IEnumerator PlayAudioTwiceCoroutine()
    {
        // ù ��° ���
        audioSource.Play();

        // ù ��° ����� ���� ������ ��ٸ���
        yield return new WaitForSeconds(audioSource.clip.length);

        // �� ��° ���
        audioSource.Play();
    }
}
