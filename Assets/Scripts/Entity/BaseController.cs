using UnityEngine;

// ��� ĳ������ �⺻ ������, ȸ��, �˹� ó���� ����ϴ� ��� Ŭ����
public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ ���� ������
    [SerializeField] private Transform weaponPivot; // ���⸦ ȸ����ų ���� ��ġ

    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero; // �˹� ����
    private float knockbackDuration = 0.0f; // �˹� ���� �ð�
    protected AnimationHandler animationHandler;
    protected StatHandler statHandler; // ĳ������ �ɷ�ġ(�ӵ�, ü�� ��)�� ��� �ִ� ������Ʈ

    [SerializeField] public WeaponHandler WeaponPrefab; // ������ ���� ������ (������ �ڽĿ��� ã�� ���)
    protected WeaponHandler weaponHandler; // ������ ����

    protected bool isAttacking; // ���� �� ����
    private float timeSinceLastAttack = float.MaxValue; // ������ ���� ���� ��� �ð�

    protected bool isHorizontalMove;

    // ���� �޼���� �⺻������ �θ� Ŭ�������� ���ǵǰ� �ڽ� Ŭ�������� �������� �� �ִ� �޼����Դϴ�.
    // ���� �޼���� `virtual` Ű���带 ����Ͽ� ����Ǹ�, �ڽ� Ŭ�������� �ʿ信 ���� �����ǵ� �� �ֽ��ϴ�.
    // �̸� ���� �ڽ� Ŭ�������� �θ� Ŭ������ �޼��带 �����ϰų� Ȯ���� �� �ֽ��ϴ�.
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        // �������� �����Ǿ� �ִٸ� �����ؼ� ���� ��ġ�� ����
        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>(); // �̹� �پ� �ִ� ���� ���
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        //Rotate(lookDirection);
        HandleAttackDelay(); // ���� �Է� �� ��Ÿ�� ����
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // �˹� �ð� ����
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed; // �̵� �ӵ�

        // �˹� ���̸� �̵� �ӵ� ���� + �˹� ���� ����
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �̵� �ӵ� ����
            direction += knockback; // �˹� ���� �߰�
        }

        // ���� ���� �̵�
        _rigidbody.velocity = direction;
        // �̵� �ִϸ��̼� ó��
        animationHandler.Move(direction);
    }

    // ĳ���� ȸ�� ���� ��°� (���� �¿� �ø��� ����)
    //private void Rotate(Vector2 direction)
    //{
    //    float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    bool isLeft = Mathf.Abs(rotZ) > 90f;

    //    // ��������Ʈ �¿� ����
    //    characterRenderer.flipX = isLeft;

    //    if (weaponPivot != null)
    //    {
    //        weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    //    }

    //    // ���⵵ �Բ� �¿� ���� ó��
    //    weaponHandler?.Rotate(isLeft);
    //}

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // ��� ������ �Ŀ���ŭ �ݴ�� �о
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
        {
            return;
        }

        // ���� ��ٿ� ���̸� �ð� ����
        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        // ���� �Է� ���̰� ��Ÿ���� �������� ���� ����
        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack(); // ���� ���� ����
        }
    }

    protected virtual void Attack()
    {
        // �ٶ󺸴� ������ ���� ���� ����
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    public virtual void Death()
    {
        // ������ ����
        _rigidbody.velocity = Vector3.zero;

        // ��� �ִϸ��̼� ó��
        animationHandler.Death();

        // ��� SpriteRenderer�� ���� ���缭 ���� ȿ�� ����
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.5f;
            renderer.color = color;
        }

        weaponHandler = null;

        // ��� ������Ʈ(��ũ��Ʈ ����) ��Ȱ��ȭ
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 1�� �� ������Ʈ �ı�
        Destroy(gameObject, 1f);
    }
}