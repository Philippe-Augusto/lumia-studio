using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator playerAnimator;

    public void PlayAnimation (string animationName) {
        playerAnimator.Play(animationName);
    }
}
