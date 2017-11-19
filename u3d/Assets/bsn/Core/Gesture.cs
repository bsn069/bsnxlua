using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace NBsn 
{

public class C_Gesture
{
	public void Init(){
		SetSecret();
		EasyTouch.On_SwipeStart += On_SwipeStart;
		EasyTouch.On_SwipeEnd += On_SwipeEnd;		
	}

		
	public void UnInit(){
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;	
	}

	private void On_SwipeStart( Gesture gesture){
		if ((Time.time - m_fLastInputTime) > 1.0f)
		{
			m_index = 0;
		}
		// NBsn.C_Global.Instance.Log.InfoFormat(
		// 	"On_SwipeStart gesture.swipe={0} deltaTime={1} actionTime={2} Time.time={3}"
		// 	, gesture.swipe
		// 	, gesture.deltaTime
		// 	, gesture.actionTime
		// 	, Time.time
		// );
	}

	private void On_SwipeEnd(Gesture gesture){
		m_fLastInputTime = Time.time;
		InputSecret(gesture.swipe);
		NBsn.C_Global.Instance.Log.InfoFormat(
			"On_SwipeEnd gesture.swipe={0} deltaTime={1} actionTime={2} Time.time={3} m_index={4}"
			, gesture.swipe
			, gesture.deltaTime
			, gesture.actionTime
			, Time.time
			, m_index
		);
	}

	public void SetSecret()
	{
		m_index = 0;
		m_Secret = new EasyTouch.SwipeDirection[]{
			EasyTouch.SwipeDirection.Right,
			EasyTouch.SwipeDirection.Down,
			EasyTouch.SwipeDirection.Left,
			EasyTouch.SwipeDirection.Up
		};
	}

	private void InputSecret(EasyTouch.SwipeDirection sd)
	{
		if (m_Secret == null)
		{
			return;
		}

		if (m_Secret[m_index] != sd)
		{
			m_index = 0;
			return;
		}

		++m_index;
		if (m_index >= m_Secret.Length)
		{
			m_index = 0;
			Success();
		}
	}

	private void Success()
	{
		NBsn.C_Global.Instance.Log.Info("Success");
	}

	private EasyTouch.SwipeDirection[] m_Secret = null;
	private int m_index = 0;
	private float m_fLastInputTime = 0;		
}

}