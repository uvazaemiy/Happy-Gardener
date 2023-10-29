using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public BallMovement Player;
    [SerializeField] private GameObject WinLosePanel;

    private bool win;
    private bool lose;
    private int levelNumber;
    private int uiLevelNumber = 0;
    
    private IEnumerator Start()
    {
        instance = this;
        
        Application.targetFrameRate = 120;
        
        levelNumber = SceneManager.GetActiveScene().buildIndex;
        
        uiLevelNumber = PlayerPrefs.GetInt("uiLevelNumber");
        if (uiLevelNumber == 0)
            uiLevelNumber = 1;

        LevelNameText.instance.LevelText.text = "Level " + (uiLevelNumber).ToString();
        
        yield return UIController.instance.GlobalFade.DOFade(0, 1).WaitForCompletion();
        Player.allowMove = true;
    }

    public IEnumerator Win()
    {
        win = true;
        yield return new WaitForSeconds(0.75f);
        
        if (win)
        {
            SoundController.instance.PlayWinSound();

            Player.allowMove = false;
            WinLosePanel.SetActive(true);
            
            yield return StartCoroutine(UIController.instance.ShowWinPanel());
        }
    }

    public IEnumerator Lose()
    {
        win = false;

        if (!lose)
        {
            lose = true;
            SoundController.instance.PlayLoseSound();
        
            Player.allowMove = false;
            WinLosePanel.SetActive(true);

            yield return StartCoroutine(UIController.instance.ShowLosePanel());
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelRoutine());
    }

    private IEnumerator LoadNextLevelRoutine()
    {
        uiLevelNumber++;
        PlayerPrefs.SetInt("uiLevelNumber", uiLevelNumber);
        
        StartCoroutine(UIController.instance.HideAll());
        yield return UIController.instance.GlobalFade.DOFade(1, 1.5f).WaitForCompletion();

        DOTween.KillAll();

        if (levelNumber == 18)
        {
            PlayerPrefs.SetInt("realLevel", 4);
            SceneManager.LoadScene(4);
        }
        else
        {
            PlayerPrefs.SetInt("realLevel", levelNumber + 1);
            SceneManager.LoadScene(levelNumber + 1);
        }
    }

    public void RestartLevel()
    {
        if (!win)
            StartCoroutine(RestartLevelRoutine());
    }

    private IEnumerator RestartLevelRoutine()
    {
        Player.allowMove = false;

        StartCoroutine(UIController.instance.HideAll());
        yield return UIController.instance.GlobalFade.DOFade(1, 1.5f).WaitForCompletion();

        DOTween.KillAll();
        SceneManager.LoadScene(levelNumber);
    }
}
