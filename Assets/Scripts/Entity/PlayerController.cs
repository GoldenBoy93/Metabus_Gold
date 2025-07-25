using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager; // ���� �Ŵ��� ����
    }

    protected override void HandleAction()
    {
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        if (hDown || hUp)
            isHorizontalMove = true;
        else if (vDown || vUp)
            isHorizontalMove = false;

        movementDirection = isHorizontalMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);

        // ����Ű ���� (zŰ)
        isAttacking = Input.GetKeyDown(KeyCode.Z);
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // ���� ���� ó��
    }
}
