using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AcidFlowControl : MonoBehaviour
{


    private bool isClicked = false;
    private Image i;
    public Sprite[] FlowStateImages;
    private int counter = 0;
    public int ImSpeed;

	// Timer
	private float startTime;
	private float timeElapsed;
	private float maxInterval = 5f;



	public AudioClip pipeAudio;
	AudioSource audio;
	private bool pipeAudioBoolean;


    public void ButtonToggle()
    {
        if (isClicked)
        {
            isClicked = false;
            //swap texture to OFF
        }
        else
        {
            isClicked = true;
            //swap texture to ON
			startTime = Time.time;
        }
    }

    // Use this for initialization
    void Start()
    {
        i = GetComponent<Image>();
		//GetComponent<AudioSource>().clip = pipeAudio;
		audio = GetComponent<AudioSource>();
		pipeAudioBoolean = false;

    }

    // Update is called once per frame
    void Update()
    {
		
		if (!pipeAudioBoolean && isClicked) {
			audio.PlayOneShot (pipeAudio, 1.0f);
			//GetComponent<AudioSource>().Play();
			Debug.Log ("audio played");
			pipeAudioBoolean = true;
		}

        if (isClicked == false)
        {
            i.sprite = FlowStateImages[0];

			pipeAudioBoolean = false;
            return;
        }
        if (isClicked == true)
        {
			//Debug.Log ("pipeAudioBoolean:" + pipeAudioBoolean);
			timeElapsed = Time.time - startTime;
			if (timeElapsed > maxInterval) isClicked = false;
			if (counter < ImSpeed)
            {
                i.sprite = FlowStateImages[1];
                counter++;
                return;
            }
            else
            {
                i.sprite = FlowStateImages[2];
                counter++;
                if (counter == ImSpeed * 2)
                    counter = 0;
                return;
            }
        }



    }
}
