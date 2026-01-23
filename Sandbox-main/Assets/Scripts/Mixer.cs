using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

// The TriggerListener class handles the invocation of UnityEvents when a collider
// enters or exits a trigger collider attached to the GameObject this script is assigned to.
//
// modification of the work of @author J.C. Wichman

public class Mixer : MonoBehaviour
{
    /* 
    //Some more advanced features you can enable if you need a layer or tag filter.
        
    [Space(10)]
    // Defines which layers the trigger should respond to. By default, it responds to all layers.
    // (~ is the bitwise complement operator resulting in the highest integer where all bits are 1).
    public LayerMask layerMask = ~0;
    public string tagFilter = null;

    

    [Space(10)]
    */
    public string bottleTag;
    public string combination;
    public List<string> ingredients;

    [SerializeField] List<GameObject> potions = new List<GameObject>();
    [SerializeField] List<Color> LiquidColours = new List<Color>();
    [SerializeField] int PotionID;
    public Material LiquidMaterial;
    
    [SerializeField] Transform potionSpawnTransform;
    [SerializeField] ParticleSystem potionSpawnParticles;
    [SerializeField] List<ParticleSystem> fruitUsedParticles = new List<ParticleSystem>();

    [SerializeField] ParticleSystem explosionParticles;
    
    
    // UnityEvent that is invoked when another collider enters the trigger.
    public UnityEvent onTriggerEnterEvent;

    // UnityEvent that is invoked when another collider stays on the trigger.
    public UnityEvent onTriggerStayEvent;

    // UnityEvent that is invoked when another collider exits the trigger.
    public UnityEvent onTriggerExitEvent;



    // This method is called when another collider enters the trigger collider
    // attached to the GameObject to which this script is attached.
    // 'other' represents the Collider that enters the trigger.
    private void Start()
    {
       
    }
    void OnTriggerEnter(Collider other)
    {
        // Checks if the script is enabled to perform detection, otherwise exit
        if (!enabled) return;

        onTriggerEnterEvent?.Invoke();
        
        int randomEffect = UnityEngine.Random.Range(0, 2);
        fruitUsedParticles[randomEffect].Play();
        Vector3 spawnImpulse = new Vector3(UnityEngine.Random.Range(0.2f, 0.5f), UnityEngine.Random.Range(4.5f, 7.5f), UnityEngine.Random.Range(0.2f, 0.5f));


        //Some more advanced features you can enable if you need a layer or tag filter.
        //if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
        //if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // Invoke the onTriggerEnterEvent when a collider enters the trigger.
        if (other.tag == bottleTag)
        {
            
            { 
                
                potionSpawnParticles.Play();
                GameObject potion = Instantiate(potions[PotionID], potionSpawnTransform.position, Quaternion.identity);
                potion.GetComponent<Rigidbody>().AddForce(spawnImpulse, ForceMode.Impulse);
                //Resets the combination when you create a potion
                combination = "";
                Destroy(other.gameObject);
            }

        }

        else if (ingredients.Contains(other.tag) && !combination.Contains(other.tag))
        {
            combination += other.tag;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            char[] charX = combination.ToCharArray();

            Array.Sort(charX);
            combination = new string(charX).ToLower();
        }
        if (combination != "" && ingredients.Contains(other.tag))
        {
            
            switch (combination)
            {
                case ("ab"):
                    Debug.Log("dis is AB");
                    PotionID = 1;


                    break;
                case ("bc"):
                    Debug.Log("Dis is BC");
                    PotionID =2;


                    break;
                case ("ac"):
                    Debug.Log("Dis is AC");
                    PotionID = 3;


                    break;
                case ("abc"):
                    Debug.Log("Explode");
                    PotionID = 0;
                    combination = "";
                    explosionParticles.Play();
                    break;
            }
            LiquidMaterial.color = LiquidColours[PotionID];

            //Disables the collider so it doesnt register it more than once if it bounces out then in again
            //Destroys it 5 seconds being in the couldron

            Destroy(other.gameObject);
        }
        

        


    }

    // This method is called when another collider stays on the trigger collider
    // attached to the GameObject to which this script is attached.
    // 'other' represents the Collider that stays on the trigger.
    void OnTriggerStay(Collider other)
    {
        // Checks if the script is enabled to perform detection, otherwise exit
        if (!enabled) return;

        //Some more advanced features you can enable if you need a layer or tag filter.
        //if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
        //if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // Invoke the onTriggerEnterEvent when a collider enters the trigger.
        onTriggerStayEvent?.Invoke();
    }

    // This method is called when another collider exits the trigger collider
    // attached to the GameObject to which this script is attached.
    // 'other' represents the Collider that exits the trigger.
    void OnTriggerExit(Collider other)
    {
        // Checks if the script is enabled to perform detection, otherwise exit
        if (!enabled) return;

        //Some more advanced features you can enable if you need a layer or tag filter.
        //if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
        //if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // Invoke the onTriggerExitEvent when a collider exits the trigger.
        onTriggerExitEvent?.Invoke();
    }

}
