using UnityEngine;

// ��� ĳ������ �⺻ ������, ȸ��, �˹� ó���� ����ϴ� ��� Ŭ����
public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ


    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero; // �˹� ����
    private float knockbackDuration = 0.0f; // �˹� ���� �ð�
    protected AnimationHandler animationHandler;
    protected StatHandler statHandler; // ĳ������ �ɷ�ġ(�ӵ�, ü�� ��)�� ��� �ִ� ������Ʈ


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

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // ��� ������ �Ŀ���ŭ �ݴ�� �о
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        // ���� ��ٿ� ���̸� �ð� ����
        if (timeSinceLastAttack <= statHandler.AttackDelay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        // ���� �Է� ���̰� ��Ÿ���� �������� ���� ����
        if (isAttacking && timeSinceLastAttack > statHandler.AttackDelay)
        {
            timeSinceLastAttack = 0;
            Attack(); // ���� ���� ����
        }
    }

    protected virtual void Attack()
    {
        statHandler.Attack();
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

        // ��� ������Ʈ(��ũ��Ʈ ����) ��Ȱ��ȭ
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // 1�� �� ������Ʈ �ı�
        Destroy(gameObject, 1f);
    }
}