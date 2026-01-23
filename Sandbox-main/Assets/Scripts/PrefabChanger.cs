using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabChanger : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new();
    [SerializeField] int currentStage;
    int initialStage;

    private void Start()
    {
        initialStage = currentStage;
        prefabs[initialStage].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Fruit>())
        {
            switch (other.GetComponent<Fruit>().fruitData.fruitType)
            {
                case FruitData.FruitType.Refresh:
                {
                    Reset();
                    break;
                }
                case FruitData.FruitType.Age:
                {
                    Age();
                    break;
                }
                case FruitData.FruitType.Deage:
                {
                    Deage();
                    break;
                }       
            }
            Destroy(other.gameObject);
        }
    }

    void Age()
    {
        if (currentStage < prefabs.Count - 1)
        {
            prefabs[currentStage].SetActive(false);
            currentStage++;
        }
        else
        {
            currentStage = prefabs.Count - 1;
        }
        prefabs[currentStage].SetActive(true);
    }

    void Deage()
    {
        if (currentStage > 0)
        {
            prefabs[currentStage].SetActive(false);
            currentStage--;
        }
        else
        {
            prefabs[currentStage].SetActive(false);
            currentStage = 0;
        }
        prefabs[currentStage].SetActive(true);
    }

    void Reset()
    {
        prefabs[currentStage].SetActive(false);
        prefabs[initialStage].SetActive(true);  
    }
}
