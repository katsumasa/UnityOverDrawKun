// (C) UTJ
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utj.OverdrawKun;


[CustomEditor(typeof(OverdrawKun))]
public class OverdrawKunEditor :  Editor{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OverdrawKun overdrawKun = target as OverdrawKun;

        if (overdrawKun.State == OverdrawKun.STATE.IDLE)
        {
            if (GUILayout.Button("Record"))
            {
                overdrawKun.BeginProfile();
            }
        }
        else
        {
            if (GUILayout.Button("Stop"))
            {
                overdrawKun.EndProfile();
            }
            GUILayout.TextField("RecordNo:" + overdrawKun.RecordNum.ToString());
        }
    }
}
#endif