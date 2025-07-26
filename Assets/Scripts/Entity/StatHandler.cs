using UnityEngine;

// ĳ������ �⺻ �ɷ�ġ�� �����ϰ� �����ϴ� Ŭ����
public class StatHandler : MonoBehaviour
{
    [Header("Character Stat Info")]

    // ü�� (1 ~ 100 ���� ���� ���)
    [Range(1, 100)][SerializeField] private int health = 10;

    // �ܺο��� ���� ������ ������Ƽ (�� ���� �� �ڵ����� 0~100 ���̷� ����)
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    // �̵� �ӵ� (1f ~ 20f ���� ���� ���)
    [Range(1f, 20f)][SerializeField] private float speed = 3;
    // �ܺο��� ���� ������ ������Ƽ (�� ���� �� 0~20f�� ����)
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }


    [Header("Attack Info")]
    [SerializeField] private float attackDelay = 1f; // ���� �� ������
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }

    [SerializeField] private float attackPower = 1f; // ���ݷ�
    public float AttackPower { get => attackPower; set => attackPower = value; }

    [SerializeField] private float attackRange = 10f; // ���� ���� ����
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target; // ���� ������ ��� ���̾�

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // �˹� ���� ����
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f; // �˹� ��
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f; // �˹� ���� �ð�
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    public Vector2 collideBoxSize = Vector2.one; // ���� ���� (�浹 �ڽ� ũ��)

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Animator animator;

    public BaseController Controller { get; private set; } // ����ϴ� ĳ���� ��Ʈ�ѷ�



    

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

        // BoxCast�� ���� ���� ���� (LookDirection �������� �浹 �˻�)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, // ��ġ
            collideBoxSize,              // �ڽ� ũ��
            0,                           // ȸ�� ����
            Vector2.zero,                // �̵� �Ÿ� ���� (������ ��ġ)
            1,                           // �Ÿ� 0 (�� ���� �˻�)
            target                       // ���� ������ ��� ���̾� ����ũ
        );

        if (hit.collider != null)
        {
            Debug.Log("hit");
            // ��󿡰� ü�� ���� ����
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                Debug.Log("Damage");
                resourceController.ChangeHealth(-AttackPower); // ������ ����

                // �˹� ȿ���� �����Ǿ� ���� ��� ����
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