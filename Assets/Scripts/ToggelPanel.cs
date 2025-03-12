using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // Assign your panel in the Inspector

    public void HidePanel()
    {
        panel.SetActive(false); // Hides the panel
    }

    //public void ShowPanel()
    //{
    //    panel.SetActive(true); will show the panel again 
    //}
}