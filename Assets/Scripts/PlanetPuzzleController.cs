using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlanetPuzzleController : MonoBehaviour
{
    public PlanetPuzzleTrigger earthTrigger, mercuryTrigger, moonTrigger, sunTrigger;
    public MoveObjBehavior doorMove;

    private bool hasActivated = false;   

    void Update()
    {
        if (!hasActivated &&
            earthTrigger.triggerActive &&
            mercuryTrigger.triggerActive &&
            moonTrigger.triggerActive &&
            sunTrigger.triggerActive)
        {
            hasActivated = true;          
            doorMove.MoveObj();           
        }
    }
}
