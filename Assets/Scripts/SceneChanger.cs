using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
