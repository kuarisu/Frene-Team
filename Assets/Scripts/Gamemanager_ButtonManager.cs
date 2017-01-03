using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager_ButtonManager : MonoBehaviour {

    [SerializeField]
    List<GameObject> m_GlobalListOfButton = new List<GameObject>();


    public List<GameObject> m_TeamOne = new List<GameObject>();


    public List<GameObject> m_TeamTwo = new List<GameObject>();

    public static Gamemanager_ButtonManager Instance;
    private void Awake()
    {
        if (Gamemanager_ButtonManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Gamemanager_ButtonManager.Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        foreach (GameObject m_Button in m_GlobalListOfButton)
        {
            int _i = 0;
            int _y = 0;

            if (m_Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 1)
            {
                m_TeamOne.Add(m_Button);
            }
            if (m_Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 2)
            {
                m_TeamTwo.Add(m_Button);
            }
        }
	}

}
