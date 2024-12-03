using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadEasy()
    {
        SceneManager.LoadScene("Easy_Scene");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void loadMedium()
    {
        SceneManager.LoadScene("Medium_Scene");
    }

    public void loadHard()
    {
        SceneManager.LoadScene("Hard_Scene");
    }
}
