using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevelIndex;
    bool gameEnded = false;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } else
        {
            Destroy(gameObject);
        }
    }

    public class ProgressManager
    {
        private const string progressKey = "LevelProgress";
        private const string starsKey = "StarsByLevel";

        public static void MarkLevelCompleted(int levelIndex)
        {
            int lastLevel = GetMaxUnlockedLevelIndex();
            if (levelIndex + 1 > lastLevel)
            {
                PlayerPrefs.SetInt(progressKey, levelIndex + 1);
                PlayerPrefs.Save();
            }
        }

        public static int GetMaxUnlockedLevelIndex()
        {
            return PlayerPrefs.GetInt(progressKey, 0);
        }

        public static void SaveStarsByLevel(int levelIndex, int starsCount)
        {
            string key = starsKey + levelIndex.ToString();
            int savedStarsCount = PlayerPrefs.GetInt(key, 0);
            if (savedStarsCount < starsCount)
            {
                PlayerPrefs.SetInt(key, starsCount);
            }
        }

        public static int GetStarsByLevel(int levelIndex)
        {
            string key = starsKey + levelIndex;
            return PlayerPrefs.GetInt(key, 0);

        }
    }

    public void OnLevelComplete()
    {
        Time.timeScale = 0f;

        ProgressManager.MarkLevelCompleted(currentLevelIndex);
        ProgressManager.SaveStarsByLevel(currentLevelIndex, LevelManager.Instance.collectedStars);
    }

    public void Lose()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("YOU LOSE!");
        SceneManager.LoadScene("Main menu");
    }
}


