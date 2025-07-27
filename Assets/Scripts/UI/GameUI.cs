using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Slider hpSlider;

    private void Start()
    {
        UpdateHPSlider(1); // ���� �� ü�� �����̴��� ���� ä�� (100%)
    }

    // ü�� �����̴� ���� �ۼ�Ʈ(0~1)�� ����
    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}