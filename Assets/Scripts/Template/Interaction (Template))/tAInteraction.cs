using System;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// When creating gameobjects for this script, the script and progressBarObj should be separated
public abstract class tAInteraction : MonoBehaviour
{
    // Delegate
    public UnityEvent D_PreEvents = null;
    public UnityEvent D_PostEvents = null;

    //Interact
    [HideInInspector] public bool bCanInteract = false;
    [HideInInspector] public bool bIsActive = true;
    [HideInInspector] public float fTimePress;
    protected KeyCode _keyToPress = KeyCode.E;
    protected string sTagToCompare = "Player";

    [Space]
    [Header("Interactables")]
    public GameObject _progressBarObj; // the parent object in which the progressbarImg is set
    public Image _progressBarImg; // image should be in fill mode
    
    void Awake()
    {
        OCreateDelegates();
        OAwake();
    }

    void OnDestroy()
    {
        ODeleteDelegates();
    }

    void Start()
    {
        if (D_PreEvents == null)
            D_PreEvents = new UnityEvent();
        if (D_PostEvents == null)
            D_PostEvents = new UnityEvent();
        OStart();
    }

    public void Update()
    {
        InheritorsUpdate();
    }
    
    // Default Update content
    public virtual void InheritorsUpdate()
    {
        // if item is interactable and it's active(existing)
        if (bCanInteract && bIsActive)
        {
            // show the progress bar together with the Image
            _progressBarObj.SetActive(true);
            // condition before interacting with the obj
            if(OBeforeInteraction())
            {
                // when input key is release
                if (Input.GetKeyUp(_keyToPress))
                {
                    //TODO: Call animations here!!
                    
                    // call the item's events
                    CallItemEvents(true);

                    // reset progression
                    ResetProgressBar();
                }
                // input key to press
                else if (Input.GetKey(_keyToPress))
                {
                    //TODO: Call animations here!!

                    // increment visual progressbarImg
                    fTimePress += Time.deltaTime;
                    if (_progressBarImg != null)
                    {
                        _progressBarImg.fillAmount = fTimePress / 2.0f;
                    }

                    // If interaction progress gets completed
                    if (OFillCompletion())
                    {
                        //TODO: Call animations here!!

                        // call the item's events
                        CallItemEvents(false);

                        // reset progression
                        ResetProgressBar();
                    }
                }
            }
        }
    }
    
    protected void ResetProgressBar()
    {
        fTimePress = 0;
        if (_progressBarImg != null)
        {
            _progressBarImg.fillAmount = 0.0f;
        }
    }
    
    protected void DeActivate()
    {
        bIsActive = false;
        _progressBarObj.SetActive(false);

    }

    // Add here the delegate to be called for a specific puzzle
    protected void CallItemEvents(bool bIsPreEvent)
    {
        // call the delegate of this clue
        if (bIsPreEvent && D_PreEvents != null)
        {
            D_PreEvents.Invoke();
        }
        else if (bIsPreEvent && D_PostEvents != null)
        {
            D_PostEvents.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OOnTriggerExit(other);
    }

    /* ABSTRACT METHODS */
    
    // overridable function for Awake method; Default
    public abstract void OAwake();

    // overridable function for Start method; Default
    public abstract void OStart();
    
    // Inherited class should override this method if they want to add events to the item interaction; Default
    public abstract void OCreateDelegates();
    public abstract void ODeleteDelegates();

    
    /* VIRTUAL METHODS */

    // this is the default condition for interaction; Default
    public virtual bool OBeforeInteraction()
    {
        return true;
    }

    // this is the default condition for radial-fill completion; Default
    public virtual bool OFillCompletion()
    {
        if(_progressBarImg != null && _progressBarImg.fillAmount >= 1.0f)
            return true;
        return false;
    }
    
    // this is the default condition for OnTriggerEnter; Default
    public virtual void OOnTriggerEnter(Collider other)
    {
        if (other.CompareTag(sTagToCompare) && bIsActive)
        {
            this.bCanInteract = true;
        }
    }

    // this is the default condition for OnTriggerExit; Default
    public virtual void OOnTriggerExit(Collider other)
    {
        if (other.CompareTag(sTagToCompare) && bIsActive)
        {
            this.bCanInteract = false;
            // reset progression
            ResetProgressBar();
            // Deactivate
            DeActivate();
        }
    }

    /*
    // Load system
    public void LoadData(GameData data)
    {
        if (data.objectivesDone.ContainsKey((int) Item_Identification))
        {
            data.objectivesDone.TryGetValue((int)Item_Identification, out bIsActive);
            if (!bIsActive)
            {
                OLoadData(data);
            }
        }
    }
    
    // Save system
    public void SaveData(GameData data)
    {
        OSaveData(data);
    }
    
    // overridable function for load method
    public virtual void OLoadData(GameData data)
    {
        if (!bIsActive)
        {
            CallItemEvents(Item_Identification);
        }
    }
    
    // overridable function for save method
    public virtual void OSaveData(GameData data)
    {
        if (data.objectivesDone.ContainsKey((int)Item_Identification))
        {
            data.objectivesDone.Remove((int)Item_Identification);
        }
        data.objectivesDone.Add((int)Item_Identification, bIsActive);
    }
    */
}
