using System;
using Unity.VisualScripting;
using UnityEngine;

// Represents a counter in the game world that the player can interact with

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    // Reference to the second counter that this counter can interact with (for example, to pass a KitchenObject to)
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    // Reference to the KitchenObject currently on this counter (if any)
    private KitchenObject kitchenObject;

    public void Update()
    {
        if (testing)
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }


    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }

        else
        {
            // give object to player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetJKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
