using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public static LoadGame Instance;

    public bool wasLoaded;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance inicializada.");
        }

    }

    void OnDisable()
    {
        Debug.Log("LoadGame desabilitado: " + this.GetInstanceID());
    }

    public void Loading()
    {
        Debug.Log(PlayerPrefs.HasKey("LevelSaved"));
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            wasLoaded = true;
            PlayerPrefs.SetInt("wasLoaded", 1);

            string levelToLoad = PlayerPrefs.GetString("LevelSaved");
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
