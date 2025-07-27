using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Slider playerHpSlider;

    private void Start()
    {
        UpdatePlayerHPSlider(1); // ���� �� ü�� �����̴��� ���� ä�� (100%)
    }

    // ü�� �����̴� ���� �ۼ�Ʈ(0~1)�� ����
    public void UpdatePlayerHPSlider(float percentage)
    {
        playerHpSlider.value = percentage;
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}