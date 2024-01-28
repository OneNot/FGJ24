using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Image dashIcon, vaultIcon;

    [SerializeField]
    private TMP_Text timeTextIndicator;

    private float gameTimeElapsed = 0f, gameTimeStartTimeStamp = 0f;

    private void Awake()
    {

    }

    private void Update()
    {
        gameTimeElapsed = Time.time - gameTimeStartTimeStamp;
        float seconds = (float)Math.Round(gameTimeElapsed % 60, 2);
        int minutes = (int)gameTimeElapsed / 60;
        timeTextIndicator.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString().Replace(".", ":").Replace(",", ":");
    }

    public void SetDashState(bool setEnabled)
    {
        dashIcon.color = new Color(dashIcon.color.r, dashIcon.color.g, dashIcon.color.b, setEnabled ? 1f : 0.45f);
    }

    public void SetVaultState(bool setEnabled)
    {
        vaultIcon.color = new Color(vaultIcon.color.r, vaultIcon.color.g, vaultIcon.color.b, setEnabled ? 1f : 0.45f);
    }

    public void StartGameTimer()
    {
        gameTimeElapsed = 0f;
        gameTimeStartTimeStamp = Time.time;
    }
}
