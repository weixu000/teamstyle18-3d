using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRPYScene : MonoBehaviour
{
    public void loadAIScene()
    {
        SceneManager.LoadScene("RPYScene");
        Debug.Log("Loaded RPYScene");
    }
}