using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Propriety : MonoBehaviour {

    
    public int m_ButtonNumber;
    
    public bool m_ButtonColor;
    
    public int m_NumberOfTeam;
    
    public string m_ButtonInput;
    [HideInInspector]
    public bool m_IsPressed;

	void Update () {
		
        if(Input.GetKeyDown(m_ButtonInput))
        {
            m_IsPressed = true;
        }
        if (Input.GetKeyUp(m_ButtonInput))
        {
            m_IsPressed = false;
        }

	}
}
