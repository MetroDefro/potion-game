using UnityEngine;
using UnityEngine.UI;
using System;

public class IngredientButton : MonoBehaviour
{
    [SerializeField] private Herb herb;

    private Button button;
    private Image image;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }
    public void Initialize(Action<Herb> OnClickButton)
    {
        button.onClick.AddListener(() => OnClickButton(herb));
    }

    public void SetEnable(bool isEnable)
    {
        image.enabled = isEnable;
    }
}
