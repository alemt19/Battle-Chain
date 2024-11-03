using UnityEngine;

public class OvniController : MonoBehaviour
{

    
    
    
    public OvniMover ovniMover;
    public AimTurret aimTurret;
    public Turret[] turrets;

    private void Awake()
    {
        
        
        ovniMover = GetComponentInChildren<OvniMover>();
        aimTurret = GetComponentInChildren<AimTurret>();
        turrets = GetComponentsInChildren<Turret>();
        
    }    

    public void newAwake()
    {
        turrets = GetComponentsInChildren<Turret>();
    }

    public void HandleShoot()
    {
        foreach (var turret in turrets)
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        ovniMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);
        
    }

    
}
