using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    public GameObject mainMenuPanel;

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
