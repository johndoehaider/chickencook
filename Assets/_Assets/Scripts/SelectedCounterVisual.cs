using Unity.VisualScripting;
using UnityEngine;

// Shows a visual indicator on the counter that is currently selected by the player

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectionVisual;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            selectionVisual.SetActive(true);
        }
        else
        {
            selectionVisual.SetActive(false);
        }

    }

}
