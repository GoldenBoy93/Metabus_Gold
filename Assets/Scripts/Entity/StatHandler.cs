using UnityEngine;

// 캐릭터의 기본 능력치를 저장하고 관리하는 클래스
public class StatHandler : MonoBehaviour
{
    [Header("Character Stat Info")]

    // 체력 (1 ~ 100 사이 값만 허용)
    [Range(1, 100)][SerializeField] private int health = 10;

    // 외부에서 접근 가능한 프로퍼티 (값 변경 시 자동으로 0~100 사이로 제한)
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    // 이동 속도 (1f ~ 20f 사이 값만 허용)
    [Range(1f, 20f)][SerializeField] private float speed = 3;
    // 외부에서 접근 가능한 프로퍼티 (값 변경 시 0~20f로 제한)
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }


    [Header("Attack Info")]
    [SerializeField] private float attackDelay = 1f; // 공격 간 딜레이
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }

    [SerializeField] private float attackPower = 1f; // 공격력
    public float AttackPower { get => attackPower; set => attackPower = value; }

    [SerializeField] private float attackRange = 10f; // 공격 가능 범위
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target; // 공격 가능한 대상 레이어

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // 넉백 적용 여부
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f; // 넉백 힘
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f; // 넉백 지속 시간
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    public Vector2 collideBoxSize = Vector2.one; // 공격 범위 (충돌 박스 크기)

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Animator animator;

    public BaseController Controller { get; private set; } // 사용하는 캐릭터 컨트롤러



    

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
    }

    protected void Start()
    {
        
    }

    public void Attack()
    {
        AttackAnimation();

        // BoxCast로 근접 공격 판정 (LookDirection 방향으로 충돌 검사)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, // 위치
            collideBoxSize,              // 박스 크기
            0,                           // 회전 없음
            Vector2.zero,                // 이동 거리 없음 (고정된 위치)
            1,                           // 거리 0 (한 번만 검사)
            target                       // 공격 가능한 대상 레이어 마스크
        );

        if (hit.collider != null)
        {
            Debug.Log("hit");
            // 대상에게 체력 감소 적용
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                Debug.Log("Damage");
                resourceController.ChangeHealth(-AttackPower); // 데미지 적용

                // 넉백 효과가 설정되어 있을 경우 적용
                if (IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }
}