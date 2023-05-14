using MAGES.ActionPrototypes;
using MAGES.sceneGraphSpace;
using MAGES.UIManagement;
using MAGES.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuestionAction : QuestionAction
{

    private int numberOfBottlesToSpawn = 3;
    public override void Initialize()
    {
        SetQuestionPrefab("Lesson0/Stage0/Action0/QuestionPrefab");
        InterfaceManagement.Get.InterfaceRaycastActivation(true);
        
        base.Initialize();
    }

    public override void Perform()
    {
        InterfaceManagement.Get.InterfaceRaycastActivation(false);
        for (int i = 0; i < numberOfBottlesToSpawn; i++)
        {
            GameObject bottle = PrefabImporter.SpawnActionPrefab("Lesson0/Stage0/Action1/Interactable_Bottle");
            bottle.transform.name = $"Interactable_Bottle{i+1}(Clone)";
            bottle.transform.localPosition += new Vector3(0, 0, -0.2f * i);
        }

        base.Perform();
    }
}
