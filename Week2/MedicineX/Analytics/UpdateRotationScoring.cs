using UnityEngine;
using System.Linq;
using MAGES.ActionPrototypes;
using MAGES.UIManagement;
using MAGES.sceneGraphSpace;

public class UpdateRotationScoring : MonoBehaviour
{ 
    public int hasSpilled = 0;
    public bool liquidExtracted = false;
    GameObject currAction;

    void Start()
    {
        currAction = GameObject.Find($"Bottle{gameObject.name[..20].Last()}");
    }

    void FixedUpdate()
    {
        float x = transform.eulerAngles.x;
        float z = transform.eulerAngles.z;
        if (!((-5 <= x && x <= 80) || (280 <= x && x <= 360)) ||
            !((-5 <= z && z <= 80) || (280 <= z && z <= 360)))
        {
            HandleSpilled();   
        }
    }

    private void HandleSpilled()
    {
        hasSpilled++;
        if (hasSpilled > 1) hasSpilled = 2;
        if (hasSpilled != 1 || liquidExtracted) return;

        Operation.Get.Perform(currAction);
        InterfaceManagement.Get.SpawnDynamicNotificationUI(NotificationUITypes.UIError, "Medicine Spilled :(",4f);
        GameObject.Find("UserHands").transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 
    }

}
