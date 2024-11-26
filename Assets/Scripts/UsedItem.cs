using System;
using UnityEngine;
using UnityEngine.UI;

public class UsedItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public UsedItem Initialize(Sprite sprite, Action onClick)
    {
        image.sprite = sprite;
        button.onClick.AddListener(() => onClick());

        return this;
    }
}
