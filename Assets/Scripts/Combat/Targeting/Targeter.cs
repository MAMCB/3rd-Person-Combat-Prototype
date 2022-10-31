using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    private Camera MainCamera;
    public List<Target> targets = new List<Target>();
    int targetIndex = 0;
    public bool inTargetingState = false;
    [field: SerializeField]  public Target currentTarget{ get; private set; }

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
    {
        if(targets.Count != 0 &&inTargetingState)
        {
           foreach(Target target in targets)
            {
                if(target.name == currentTarget.name)
                {
                    target.ActivateLockOn();
                    Debug.Log(target.name + "is locked");
                }
                else
                {
                    target.DeactivateLockOn();  
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target target)) { return; }
        
            targets.Add(target);
        target.OnDestroyed += RemoveTarget;
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
        
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach(Target target in targets)
        {
            Vector2 viewPos = MainCamera.WorldToViewportPoint(target.transform.position);
            if(!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);//0.5f represents the center of the screen (maximum of 1)
            if(toCenter.sqrMagnitude<closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if(closestTarget==null) { return false; }

        currentTarget= closestTarget;
        
        cineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null) { return; }
        cineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget.DeactivateLockOn();
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        
        if(currentTarget==target)
        {
            cineTargetGroup.RemoveMember(currentTarget.transform);
            
            currentTarget = null;
            
        }

        target.OnDestroyed -= RemoveTarget;
       
        targets.Remove(target);
        
        if(targets.Count!=0 && inTargetingState)
        {
            currentTarget = targets[0];
        }
    }

    public void NextTarget()
    {
        
        targetIndex++;
        if(targetIndex>=targets.Count-1)
        {
            targetIndex = 0;
        }
        currentTarget= targets[targetIndex];
        
       
        
    }

    public void PreviousTarget()
    {
        
        targetIndex--;
        if (targetIndex <= 0)
            {
                targetIndex = targets.Count - 1;
            }
        currentTarget = targets[targetIndex];
        


    }

   
}

