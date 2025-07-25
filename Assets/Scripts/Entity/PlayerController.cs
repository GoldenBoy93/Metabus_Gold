using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager; // 게임 매니저 연결
    }

    protected override void HandleAction()
    {
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        float vertical = Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        if (hDown || hUp)
            isHorizontalMove = true;
        else if (vDown || vUp)
            isHorizontalMove = false;

        movementDirection = isHorizontalMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);

        // 공격키 지정 (z키)
        isAttacking = Input.GetKeyDown(KeyCode.Z);
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // 게임 오버 처리
    }
}
