using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    public Herb Herb => herb;

    private Image image;
    private Herb herb;

    private void Awake()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetHerb(Herb herb)
    {
        this.herb = herb;
        image.sprite = herb.sprite;
        gameObject.SetActive(true);
    }

    public void DeleteHerb()
    {
        this.herb = null;
        image.sprite = null;
        gameObject.SetActive(false);
    }
}
