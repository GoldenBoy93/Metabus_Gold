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
        UpdatePlayerHPSlider(1); // 시작 시 체력 슬라이더를 가득 채움 (100%)
    }

    // 체력 슬라이더 값을 퍼센트(0~1)로 설정
    public void UpdatePlayerHPSlider(float percentage)
    {
        playerHpSlider.value = percentage;
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}