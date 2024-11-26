using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Object Asset/Potion")]
public class Potion : ScriptableObject
{
    public string Name;
    public string Description;
    public string HiddenName;
    public string HiddenDescription;

    public int[] HerbIndexes;
    public int[] HiddenHerbIndexes;

    public Sprite Sprite;
    public Sprite HiddenSprite;

    public int Index;
}
