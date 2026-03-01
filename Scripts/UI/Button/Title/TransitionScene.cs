using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ÉVÅ[ÉìÇÃëJà⁄
/// </summary>
public class TransitionScene : MonoBehaviour
{
    [SerializeField] private string nowStage = "Stage";
    private const string STR_TITLE = "Title";
    public void ToSelectStage(string _toStage)
    {
        SceneManager.LoadScene(_toStage);
    }
    public void ToRetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToNextStage()
    {
        SceneManager.LoadScene(nowStage);
    }

    public void ToTitle()
    {
        Debug.Log("Title");
        SceneManager.LoadScene(STR_TITLE);
    }
}
