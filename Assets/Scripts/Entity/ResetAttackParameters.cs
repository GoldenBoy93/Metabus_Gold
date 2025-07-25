using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StateMachineBehaviour를 상속받아 Animator 상태에 직접 연결
public class ResetAttackParameters : StateMachineBehaviour
{
    // Animator 상태를 빠져나갈 때 호출됨
    // stateInfo: 현재 상태 정보
    // layerIndex: 현재 레이어 인덱스
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hAxisRaw와 vAxisRaw 파라미터를 0으로 초기화
        animator.SetFloat("hAxisRaw_A", 0f);
        animator.SetFloat("vAxisRaw_A", 0f);

        Debug.Log("Attack Blend Tree 상태 종료! hAxisRaw_A, vAxisRaw_A 초기화됨.");
    }
}
