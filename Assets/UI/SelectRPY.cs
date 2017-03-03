using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectRPY : MonoBehaviour
{
    public UILabel filename;
    public void enterGame()
    {
        NetCom.fileName = filename.text;
        if (File.Exists(filename.text))
        {
            Debug.Log("RPY file:" + filename.text);
            SceneManager.LoadScene("GameScene");
            Debug.Log("GameScene loaded");
        }
        else
        {
            Debug.Log("RPY file dose not exist");
        }
	}
}
