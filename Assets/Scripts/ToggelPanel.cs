using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; //allows me to choose which panel will be toggeled

    public void HidePanel()
    {
        panel.SetActive(false); // turns panel off
    }

    //public void ShowPanel()
    //{
    //    panel.SetActive(true); will show the panel again 
    //}
}