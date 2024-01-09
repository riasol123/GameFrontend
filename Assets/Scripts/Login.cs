using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject loginField;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject scores;
    [SerializeField] private GameObject hider;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject error;
    [SerializeField] private Text email;
    [SerializeField] private Text password;
    [SerializeField] private Text record;

    void Start()
    {
        PlayerPrefs.SetString("token", "");
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

        if (PlayerPrefs.GetString("token") != "")
        {
            startButton.SetActive(true);
            loginField.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void LoginButton()
    {
        error.SetActive(false);
        StartCoroutine(LoginFun());
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
        StartCoroutine(SetProgress());
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");

    }

    IEnumerator SetProgress()
    {
        string url = "http://192.168.46.177:5000/auth/score/" + PlayerPrefs.GetString("token");

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        string jsonData = "{\"progress\":\"" + (PlayerPrefs.GetInt("Score")).ToString() + "\"}";

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
    }

    IEnumerator GetProgress()
    {
        string url = "http://192.168.46.177:5000/auth/whoami/" + PlayerPrefs.GetString("token");

        UnityWebRequest request = new UnityWebRequest(url, "GET");

        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        
        if (request.isNetworkError || request.isHttpError)
        {
        }
        else
        {
            string response = request.downloadHandler.text;
            PlayerPrefs.SetInt("Score", Convert.ToInt32(response));
            record.text = (PlayerPrefs.GetInt("Score")).ToString();
        }


    }

    IEnumerator LoginFun()
    {
        string urlLogin = "http://192.168.46.177:5000/auth/login";

        UnityWebRequest request = new UnityWebRequest(urlLogin, "POST");

        string jsonData = "{\"email\":\"" + email.text + "\", \"password\":\"" + password.text + "\"}";

        request.SetRequestHeader("Content-Type", "application/json");

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            string urlReg = "http://192.168.46.177:5000/auth/registration";
            UnityWebRequest requestReg = new UnityWebRequest(urlReg, "POST");

            string jsonDataReg = "{\"email\":\"" + email.text + "\", \"password\":\"" + password.text + "\"}";

            requestReg.SetRequestHeader("Content-Type", "application/json");

            byte[] postDataReg = System.Text.Encoding.UTF8.GetBytes(jsonDataReg);
            requestReg.uploadHandler = new UploadHandlerRaw(postDataReg);
            requestReg.downloadHandler = new DownloadHandlerBuffer();

            yield return requestReg.SendWebRequest();
            if (requestReg.isNetworkError || requestReg.isHttpError) error.SetActive(true);
            else 
            {
                string response = requestReg.downloadHandler.text;
                PlayerPrefs.SetString("token", response);
                startButton.SetActive(true);
                loginField.SetActive(false);
                StartCoroutine(GetProgress());
            }
        }
        else
        {
            string response = request.downloadHandler.text;
            PlayerPrefs.SetString("token", response);
            startButton.SetActive(true);
            loginField.SetActive(false);
            StartCoroutine(GetProgress());
        }


    }
}
