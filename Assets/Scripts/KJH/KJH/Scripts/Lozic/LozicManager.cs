using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LozicManager : MonoBehaviour
{
    [SerializeField]
    GearLozic gear;
    [SerializeField]
    ColorButtonLozic colorButton;
    [SerializeField]
    SteamLozic steam;
    [SerializeField]
    SundialLozic sundial;
    [SerializeField]
    LeverLozic lever;

    public bool[] solve_Lozic;
    
    // Update is called once per frame
    void Update()
    {
        solve_Lozic[0] = gear.lozicClear;
        solve_Lozic[1] = colorButton.lozicClear;
        solve_Lozic[2] = steam.lozicClear;
        solve_Lozic[3] = sundial.lozicClear;
        solve_Lozic[4] = lever.lozicClear;
    }
}
