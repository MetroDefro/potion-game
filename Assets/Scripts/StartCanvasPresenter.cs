using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartCanvasPresenter : MonoBehaviour
{
    StartCanvasView view;

    private void Awake()
    {
        view = GetComponent<StartCanvasView>();
    }

    public void Start()
    {
        view.StartButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("MainScene");
        });
    }
}
