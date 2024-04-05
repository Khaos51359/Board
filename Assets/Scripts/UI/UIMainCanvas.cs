using UnityEngine;

public class UIMainCanvas : MonoBehaviour
{
    public void Start()
    {
        UIStartButton.OnClicked += OnStartButtonClicked;
    }

    private void OnStartButtonClicked()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        UIStartButton.OnClicked -= OnStartButtonClicked;
    }
}
