using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StateMachineBehaviour�� ��ӹ޾� Animator ���¿� ���� ����
public class ResetAttackParameters : StateMachineBehaviour
{
    // Animator ���¸� �������� �� ȣ���
    // stateInfo: ���� ���� ����
    // layerIndex: ���� ���̾� �ε���
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hAxisRaw�� vAxisRaw �Ķ���͸� 0���� �ʱ�ȭ
        animator.SetFloat("hAxisRaw_A", 0f);
        animator.SetFloat("vAxisRaw_A", 0f);

        Debug.Log("Attack Blend Tree ���� ����! hAxisRaw_A, vAxisRaw_A �ʱ�ȭ��.");
    }
}
