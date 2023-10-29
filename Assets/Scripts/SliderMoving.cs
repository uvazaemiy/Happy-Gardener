using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderMoving : MonoBehaviour
{
    [SerializeField] private float LerpTime;
    [SerializeField] private float Step;
    [SerializeField] private Slider slider;
    [SerializeField] private Image GlobalFade;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        
        slider.gameObject.SetActive(true);
        yield return StartCoroutine(ChangeSlider());
        
        yield return new WaitForSeconds(1);
        yield return GlobalFade.DOFade(1, 1).WaitForCompletion();

        DOTween.KillAll();

        int realLevel = 1;
        if (PlayerPrefs.GetInt("realLevel") != 0)
            realLevel = PlayerPrefs.GetInt("realLevel");
        SceneManager.LoadScene(realLevel);
    }

    private IEnumerator ChangeSlider()
    {
        slider.value = Mathf.Lerp(slider.value + Step, 1, LerpTime);
        yield return new WaitForEndOfFrame();
        if (slider.value != 1)
            StartCoroutine(ChangeSlider());
    }
}
