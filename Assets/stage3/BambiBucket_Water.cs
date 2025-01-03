using UnityEngine;

public class BambiBucket_Water : MonoBehaviour
{
    public ParticleSystem waterSpray;  // 물 방출 파티클 시스템
    public AudioSource waterSound;     // 물 방출 사운드
    public AudioClip waterSplashSound; // 물이 떨어지는 소리
    public AudioClip waterFlowSound;   // 물이 흐르는 소리 (물 쏟아지는 소리)
    public float sprayForce = 10f;     // 물 방출의 세기
    public float sprayDuration = 5f;   // 물을 쏘는 지속 시간
    private bool isSpraying = false;
    private float sprayTimeRemaining;

    public Transform helicopterTransform; // 헬리콥터의 Transform
    public Transform bucketTransform;     // 밤비버킷의 위치
    public float bucketOffset = 2f;       // 밤비버킷과 헬리콥터 사이의 거리

    public ParticleSystem splashParticles; // 물이 튕길 때의 파티클 시스템 (추가)

    // 물 방출을 제어하는 플래그와 타이머를 관리
    void Update()
    {
        // 물 방출 중이라면 지속 시간 체크
        if (isSpraying)
        {
            sprayTimeRemaining -= Time.deltaTime;
            if (sprayTimeRemaining <= 0)
            {
                StopSpraying();  // 지정한 시간 후 자동으로 멈추기
            }
        }

        // 헬리콥터를 기준으로 밤비버킷 위치 업데이트
        Vector3 bucketPosition = helicopterTransform.position + helicopterTransform.forward * bucketOffset;
        bucketTransform.position = bucketPosition; // 밤비버킷의 위치를 헬리콥터 앞쪽으로 설정

        // 밤비버킷의 회전도 헬리콥터와 함께 회전하도록 설정
        bucketTransform.rotation = helicopterTransform.rotation;
    }

    // 물 방출 시작
    void StartSpraying()
    {
        if (!isSpraying)  // 이미 물 방출 중이지 않으면
        {
            isSpraying = true;
            sprayTimeRemaining = sprayDuration;  // 방출 시간이 지나면 멈추도록 설정
            waterSpray.Play(); // 물 방출 시작
            waterSound.clip = waterFlowSound;   // 물 흐르는 소리 설정
            waterSound.loop = true;             // 반복해서 소리 재생
            waterSound.Play();                  // 물 방출 소리 재생

            // 물 방출 방향 조정
            var main = waterSpray.main;
            main.startSpeed = sprayForce; // 물의 방출 속도 조정
            waterSpray.transform.rotation = bucketTransform.rotation; // 밤비버킷의 회전값을 물 방출 방향에 반영
        }
    }

    // 물 방출 중지
    void StopSpraying()
    {
        if (isSpraying)
        {
            isSpraying = false;
            waterSpray.Stop(); // 물 방출 중지
            waterSound.Stop(); // 물 방출 소리 중지
            // 물 방출이 끝나면 파티클 시스템 비활성화
            waterSpray.gameObject.SetActive(false);
        }
    }

    // 물 튕김 효과를 위한 Collision 설정
    void OnCollisionEnter(Collision collision)
    {
        // 물이 다른 물체와 충돌할 때 튕기는 소리 또는 파티클 추가
        if (collision.relativeVelocity.magnitude > 1f && splashParticles != null)
        {
            // 물이 떨어지는 위치에서 소리 재생
            AudioSource.PlayClipAtPoint(waterSplashSound, collision.contacts[0].point);

            // 물 튕기는 파티클 생성
            splashParticles.transform.position = collision.contacts[0].point; // 충돌 지점에 파티클 설정
            splashParticles.Play(); // 파티클 재생
        }
    }

    // 헬리콥터와 화살표의 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        // 헬리콥터와 화살표가 충돌할 때 물 방출을 시작하도록 설정
        if (other.CompareTag("Arrow") && !isSpraying)  // 화살표의 태그가 "Arrow"일 경우
        {
            StartSpraying();  // 물 방출 시작
        }
    }

    // 물 방출 종료 후 성능 최적화를 위해 파티클 시스템을 비활성화
    void OnDisable()
    {
        if (waterSpray != null)
        {
            waterSpray.gameObject.SetActive(false); // 비활성화하여 성능 최적화
        }
    }

    // 물 방출 시작 전에 물 방출 세기를 더 정밀하게 설정할 수 있는 함수 추가
    public void AdjustSprayForce(float newForce)
    {
        sprayForce = Mathf.Clamp(newForce, 0f, 50f); // 세기는 0부터 50까지 조정 가능
        if (isSpraying)
        {
            var main = waterSpray.main;
            main.startSpeed = sprayForce;
        }
    }
}
