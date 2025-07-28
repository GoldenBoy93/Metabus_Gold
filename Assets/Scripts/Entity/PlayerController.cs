using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public GameManager gameManager;
    GameObject scanObject;
    
    public GameObject gameUI; // PlayerController ��ũ��Ʈ�� GameUI ������Ʈ�� ������ �� �ֵ��� �ν����Ϳ� �߰�

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager; // ���� �Ŵ��� ����
    }

    protected override void HandleAction()
    {
        // �������� �ƴҰ�� ���� => �÷��̾� ��Ʈ�� �Ұ�
        if (!gameUI.activeSelf)
        {
            return;
        }

        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        // ��ũ�׼��� true �����϶��� Ű�Է� �ȵ�
        float horizontal = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��

        bool hDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isTalkAction ? false : Input.GetButtonDown("Horizontal");
        bool vUp = gameManager.isTalkAction ? false : Input.GetButtonDown("Vertical");

        if (hDown || hUp)
            isHorizontalMove = true;
        else if (vDown || vUp)
            isHorizontalMove = false;

        // 4�������θ� �����̱�
        movementDirection = isHorizontalMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);

        // �÷��̾ �̵� ���� ���� lookDirection ������Ʈ
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }

        // ����Ű ���� (zŰ)
        isAttacking = Input.GetKeyDown(KeyCode.Z);

        if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            //Debug.Log($"This is {scanObject.name}");
            
            gameManager.TalkAction(scanObject);
        }
    }

    protected override void HandleAction2()
    {
        // ����׼� Ray
        Debug.DrawRay(_rigidbody.position, lookDirection * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(_rigidbody.position, LookDirection, 1f, LayerMask.GetMask("NPC"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // ���� ���� ó��
    }
}
