using UnityEngine;
using UnityEngine.UI;

public class LevelNameText : MonoBehaviour
{
    public static LevelNameText instance;
    public Text LevelText;
    
    private void Start()
    {
        instance = this;
        
        LevelText = GetComponent<Text>();
    }
}
