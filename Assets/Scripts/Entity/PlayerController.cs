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

        // 핸들액션은 베이스컨트롤러에서 계속 업데이트가 되는데 룩다이렉션을 조건안에 넣어서
        // movementDirection이 (0,0)이 아닐 때만(=움직이고 있을때만) lookDirection이 업데이트 되도록 만듦
        // 즉 멈추면 바라보는 방향 그대로 정지되게 만듦
        //if (movementDirection.magnitude > 0) 
        //{
        //    lookDirection = movementDirection;
        //}

        // 공격키 지정 (z키)
        isAttacking = Input.GetKeyDown(KeyCode.Z);
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver(); // 게임 오버 처리
    }
}
