using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Data/Fruit Data")]
public class FruitData : ScriptableObject
{

    public FruitType fruitType;
    public enum FruitType { Refresh, Age, Deage}
}
