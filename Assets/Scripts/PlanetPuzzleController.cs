using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlanetPuzzleController : MonoBehaviour
{
    public PlanetPuzzleTrigger earthTrigger, mercuryTrigger, moonTrigger, sunTrigger;
    public MoveObjBehavior doorMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Update()
    {
        if(earthTrigger.triggerActive && mercuryTrigger.triggerActive && moonTrigger.triggerActive && sunTrigger.triggerActive)
        {
            doorMove.MoveObj();
        }
    }
}
