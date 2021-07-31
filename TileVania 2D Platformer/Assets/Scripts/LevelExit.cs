using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float TimeToNextScene = 3f;
    [SerializeField] float SlowMo = 0.15f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(FinsihedLevel());
    }

    IEnumerator FinsihedLevel()
    {
        Time.timeScale = SlowMo;
        yield return new WaitForSecondsRealtime(TimeToNextScene);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
