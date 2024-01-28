using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Transform lifeBar;
    [SerializeField]
    private GameObject lifeIconPrefab;
    [SerializeField]
    private Image dashIcon, vaultIcon;

    [SerializeField]
    private TMP_Text timeTextIndicator;

    [SerializeField]
    private GameObject gameOverPopupGO;
    [SerializeField]
    private TMP_Text gameOverTitleTextElement, gameOverTimeTextElement;

    private float gameTimeElapsed = 0f, gameTimeStartTimeStamp = 0f;

    private bool isTimerActive = false;

    private void Awake()
    {
        gameOverPopupGO.SetActive(false);
    }

    private void Update()
    {

        if (isTimerActive)
        {
            gameTimeElapsed = Time.time - gameTimeStartTimeStamp;
            float seconds = (float)Math.Round(gameTimeElapsed % 60, 2);
            int minutes = (int)gameTimeElapsed / 60;
            timeTextIndicator.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString().Replace(".", ":").Replace(",", ":");
        }
    }

    public void GameOver(bool isWin)
    {
        StopGameTimer();
        gameOverPopupGO.SetActive(true);
        gameOverTitleTextElement.text = isWin ? "You Win" : "Game Over";

        float seconds = (float)Math.Round(gameTimeElapsed % 60, 2);
        int minutes = (int)gameTimeElapsed / 60;
        gameOverTimeTextElement.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString().Replace(".", ":").Replace(",", ":");
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
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
        isTimerActive = true;
    }
    public void StopGameTimer()
    {
        isTimerActive = false;
    }

    public void SetLifeAmount(int howManyLeft)
    {
        foreach (Transform child in lifeBar)
            Destroy(child.gameObject);

        for (int i = 0; i < howManyLeft; i++)
            Instantiate(lifeIconPrefab, lifeBar);
    }
}
