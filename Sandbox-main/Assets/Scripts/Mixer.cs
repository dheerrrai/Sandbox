using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public MeshRenderer MeshRenderer;
    public int currentColour;
    public MatShift Potion;
    public string bottleTag;
    public string wandTag;
    public string ingredientTag;
    public List<string> ingredients;
    public Vector3 Rotation = new Vector3(0,0,10);
    public string combination="";
    // UnityEvent that is invoked when another collider enters the trigger.
    public UnityEvent onTriggerEnterEvent;

    // UnityEvent that is invoked when another collider stays on the trigger.
    public UnityEvent onTriggerStayEvent;

    // UnityEvent that is invoked when another collider exits the trigger.
    public UnityEvent onTriggerExitEvent;


    [SerializeField] Transform potionSpawnTransform;

    // This method is called when another collider enters the trigger collider
    // attached to the GameObject to which this script is attached.
    // 'other' represents the Collider that enters the trigger.
    private void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        // Checks if the script is enabled to perform detection, otherwise exit
        if (!enabled) return;

        //Some more advanced features you can enable if you need a layer or tag filter.
        //if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
        //if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // Invoke the onTriggerEnterEvent when a collider enters the trigger.
        if(other.tag == bottleTag)
        {
            Potion.MatChange(currentColour);
        
        }

        else if (ingredients.Contains(other.tag))
        {
            ingredientTag += other.tag;

            char[] charX = ingredientTag.ToCharArray();

            Array.Sort(charX);
            ingredientTag = new string(charX).ToLower();
        }
        if (ingredientTag != "")
        {
            switch (ingredientTag)
            {
                case ("ab"):
                    Debug.Log("dis is AB");
                    ingredientTag = "";
                    //should change what is spawned
                    Instantiate(Potion, potionSpawnTransform.position, Quaternion.identity);
                    break;
                case ("bc"):
                    Debug.Log("Dis is BC");
                    ingredientTag = "";
                    //should change what is spawned
                    Instantiate(Potion, potionSpawnTransform.position, Quaternion.identity);
                    break;
                case ("ac"):
                    Debug.Log("Dis is AC");
                    ingredientTag = "";
                    //should change what is spawned
                    Instantiate(Potion, potionSpawnTransform.position, Quaternion.identity);
                    break;
            }

            //Disables the collider so it doesnt register it more than once if it bounces out then in again
            //Destroys it 5 seconds being in the couldron
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(other.gameObject, 3f);
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
