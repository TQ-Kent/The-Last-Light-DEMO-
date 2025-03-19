using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
