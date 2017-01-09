using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atelier_Conditions : MonoBehaviour {

    [SerializeField]
    enum Team
    {
        Team_One,
        Team_Two,
        Both,
    }

    [SerializeField]
    Team m_Team;
    
    #region Button Color
   
    [SerializeField]
    enum ColorButton
    {
        None,
        Blue,
        Pink,
    }

    [SerializeField]
    ColorButton m_ColorOfButton;
    #endregion

    #region Button Interaction

    [SerializeField]
    enum InteractionButton
    {
        None,
        Spam,
        PressedOnce,
        PressedTime,
    }

    [SerializeField]
    InteractionButton m_TypeOfInteractionButton;

    [SerializeField]
    int m_TimePressedButton;
    #endregion

    #region Button Number
    [SerializeField]
    enum NumberButton
    {
        None,
        Even,
        Odd,
    }

    [SerializeField]
    NumberButton m_NumberButton;
    #endregion

    #region Button Sequence
    [SerializeField]
    enum Sequence
    {
        None,
        Color,
        Number,
    }

    [SerializeField]
    Sequence m_Sequences;


    [SerializeField]
    List<bool> m_SquenceConditionsColor = new List<bool>();

    [SerializeField]
    List<int> m_SquenceConditionsNb = new List<int>();
    #endregion

    #region Timer

    [SerializeField]
    float m_CurrentTime;
    [SerializeField]
    float m_MaxTime;

    float m_TimeToEnd;
    #endregion

    #region Scoring
    [SerializeField]
    float m_ScoreTeam1;
    [SerializeField]
    float m_ScoreTeam2;


    [SerializeField]
    int m_GainPoints;
    #endregion

    #region Bools
    bool m_UsingColor = false;
    bool m_cPink = false;
    bool m_cBlue = false;

    bool m_UsingNumber = false;
    bool m_nOdd = false;
    bool m_nEven = false;

    bool m_UsingInteraction = false;
    bool m_bPressOne = false;
    bool m_bPressTime = false;
    bool m_bSpam = false;

    bool m_UsingSequence = false;
    bool m_sColor = false;
    bool m_sNumber = false;
    #endregion


    bool m_KeyB = false;
    bool m_KeyC = false;
    bool m_KeyD = false;
    
    bool m_KeyW = false;
    bool m_KeyX = false;
    bool m_KeyY = false;

    int m_iT1 = 0;
    int m_iT2 = 0;

    bool m_Team1 = false;
    bool m_Team2 = false;

    bool m_RightOrder;


    void Start()
    {
        #region bool Color
        switch (m_ColorOfButton)
        {
            case ColorButton.None:
                m_UsingColor = false;
                break;
            case ColorButton.Blue:
                m_UsingColor = true;
                m_cBlue = true;
                break;
            case ColorButton.Pink:
                m_UsingColor = true;
                m_cPink = true;
                break;
            default:
                break;
        }
        #endregion

        #region bool Interaction
        switch (m_TypeOfInteractionButton)
        {
            case InteractionButton.None:
                m_UsingInteraction = false;
                break;
            case InteractionButton.Spam:
                m_UsingInteraction = true;
                m_bSpam = true;
                break;
            case InteractionButton.PressedOnce:
                m_UsingInteraction = true;
                m_bPressOne = true;
                break;
            case InteractionButton.PressedTime:
                m_UsingInteraction = true;
                m_bPressTime = true;
                break;
            default:
                break;
        }
        #endregion

        #region bool Number
        switch (m_NumberButton)
        {
            case NumberButton.None:
                m_UsingNumber = false; 
                break;
            case NumberButton.Even:
                m_UsingNumber = true;
                m_nEven = true;
                break;
            case NumberButton.Odd:
                m_UsingNumber = true;
                m_nOdd = true;
                break;
            default:
                break;
        }
        #endregion

        #region bool Sequence
        switch (m_Sequences)
        {
            case Sequence.None:
                m_UsingSequence = false;
                break;
            case Sequence.Color:
                m_UsingSequence = true;
                m_sColor = true;
                break;
            case Sequence.Number:
                m_UsingSequence = true;
                m_sNumber = true;
                break;
            default:
                break;
        }
        #endregion  

        switch (m_Team)
        {
            case Team.Team_One:
                m_Team1 = true;
                m_Team2 = false;
                break;
            case Team.Team_Two:
                m_Team1 = false;
                m_Team2 = true;
                break;
            case Team.Both:
                m_Team1 = true;
                m_Team2 = true;
                break;
            default:
                break;
        }

        BoolCondition();
        StartCoroutine(CheckEndAtelier());
    }

    void Update()
    {
        if (Input.GetKeyUp("b") && !m_KeyB && m_Team1)
            m_KeyB = true;

        if (Input.GetKeyUp("c") && !m_KeyC && m_Team1)
            m_KeyC = true;

        if (Input.GetKeyUp("d") && !m_KeyD && m_Team1)
            m_KeyD = true;

        if (Input.GetKeyUp("w") && !m_KeyW && m_Team2)
            m_KeyW = true;

        if (Input.GetKeyUp("x") && !m_KeyX && m_Team2)
            m_KeyX = true;

        if (Input.GetKeyUp("y") && !m_KeyY && m_Team2)
            m_KeyY = true;

    }

    void BoolCondition()
    {

        if(m_UsingColor)
        {
            if (m_cPink)
                StartCoroutine(CheckPink());

            if(m_cBlue)
                StartCoroutine(CheckBlue());
        }

        if(m_UsingInteraction)
        {
            if (m_bPressOne)
                StartCoroutine(CheckPressOne());

            if (m_bPressTime)
                StartCoroutine(CheckPressTime());

            if (m_bSpam)
                StartCoroutine(CheckSpam());

        }

        if(m_UsingNumber)
        {
            if(m_nEven)
                StartCoroutine(CheckEven());

            if(m_nOdd)
                StartCoroutine(CheckOdd());


        }

        if(m_UsingSequence)
        {
            if (m_sColor)
                StartCoroutine(CheckSequenceColor());

            if (m_sNumber)
                StartCoroutine(CheckSequenceNumber());
        }
    }

    //Check Color
    IEnumerator CheckPink()
    {
        int _iT1 = 0;
        int _iT2 = 0;

        while (m_CurrentTime < m_MaxTime)
        {
            //if Right Input
            if (m_KeyC)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                m_iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyC = false;

            }
            if (m_KeyY)
            {
                m_ScoreTeam2 += 1 / m_CurrentTime;
                m_iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyY = false;
            }

            //if Wrong Input
            if (m_KeyB || m_KeyD)
            {
                m_ScoreTeam1 -= 1;
                yield return new WaitForEndOfFrame();
                m_KeyB = false;
                m_KeyD = false;
            }
            if (m_KeyW || m_KeyX)
            {
                m_ScoreTeam2 -= 1;
                yield return new WaitForEndOfFrame();
                m_KeyW = false;
                m_KeyX = false;
            }

            //When the game finish
            if (m_iT1 >= 1 || m_iT2 >= 1)
            {
                m_TimeToEnd = m_CurrentTime;
            }

            m_CurrentTime +=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingColor = false;
		yield return null;
    }

    IEnumerator CheckBlue()
    {
        int _iT1 = 0;
        int _iT2 = 0;
        while (m_CurrentTime < m_MaxTime)
        {
            //if Right Input
            if (m_KeyB)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                _iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyB = false;
            }

            if (m_KeyD)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                _iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyD = false;
            }


            if (m_KeyW)
            {
                m_ScoreTeam2 += 1 / m_CurrentTime;
                _iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyW = false;
            }
            if (m_KeyX)
            {
                m_ScoreTeam2 += 1 / m_CurrentTime;
                _iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyX = false;
            }

            // if Wrong Input
            if(m_KeyC)
            {
                m_ScoreTeam1 -= 1;
                yield return new WaitForEndOfFrame();
                m_KeyC = false;
            }
            if (m_KeyY)
            {
                m_ScoreTeam2 -= 1;
                yield return new WaitForEndOfFrame();
                m_KeyY = false;
            }

            //End Atelier
            if (_iT2 >= 2 || _iT1 >= 2)
            {
                m_TimeToEnd = m_CurrentTime;
            }

            m_CurrentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingColor = false;
    }

    //Check Input
    IEnumerator CheckPressOne()
    {
		bool _BPressedOnce = false;
		bool _CPressedOnce = false;
		bool _DPressedOnce = false;
		bool _WPressedOnce = false;
		bool _XPressedOnce = false;
		bool _YPressedOnce = false;



        while (m_CurrentTime < m_MaxTime)
        {
			if (m_KeyB && !_BPressedOnce)
			{
				_BPressedOnce = true;
				m_ScoreTeam1++;
				m_KeyB = false;
			}
			else if (m_KeyB && _BPressedOnce)
			{
				m_ScoreTeam1--;
			}	

			if (m_KeyC && !_CPressedOnce)
			{
				_CPressedOnce = true;
				m_ScoreTeam1++;
				m_KeyC = false;
			}
			else if (m_KeyC && _CPressedOnce)
			{
				m_ScoreTeam1--;
			}	

			if (m_KeyD && !_DPressedOnce)
			{
				_DPressedOnce = true;
				m_ScoreTeam1++;
				m_KeyD = false;
			}
			else if (m_KeyD && _DPressedOnce)
			{
				m_ScoreTeam1--;
			}	


			if (m_KeyW && !_WPressedOnce)
			{
				_WPressedOnce = true;
				m_ScoreTeam2++;
				m_KeyW = false;
			}
			else if (m_KeyW && _WPressedOnce)
			{
				m_ScoreTeam2--;
			}

			if (m_KeyX && !_XPressedOnce)
			{
				_XPressedOnce = true;
				m_ScoreTeam2++;
				m_KeyX = false;
			}
			else if (m_KeyX && _XPressedOnce)
			{
				m_ScoreTeam2--;
			}	

			if (m_KeyY && !_YPressedOnce)
			{
				_YPressedOnce = true;
				m_ScoreTeam2++;
				m_KeyY = false;
			}
			else if (m_KeyY && _YPressedOnce)
			{
				m_ScoreTeam2--;
			}	



			m_CurrentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingInteraction = false;
    }

    IEnumerator CheckPressTime()
    {
        while (m_CurrentTime < m_MaxTime)
        {

            m_CurrentTime +=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingInteraction = false;
    }

    IEnumerator CheckSpam()
    {
        while (m_CurrentTime < m_MaxTime)
        {
            if (m_KeyB || m_KeyC || m_KeyD)
                m_ScoreTeam1++;
            if (m_KeyW || m_KeyX || m_KeyY)
                m_ScoreTeam2++;



            m_CurrentTime +=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingInteraction = false;
    }

    //Check Number
    IEnumerator CheckEven()
    {
        int _iT1 = 0;
        int _iT2 = 0;
        while (m_CurrentTime < m_MaxTime)
        {
            if (m_KeyC)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                m_iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyC = false;
            }
            if (m_KeyB || m_KeyD)
            {
                m_ScoreTeam1--;
                yield return new WaitForEndOfFrame();
                m_KeyB = false;
                m_KeyD = false;
            }

            if (m_KeyW)
            {
                m_ScoreTeam2 += 1 / m_CurrentTime;
                m_iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyW = false;
            }
            if (m_KeyX)
            {
                m_ScoreTeam2 += 1 / m_CurrentTime;
                m_iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyX = false;
            }
            if (m_KeyY)
            {
                m_ScoreTeam2--;
                yield return new WaitForEndOfFrame();
                m_KeyY = false;
            }

            if (_iT1 >= 1)
            {
                m_TimeToEnd = m_CurrentTime;
            }

            if (_iT2 >= 2)
            {
                m_TimeToEnd = m_CurrentTime;
            }
            m_CurrentTime +=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingNumber = false;
    }

    IEnumerator CheckOdd()
    {
        int _iT1 = 0;
        int _iT2 = 0;
        while (m_CurrentTime < m_MaxTime)
        {
            if (m_KeyB)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                m_iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyB = false;
            }

            if (m_KeyD)
            {
                m_ScoreTeam1 += 1 / m_CurrentTime;
                m_iT1++;
                yield return new WaitForEndOfFrame();
                m_KeyD = false;
            }
            if (m_KeyC)
            {
                m_ScoreTeam1--;
                yield return new WaitForEndOfFrame();
                m_KeyC = false;
            }

            if (m_KeyX)
            {
				m_ScoreTeam2--;
                m_iT2++;
                yield return new WaitForEndOfFrame();
                m_KeyX = false;
            }

            if (m_KeyW || m_KeyY)
            {
				m_ScoreTeam2 += 1 / m_CurrentTime;
                yield return new WaitForEndOfFrame();
                m_KeyW = false;
                m_KeyY = false;

            }


            if (_iT1 >= 2)
            {
                m_TimeToEnd = m_CurrentTime;
            }

            if (_iT2 >= 1)
            {
                m_TimeToEnd = m_CurrentTime;
            }


            m_CurrentTime +=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_UsingNumber = false;
    }

    //Check Sequence
    IEnumerator CheckSequenceColor()
    {

        int _i = 0;
		int _it1 = 0;
		int _it2 = 0;

        switch (m_Team)
        {
            case Team.Team_One:
                while (m_CurrentTime < m_MaxTime)
                {
                    foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_TeamOne)
                    {

					if (_i <= m_SquenceConditionsColor.Count-1)
                        {
                            if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor == m_SquenceConditionsColor[_i])
                            {
                                _i++;
                            }
                            else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor != m_SquenceConditionsColor[_i])
                            {
                                _i = 0;
                            }
                        }
					else if (_i == m_SquenceConditionsColor.Count-1)
                        {
						Debug.Log ("hehehehehehehe");
                            m_iT1++;
                            m_RightOrder = false;
							m_ScoreTeam1 ++;
                            m_TimeToEnd = m_CurrentTime;
                            m_MaxTime = m_CurrentTime;
                        }

                    }
                    yield return new WaitForEndOfFrame();
                }
                m_UsingSequence = false;
                break;
            case Team.Team_Two:

                while (m_CurrentTime < m_MaxTime)
                {

                    foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_TeamTwo)
                    {
					if (_i <= m_SquenceConditionsColor.Count-1)
                        {
                            if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor == m_SquenceConditionsColor[_i])
                            {

                                _i++;
                            }
                            else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor != m_SquenceConditionsColor[_i])
                            {
                                _i = 0;
                            }
                        }
					if (_i == m_SquenceConditionsColor.Count-1)
                        {
						Debug.Log ("hehehehehehehe");
                            m_iT2++;
                            m_RightOrder = false;
							m_ScoreTeam2 ++;
                            m_TimeToEnd = m_CurrentTime;
                            m_MaxTime = m_CurrentTime;
                        }

                    }
                    yield return new WaitForEndOfFrame();
                }
                m_UsingSequence = false;
                break;

		case Team.Both:

			while (m_CurrentTime < m_MaxTime)
			{

				foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_GlobalListOfButton)
				{
					if (_it1 <= m_SquenceConditionsColor.Count-1)
					{
						if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor == m_SquenceConditionsColor[_it1] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 1)
						{
							_it1++;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
						else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor != m_SquenceConditionsColor[_it1] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 1)
						{
							_it1 = 0;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
					}

					if (_it2 <= m_SquenceConditionsColor.Count-1)
					{
						if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor == m_SquenceConditionsColor[_it2] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 2)
						{
							_it2++;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
						else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonColor != m_SquenceConditionsColor[_it2] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 2)
						{
							_it2 = 0;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
					}


				}
				if (_it2 == m_SquenceConditionsColor.Count)
				{
					m_RightOrder = false;
					m_ScoreTeam2 ++;
					m_TimeToEnd = m_CurrentTime;
					m_MaxTime = m_CurrentTime;
				}
				if (_it1 == m_SquenceConditionsColor.Count)
				{
					m_RightOrder = false;
					m_ScoreTeam1 ++;
					m_TimeToEnd = m_CurrentTime;
					m_MaxTime = m_CurrentTime;
				}

				yield return new WaitForEndOfFrame();
			}
			m_UsingSequence = false;
			break;
        }
    }

    IEnumerator CheckSequenceNumber()
    {
		int _i = 0;
		int _it1 = 0;
		int _it2 = 0;

        yield return null;
        switch (m_Team)
        {
            case Team.Team_One:
                while (m_CurrentTime < m_MaxTime)
                {

                    foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_TeamOne)
                    {


					if (_i <= m_SquenceConditionsNb.Count-1)
                        {

                            if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber == m_SquenceConditionsNb[_i])
                            {

                                _i++;
                            }
                            else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber != m_SquenceConditionsNb[_i])
                            {
                                _i = 0;
                            }
                        }
					else if (_i <= m_SquenceConditionsColor.Count-1)
                        {
						Debug.Log ("hehehehehehehe");
							m_ScoreTeam1 ++;
                            m_RightOrder = false;
                            m_TimeToEnd = m_CurrentTime;
                            m_MaxTime = m_CurrentTime;
                        }

                    }
                    yield return new WaitForEndOfFrame();
                }
                m_UsingSequence = false;
                break;
            case Team.Team_Two:

                while (m_CurrentTime < m_MaxTime)
                {

                    foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_TeamTwo)
                    {


					if (_i <= m_SquenceConditionsNb.Count-1)
                        {

                            if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber == m_SquenceConditionsNb[_i])
                            {

                                _i++;
                            }
                            else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber != m_SquenceConditionsNb[_i])
                            {
                                _i = 0;
                            }
                        }
						else if (_i <= m_SquenceConditionsColor.Count-1)
                        {
                            m_RightOrder = false;
							m_ScoreTeam2 ++;
                            m_TimeToEnd = m_CurrentTime;
                            m_MaxTime = m_CurrentTime;
                        }

                    }
                    yield return new WaitForEndOfFrame();
                }
                m_UsingSequence = false;
                break;


		case Team.Both:

			while (m_CurrentTime < m_MaxTime)
			{

				foreach (GameObject Button in Gamemanager_ButtonManager.Instance.m_GlobalListOfButton)
				{

					if (_it1 <= m_SquenceConditionsNb.Count-1)
					{
						if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber == m_SquenceConditionsNb[_it1] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 1)
						{
							_it1++;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
						else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber != m_SquenceConditionsNb[_it1] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 1)
						{
							_it1 = 0;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
					}

					if (_it2 <= m_SquenceConditionsNb.Count-1)
					{
						
						if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber == m_SquenceConditionsNb[_it2] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 2)
						{
							_it2++;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
						else if (Button.GetComponent<Buttons_Propriety>().m_IsPressed && Button.GetComponent<Buttons_Propriety>().m_ButtonNumber != m_SquenceConditionsNb[_it2] && Button.GetComponent<Buttons_Propriety>().m_NumberOfTeam == 2)
						{
							_it2 = 0;
							Button.GetComponent<Buttons_Propriety> ().m_IsPressed = false;
						}
					}

				}
				if (_it1 == m_SquenceConditionsNb.Count)
				{
					m_RightOrder = false;
					m_ScoreTeam1++;
					m_TimeToEnd = m_CurrentTime;
					m_MaxTime = m_CurrentTime;
				}

				if (_it2 == m_SquenceConditionsNb.Count)
				{
					m_RightOrder = false;
					m_ScoreTeam2 ++;
					m_TimeToEnd = m_CurrentTime;
					m_MaxTime = m_CurrentTime;
				}

				yield return new WaitForEndOfFrame();
			}
			m_UsingSequence = false;
			break;

        }
    }


    IEnumerator CheckEndAtelier()
    {
		
        while (true)
        {
            if (m_UsingColor == false && m_UsingInteraction == false && m_UsingNumber == false && m_UsingSequence == false)
            {
                m_GainPoints = (int) Mathf.Round(m_GainPoints / m_CurrentTime); 

				CheckScore();
			
                yield break;
            } 
			if (m_CurrentTime > m_MaxTime)
			{
				CheckScore();
			}
                
		
            yield return new WaitForEndOfFrame();
        }
    }

 
void CheckScore()
{
	if(m_ScoreTeam1 > m_ScoreTeam2)
	{
		//Global Score Team 1 += m_ScoreGain
			FindObjectOfType<ScoreManager>().addScore("red", m_GainPoints);
	}
	else if (m_ScoreTeam1 < m_ScoreTeam2)
	{
		//Global Score Team 2 += m_ScoreGain
			FindObjectOfType<ScoreManager>().addScore("blue", m_GainPoints);
	}

	FindObjectOfType<AteliersManager> ().LaunchNext ();
		this.gameObject.SetActive(false);
}

}