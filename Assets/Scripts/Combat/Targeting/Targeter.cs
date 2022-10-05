using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    private Camera MainCamera;
    public List<Target> targets = new List<Target>();
  [field: SerializeField]  public Target currentTarget{ get; private set; }

    private void Start()
    {
        MainCamera = Camera.main;
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

        currentTarget = closestTarget;
        cineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null) { return; }
        cineTargetGroup.RemoveMember(currentTarget.transform);
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
        
        if(targets.Count!=0)
        {
            currentTarget = targets[0];
        }
    }
}

