using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject DifficultyToggle;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyToggle.transform.GetChild((int)DifficultyValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }


    #region Difficulty
    public void SetNormalDifficulty(bool isOn)
    {
        Debug.Log("Normal");
        if (isOn)
            DifficultyValues.Difficulty = DifficultyValues.Difficulties.Normal;
    }

    public void SetHardDifficulty(bool isOn)
    {
        Debug.Log("Hard");
        if (isOn)
            DifficultyValues.Difficulty = DifficultyValues.Difficulties.Hard;
    }
    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }
}
