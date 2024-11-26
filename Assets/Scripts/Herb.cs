using UnityEngine;

[CreateAssetMenu(fileName = "Herb", menuName = "Scriptable Object Asset/Herb")]
public class Herb : ScriptableObject
{
    public string Name;
    public string Description;

    public int Index;

    public Sprite sprite;
}
