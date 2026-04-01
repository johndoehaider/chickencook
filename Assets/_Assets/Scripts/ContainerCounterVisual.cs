using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    
    // this script will handle the animation of the container counter
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void OnDisable()
    {
        containerCounter.OnPlayerGrabbedObject -= ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }

}
