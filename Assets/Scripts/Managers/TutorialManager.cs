using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private float Step;
    [SerializeField] private float LerpTime;
    [SerializeField] private float HandOffsetX;
    [Space]
    [SerializeField] private Image Background;
    [SerializeField] private Image Hand;
    [SerializeField] private Image Dots;
    [SerializeField] private Text Text;

    private bool allowCoroutines = true;
    
    private void Start()
    {
        instance = this;

        StartCoroutine(StartAnim());
    }

    public IEnumerator StartAnim()
    {
        Background.DOFade(1, 1);
        Text.DOFade(1, 1);

        yield return new WaitForSeconds(1);

        StartCoroutine(AnimDots());
        StartCoroutine(AnimHand());
    }

    private IEnumerator AnimDots()
    {
        if (allowCoroutines)
        {
            Dots.DOFade(1, 1);
            Dots.fillAmount = Mathf.Lerp(Dots.fillAmount + Step, 1, LerpTime);
        
            yield return new WaitForEndOfFrame();

            if (Dots.fillAmount >= 1)
            {
                yield return Dots.DOFade(0, 1);
                Dots.fillAmount = 0;
            }

            StartCoroutine(AnimDots());
        }
    }

    private IEnumerator AnimHand()
    {
        if (allowCoroutines)
        {
            Hand.DOFade(1, 1);

            yield return Hand.transform.DOLocalMoveX(HandOffsetX, 2).From().WaitForCompletion();
            yield return Hand.DOFade(0, 0).WaitForCompletion();
            
            StartCoroutine(AnimHand());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            allowCoroutines = false;
            StartCoroutine(HideAll());
        }
    }

    private IEnumerator HideAll()
    {
        Background.DOFade(0, 1);
        Dots.DOFade(0, 1);
        Text.DOFade(0, 1);
        yield return Hand.DOFade(0, 1).WaitForCompletion();

        Background.transform.parent.gameObject.SetActive(false);
    }
}
