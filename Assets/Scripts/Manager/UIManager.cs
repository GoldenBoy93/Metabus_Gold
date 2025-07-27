using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ UI ���¸� �����ϴ� ������
public enum UIState
{
    Home,
    Game,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;
    private UIState currentState; // ���� UI ����

    private void Awake()
    {
        // �ڽ� ������Ʈ���� ������ UI�� ã�� �ʱ�ȭ
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        // �ʱ� ���¸� Ȩ ȭ������ ����
        ChangeState(UIState.Home);
    }

    public UIState GetCurrentState()
    {
        return currentState;
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }

    // ���� UI ���¸� �����ϰ�, �� UI ������Ʈ�� ���¸� ����
    public void ChangeState(UIState state)
    {
        currentState = state;

        // �� UI�� �ڽ��� �������� �� �������� �Ǵ��ϰ� ǥ�� ���� ����
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}