using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject title;
    public Text scoreText;
    private void Update()
    {
        GameStart();
    }
    private void GameStart()
    {
        if (Input.anyKeyDown)
        {
            title.SetActive(false);
        }
    }
    public void GameOver(bool die)
    {
        if (die)
            StartCoroutine(GameReStart());
    }
    IEnumerator GameReStart()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpScore(int score)
    {
        scoreText.text = score.ToString();
    }
   
}
