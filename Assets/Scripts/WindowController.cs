using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public GameObject optionPanel;

    void Start()
    {
        optionPanel.SetActive(false); // ensures that the option panel is hidden at start 
    }

    public void ToggleOptionPanel()
    {
        optionPanel.SetActive(!optionPanel.activeSelf); // show/hide the panel
    }
}


