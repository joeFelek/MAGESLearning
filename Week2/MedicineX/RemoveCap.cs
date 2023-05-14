using UnityEngine;
using MAGES.ActionPrototypes;
using MAGES.Utilities;
using MAGES.Utilities.prefabSpawnManager;
using System.Linq;
using UnityEditor.Search;
using Unity.VisualScripting;
using System;
using System.Diagnostics;
using MAGES.sceneGraphSpace;
using MAGES.AnalyticsEngine;

public class RemoveCap : CombinedAction
{
    private GameObject bottle;
    private char _index;

    public override void Initialize()
    {
        _index = gameObject.transform.name.Last();
        bottle = GameObject.Find($"Interactable_Bottle{_index}(Clone)");
        AnalyticsManager.AddScoringFactor<RotationScoringFactor>(gameObject);

        RemoveAction removeCap = gameObject.AddComponent<RemoveAction>();
        {
            removeCap.SetRemovePrefab("Lesson0/Stage0/Action1/Interactable_BottleCap", bottle);
            removeCap.SetHoloObject("Lesson0/Stage0/Action1/Hologram_BottleCap", bottle);
            removeCap.SetAfterSpawnAction(HandleRemoveCapSpawn);
            removeCap.SetPerformAction(HandleRemoveCapPerform);
        }

        InsertAction attachSyringe = gameObject.AddComponent<InsertAction>();
        {
            attachSyringe.SetInsertPrefab($"Medicine{_index}/Syringe/Interactable_Syringe{_index}", $"Medicine{_index}/Syringe/Final_Syringe{_index}", bottle);
            attachSyringe.SetHoloObject($"Medicine{_index}/Syringe/Holo_Syringe{_index}", bottle);
        }

        PumpAction pumpSyringe = gameObject.AddComponent<PumpAction>();
        {
            pumpSyringe.SetPumpPrefab($"Medicine{_index}/SyringePump/SyringePump{_index}", bottle);
            pumpSyringe.SetPerformAction(HandlePumpSyringePerform);
        }

        InsertIActions(removeCap, attachSyringe, pumpSyringe);
        
        base.Initialize();
    }

    private void HandleRemoveCapSpawn()
    {
        bottle.transform.GetChild(4).name = $"Interactable_BottleCap{_index}(Clone)";
        PrefabSpawnManager.AddNewSpawnedPrefabToManager(bottle.transform.GetChild(4).gameObject);
    }
    
    private void HandleRemoveCapPerform()
    { 
        bottle.transform.GetChild(4).name = "Interactable_BottleCap(Clone)";
        bottle.AddComponent<UpdateRotationScoring>();
    }

    private void HandlePumpSyringePerform()
    {
        UpdateRotationScoring flag = bottle.GetComponent<UpdateRotationScoring>();
        if (flag == null || flag.hasSpilled > 0) return;
        
        GameObject.Find("UserHands").transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        bottle.GetComponent<UpdateRotationScoring>().liquidExtracted = true;
        bottle.GetComponent<Rigidbody>().isKinematic = true;

        GameObject syringe = Spawn($"Medicine{_index}/SyringeFull/Interactable_SyringeFull{_index}", bottle);
        syringe.transform.parent = null;
    }

    public override void Undo()
    {
        if (bottle)
            Destroy(bottle);
        GameObject.Find("UserHands").transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        base.Undo();
    }

}