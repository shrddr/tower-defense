using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text ProgressText;

    void Start()
    {
        ProgressText.text = "Max Level Finished: " + PlayerPrefs.GetInt("MaxLevelFinished", 0);
    }
}
