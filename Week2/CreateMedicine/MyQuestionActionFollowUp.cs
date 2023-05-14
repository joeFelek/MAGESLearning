using MAGES.ActionPrototypes;
using UnityEngine;

public class MyQuestionActionFollowUp : QuestionAction
{
    public override void Initialize()
    {
        SetQuestionPrefab("Lesson0/Stage0/Action0/QuestionPrefab");
        SetAfterSpawnAction(() => GameObject.Find("QuestionPrefab(Clone)").SetActive(false));
        base.Initialize();
    }

}
