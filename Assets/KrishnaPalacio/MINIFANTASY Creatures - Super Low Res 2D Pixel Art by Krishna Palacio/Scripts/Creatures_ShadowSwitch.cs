using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Creatures_ShadowSwitch : MonoBehaviour
{
    public bool isShadowOn;

    private bool currentShadowState;
    private GameObject shadow;

    private void OnValidate() {
        if(shadow == null){
            shadow = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        }
        
        if(ShadowChanged()){
            ChangeShadow();
        }
    }

    bool ShadowChanged(){
        return isShadowOn != currentShadowState;
    }

    void ChangeShadow(){
        currentShadowState = !currentShadowState;

        shadow.SetActive(isShadowOn);
    }
}
