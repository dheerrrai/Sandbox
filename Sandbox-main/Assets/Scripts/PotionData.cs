using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Data/Potion Data")]
public class PotionData : ScriptableObject
{
    public PotionType potionType;

    public enum PotionType { Scale, Rotate, Explode }
}
