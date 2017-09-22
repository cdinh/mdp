using UnityEngine;
using System.Collections;

/**
 * script that can display the fps
 * script was taken from online
 * minor edits done by UCI Down with Food team
 */
public class HUDFPS : MonoBehaviour
{
	
	// Attach this to a GUIText to make a frames/second indicator.
	//
	// It calculates frames/second over each updateInterval,
	// so the display does not keep changing wildly.
	//
	// It is also fairly accurate at very low FPS counts (<10).
	// We do this not by simply counting frames per interval, but
	// by accumulating FPS for each frame. This way we end up with
	// correct overall FPS even if the interval renders something like
	// 5.5 frames.

	public  float updateInterval = 0.5F;
	public bool FPSActive = false;
	DebugConfig debugConfig;
	private float accum = 0; //!< FPS accumulated over the interval
	private int   frames = 0; //!< Frames drawn over the interval
	private float timeleft; //!< Left time for current interval
	
	void Start ()
	{
		debugConfig = ((GameObject)GameObject.Find ("Debug Config")).GetComponent<DebugConfig> ();	// find the debugger since
									// we enable frame rate display with that
		timeleft = updateInterval;  
	}
	
	void Update ()
	{
		FPSActive = debugConfig.FPSActive;			// get the value for fps active from the debugger
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		if (FPSActive) 
		{
				// Interval ended - update GUI text and start new interval
			if (timeleft <= 0.0) 
			{
				// display two fractional digits (f2 format)
				float fps = accum / frames;
				// display the fps
				string format = System.String.Format ("{0:F2} FPS", fps);
				GetComponent<GUIText>().text = format;
			
				// color display text based on FR "quality"
				if (fps < 30)
					GetComponent<GUIText>().material.color = Color.yellow;
				else if (fps < 10)
					GetComponent<GUIText>().material.color = Color.red;
				else
					GetComponent<GUIText>().material.color = Color.green;

				timeleft = updateInterval;
				accum = 0.0F;
				frames = 0;
			}
		} else 
		{
			// just leave the string blank when we don't want to display the fps
			string format = System.String.Format ("");
			GetComponent<GUIText>().text = format;
		}
	}
}
