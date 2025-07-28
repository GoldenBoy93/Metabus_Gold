using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public GameManager gameManager;
    GameObject scanObject;
    
    public GameObject gameUI; // PlayerController 스크립트에 GameUI 오브젝트를 연결할 수 있도록 인스펙터에 추가

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager; // 게임 매니저 연결
    }

    protected override void HandleAction()
    {
        // 게임중이 아닐경우 리턴 => 플레이어 컨트롤 불가
        if (!gameUI.activeSelf)
        {
            return;
        }

        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        // 토크액션이 true 상태일때는 키입력 안됨
        float horizontal = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        float vertical = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓

        bool hDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isTalkAction ? false : Input.GetButtonDown("Horizontal");
        bool vUp = gameManager.isTalkAction ? false : Input.GetButtonDown("Vertical");

        if (hDown || hUp)
            isHorizontalMove = true;
        else if (vDown || vUp)
            isHorizontalMove = false;

        // 4방향으로만 움직이기
        movementDirection = isHorizontalMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);

        // 플레이어가 이동 중일 때만 lookDirection 업데이트
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }

        // 공격키 지정 (z키)
        isAttacking = Input.GetKeyDown(KeyCode.Z);

        if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            //Debug.Log($"This is {scanObject.name}");
            
            gameManager.TalkAction(scanObject);
        }
    }

    protected override void HandleAction2()
    {
        // 조사액션 Ray
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
        gameManager.GameOver(); // 게임 오버 처리
    }
}
