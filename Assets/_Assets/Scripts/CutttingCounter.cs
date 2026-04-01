using Unity.VisualScripting;
using UnityEngine;

public class CutttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no kitchen object here
            if (player.HasKitchenObject())
            {
                // player is carrying something, give it to the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } 
            else
            {
                // player is not carrying anything, do nothing
            }

        }
        else
        {
            // there is a kitchen object here
            if (player.HasKitchenObject())
            {
                // both the counter and the player have a kitchen object
                // handle interaction between the two kitchen objects here
            }
            else
            {
                // player is not carrying anything, pick up the kitchen object from the counter
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // handle cutting the kitchen object here
            GetKitchenObject().DestroySelf();
            
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }


    }
 

}
