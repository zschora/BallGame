using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button[] levelButtons;
    public TMP_Text[] levelStars;

    void Start()
    {
        int unlockedLevel = GameManager.ProgressManager.GetMaxUnlockedLevelIndex();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i <= unlockedLevel);
        }

        for (int i = 0; i < levelStars.Length; i++)
        {
            levelStars[i].text = GameManager.ProgressManager.GetStarsByLevel(i).ToString();
        }
    }

    public void LoadLevel(int level)
    {
        GameManager.Instance.currentLevelIndex = level - 1;
        SceneManager.LoadScene("Level" + level);
    }
}
