using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class PrefabChanger : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new();
    [SerializeField] int currentStage;
    int initialStage;

    private void Start()
    {
        initialStage = Random.Range(0,prefabs.Count);
        currentStage = initialStage;
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
        else if(other.tag == "Potion")
        {
            if (other.GetComponent<Potion>().PotionData.potionType == PotionData.PotionType.Scale)
            {
                StartCoroutine(Expand());
            }
            else if (other.GetComponent<Potion>().PotionData.potionType == PotionData.PotionType.Rotate)
            {
                StartCoroutine(Rotate());
            }
            else if (other.GetComponent<Potion>().PotionData.potionType == PotionData.PotionType.Explode)
            {
                StartCoroutine(Explode());
            }
            Destroy(other);
            
        }
    }

    void Age()
    {
        prefabs[currentStage].SetActive(false);
        if (currentStage < prefabs.Count-1)
        {
            
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
        prefabs[currentStage].SetActive(false);
        if (currentStage >0)
        {
            
            currentStage--;
        }
        else
        {
            
            currentStage = 0;
        }
        prefabs[currentStage].SetActive(true);
    }

    void Reset()
    {
        prefabs[currentStage].SetActive(false);
        prefabs[initialStage].SetActive(true);  
    }
    
    IEnumerator Expand()
    {
        Debug.Log("incredigassy");
        for (int i = 0; i < 50; i++)
        {
            transform.localScale +=
              new Vector3(0.01f, 0.01f, 0.01f);
            yield return null;
        }
    }
    
    IEnumerator Rotate()
    {
        Debug.Log("Rotate");
        for (int i = 0; i < 1000; i++)
        {
            transform.Rotate(
              (Vector3.up * Random.Range(0f, 11f)));
            yield return null;
        }
    }
   
    IEnumerator Explode()
    {
        Debug.Log("EXPLODDE");
        for (int i = 0; i < 1000; i++)
        {
            transform.GetComponent<Rigidbody>().AddExplosionForce(20, transform.position, 10);
            yield return null;
        }
    }
}
