using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour{
    new AudioListener audio;

    void Start()
    {
        foreach (var item in SceneManager.GetSceneByName("GameScene").GetRootGameObjects())
        {
            if (item.name == "Main Camera")
            {
                audio = item.GetComponent<AudioListener>();
                break;
            }
        }
    }

    public void Check()
    {
        if (UIToggle.current.value)
        {
            audio.enabled = false;
        }
        else
        {
            audio.enabled = true;
        }

    }


}
