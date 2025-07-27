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
        UpdateHPSlider(1); // 시작 시 체력 슬라이더를 가득 채움 (100%)
    }

    // 체력 슬라이더 값을 퍼센트(0~1)로 설정
    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}