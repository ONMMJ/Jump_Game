using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] Text clearText;

    [Header("Timer")]
    [SerializeField] Text timer;
    [SerializeField] float maxTime;

    bool isGameStart;
    bool isFirst;
    float time;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        time = maxTime;
        Timer(time);
        isFirst = true;
    }
    private void Update()
    {
        if(isGameStart)
        {
            time = Mathf.Clamp(time - Time.deltaTime, 0, maxTime);
            Timer(time);
            if (time <= 0.0)
            {
                OnGameOver();
            }
        }
    }

    private void Timer(double time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timeText = string.Format("{0:D2} : {1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timer.text = timeText;
    }
    private void OnGameOver()
    {
        isGameStart = false;
        time = maxTime;
        Player.Instance.LockControl(true);

        clearText.text = "GAME OVER";
        clearText.color = Color.red;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnGameStart()
    {
        if (isFirst)
        {
            isFirst = false;
            isGameStart = true;
            panel.gameObject.SetActive(false);
            Player.Instance.LockControl(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
            SceneManager.LoadScene(0);
    }
    public void OnGameClear()
    {
        isGameStart = false;
        Player.Instance.LockControl(true);

        clearText.text = "GAME CLEAR";
        clearText.color = Color.green;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
