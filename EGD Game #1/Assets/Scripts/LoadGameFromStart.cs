using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameFromStart : MonoBehaviour
{
    public KeyCode startKey;
    public string gameSceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(startKey))
            SceneManager.LoadScene(gameSceneName);
    }
}
