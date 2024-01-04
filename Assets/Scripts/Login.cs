using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject loginField;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject scores;
    [SerializeField] private GameObject hider;
    [SerializeField] private GameObject cloud;

    void Start()
    {
        if (PlayerPrefs.GetInt("reload") == 1) StartButton();
        else
        {
            startButton.SetActive(false);
            loginField.SetActive(true);

            player.SetActive(false);
            controls.SetActive(false);
            scores.SetActive(false);
            cloud.SetActive(false);
            hider.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void LoginButton()
    {
        startButton.SetActive(true);
        loginField.SetActive(false);

    }

    public void StartButton()
    {
        PlayerPrefs.SetInt("reload", 0);
        loginField.SetActive(false);
        startButton.SetActive(false);
        player.SetActive(true);
        controls.SetActive(true);
        scores.SetActive(true);
        hider.SetActive(false);
        cloud.SetActive(false);
    }

    public void Lost()
    {
        controls.SetActive(false);
        scores.SetActive(false);
        hider.SetActive(true);
        player.SetActive(false);
        cloud.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");

    }
}
