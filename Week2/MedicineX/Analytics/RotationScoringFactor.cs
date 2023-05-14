using MAGES.AnalyticsEngine;
using MAGES.sceneGraphSpace;
using System.Linq;
using UnityEngine;

public class RotationScoringFactor : ScoringFactor
{
    private GameObject bottle;
    private float _score = 100;
    
    public override object GetReadableData()
    {
        ScoringFactorData sfData = new ScoringFactorData();
        sfData.score = Mathf.RoundToInt(_score);
        sfData.type = "Rotation Scoring Factor";
        sfData.errorMessage = "Medicine Spilled :(";
        sfData.scoreSpecific = 1;
        return sfData;  
    }

    public override ScoringFactor Initialize(GameObject g)
    {
        bottle = GameObject.Find($"Interactable_Bottle{g.name.Last()}(Clone)");
        return this;
    }

    public override float Perform(bool skipped = false)
    {
        if (skipped)
            return 0;
        
        if (bottle.GetComponent<UpdateRotationScoring>().hasSpilled > 0 &&
            !bottle.GetComponent<UpdateRotationScoring>().liquidExtracted)
            _score = 0;

        return _score;
    }

    public override void Undo()
    {
        _score = 100;
    }
}
