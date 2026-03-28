using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("IsWalking", player.IsWalking());
        animator.SetBool("IsSprinting", player.IsSprinting());
        animator.SetBool("IsJumping", player.IsJumping());
    }

}
