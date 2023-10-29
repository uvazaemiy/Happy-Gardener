using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private Canvas canvas;
    [Space]
    [Header("Settings")]
    [SerializeField] private float time = 0.2f;
    [SerializeField] private float ySettingsMoving;
    [SerializeField] private Transform SettingsButton;
    [SerializeField] private Transform SFXButton;
    [SerializeField] private Transform MusicButton;

    [Space] 
    [Header("Win Lose Panel")] 
    [SerializeField] private Image LevelFade;
    [SerializeField] private Image WinLoseTextPanel;
    [SerializeField] private Text WinLoseText;
    private Color WinColor = new Color(0.627451f, 1, 0.5686275f, 0);
    private Color LoseColor = new Color(1, 0.6340855f, 0.5686275f, 0);
    [Space]
    [SerializeField] private Image NextButtonImage;
    [SerializeField] private Text NextButtonText;
    [SerializeField] private Image NextButtonArrow;
    [FormerlySerializedAs("restartButtonImage")]
    [Space]
    [SerializeField] private Image RestartButtonImage;
    [SerializeField] private Text RestartButtonText;
    [Space]
    public Image GlobalFade;
    
    private Image SFXImage;
    private Image MusicImage;

    private float xOffset = 1;
    private float yOffset = 1;
    private bool isMoving;
    private bool stateOfSettings;

    private void Start()
    {
        instance = this;

        xOffset = canvas.transform.position.x / 360;
        yOffset = canvas.transform.position.y / 720;
        
        SFXImage = SFXButton.GetComponent<Image>();
        MusicImage = MusicButton.GetComponent<Image>();
    }
    
    public void MoveSettingsButtons()
    {
        if (!isMoving)
            StartCoroutine(MoveButtonsRoutine());
    }
    
    private IEnumerator MoveButtonsRoutine()
    {
        isMoving = true;

        if (!stateOfSettings)
        {
            SFXButton.gameObject.SetActive(true);
            //MusicButton.gameObject.SetActive(true);

            SFXImage.DOFade(1, time);
            MusicImage.DOFade(1, time);

            SFXButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving, time);
            yield return MusicButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving * 2, time).WaitForCompletion();
        }
        else
        {
            SFXImage.DOFade(0, time);
            MusicImage.DOFade(0, time);
            
            SFXButton.DOMoveY(SettingsButton.position.y, time);
            yield return MusicButton.DOMoveY(SettingsButton.position.y, time).WaitForCompletion();
            
            SFXButton.gameObject.SetActive(false);
            //MusicButton.gameObject.SetActive(false);
        }
        
        stateOfSettings = !stateOfSettings;
        isMoving = false;
    }

    public void RestartButton()
    {
        GameManager.instance.RestartLevel();
    }

    public IEnumerator ShowWinPanel()
    {
        WinLoseText.text = "YOU WIN!";
        WinLoseText.color = WinColor;
        NextButtonImage.gameObject.SetActive(true);
        
        LevelFade.DOFade(0.4f, 1);
        
        WinLoseText.DOFade(1, 1);
        WinLoseTextPanel.DOFade(0.7f, 1);
        NextButtonImage.DOFade(1, 1);
        NextButtonText.DOFade(1, 1);
        NextButtonArrow.DOFade(1, 1);
        
        WinLoseTextPanel.transform.DOLocalMoveX(100, 1).From();
        NextButtonImage.transform.DOLocalMoveX(-100, 1).From();

        yield return new WaitForSeconds(1);
    }
    
    public IEnumerator ShowLosePanel()
    {
        WinLoseText.text = "YOU LOSE";
        WinLoseText.color = LoseColor;
        RestartButtonImage.gameObject.SetActive(true);

        LevelFade.DOFade(0.4f, 1);
        
        WinLoseText.DOFade(1, 1);
        WinLoseTextPanel.DOFade(0.7f, 1);
        RestartButtonImage.DOFade(1, 1);
        RestartButtonText.DOFade(1, 1);
        
        WinLoseTextPanel.transform.DOLocalMoveX(100, 1).From();
        RestartButtonImage.transform.DOLocalMoveX(-100, 1).From();

        yield return new WaitForSeconds(1);
    }

    public IEnumerator HideAll()
    {
        WinLoseText.DOFade(0, 1);
        WinLoseTextPanel.DOFade(0, 1);
        RestartButtonImage.DOFade(0, 1);
        RestartButtonText.DOFade(0, 1);
        NextButtonImage.DOFade(0, 1);
        NextButtonText.DOFade(0, 1);
        NextButtonArrow.DOFade(0, 1);
        
        WinLoseTextPanel.transform.DOLocalMoveX(100, 1);
        RestartButtonImage.transform.DOLocalMoveX(-100, 1);
        NextButtonImage.transform.DOLocalMoveX(-100, 1);

        yield return new WaitForSeconds(1);
    }
}
