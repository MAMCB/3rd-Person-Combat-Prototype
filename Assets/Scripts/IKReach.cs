using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKReach : MonoBehaviour
{
    
    [SerializeField] AvatarIKGoal goal1 = AvatarIKGoal.RightFoot;
    [SerializeField] AvatarIKGoal goal2 = AvatarIKGoal.LeftFoot;

    [Range(0, 1)]
    [SerializeField] float weight = 0.5f;

    [Range(0,1)]
    public float DistanceToGround;
    public LayerMask layerMask;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

   

    private void OnAnimatorIK(int layerIndex)
    {
        
        animator.SetIKRotationWeight(goal1, weight);
        animator.SetIKRotationWeight(goal2, weight);
        animator.SetIKPositionWeight(goal1, weight);
        animator.SetIKPositionWeight(goal2, weight);

        RaycastHit hit;
        Ray ray = new Ray(animator.GetIKPosition(goal1) + Vector3.up,Vector3.down);
        if(Physics.Raycast(ray, out hit,DistanceToGround+1f,layerMask))
        {
            if(hit.transform.tag=="Walkable")
            {
                Vector3 footPosition = hit.point;
                footPosition.y += DistanceToGround;
                animator.SetIKPosition(goal1, footPosition);
            }
        }

    }
}
