using UnityEngine;
using System.Collections;
using System.IO;

/**
 * controls script loading for the small intestine game
 */
public class LoadScript
{
	/**
	 * Constructor
	 */
	public LoadScript() {}		

	private Color Fats1Color = new Color(37f/255f, 97f/255f, 139f/255f, 1); 	//!< create a new color for the Fats1 Particles

	/**
	 * Handles parsing of raw wave data files
	 */
	public SIWave[] loadIntestineLevel(int level, bool tutorial, int tutLevel)
	{
		TextAsset lev;
		if(!tutorial){
			lev= Resources.Load ("NewSILevel/newSILevel" + level) as TextAsset;  // for ipad we need to load resources instead of using a file
		}
		else{ 
			lev = Resources.Load ("NewSILevel/newSILevelTutorial" + tutLevel) as TextAsset;  // for ipad we need to load resources instead of using a file
		}
		StringReader reader = new StringReader (lev.text);

		string[] lines = lev.text.Split("\n"[0]);	// split just to count number of lines to create waves array

		SIWave[] waves = new SIWave[lines.Length];

		string line;	// store the content of the current line
		int j = 0;  	// index for lines array

		// parse the script into si waves
		while((line = reader.ReadLine()) != null)
		{
			SIWave wave = new SIWave();			// object that holds all information about a given wave

			string[] waveInfo = line.Split("/"[0]);					// split the current wave line for easier parsing
			wave.startDelay = float.Parse(waveInfo[0]);				// the first field in the line is the startDelay
			wave.runTime = float.Parse(waveInfo[1]);				// the second field in the line is the runTime
			wave.nutrientSpeed = float.Parse(waveInfo[2]);			// the third field in the line is the nutrientSpeed
			wave.nutrientSpawnInterval = float.Parse(waveInfo[3]);	// the fouth field in the line is the nutrientSpawnInterval
			wave.minBlobs = int.Parse(waveInfo[4]);					// the fifth field in the line is the minBlobs
			wave.maxBlobs = int.Parse(waveInfo[5]);					// the sixth field in the line is the maxBlobs

			Color[] colors = new Color[(waveInfo[6].Length)];		// the seventh field in the line are the colors
			// the colors need to be parsed to add the actual colors to the array instead of the text codes for them
			for(int k = 0; k < colors.Length; k++)
			{
				switch (waveInfo[6][k])
				{
				case 'R':
					colors[k] = new Color(235f/255f, 0f, 139f/255f);	// pink color (can see better than red)
					break;
				case 'Y':
					colors[k] = Color.yellow;
					break;
				case 'G':
					colors[k] = Fats1Color;
					break;
				}
			}
			wave.colors = colors;

			waves[j] = wave;			// save the wave in the array
			j++;						// increment the counter
		}
		return waves;
	}
}

/**
 * class that represents the format of an si wave
 */
public class SIWave
{
	public float startDelay;
	public float runTime;
	public float nutrientSpeed;
	public float nutrientSpawnInterval;
	public int minBlobs;
	public int maxBlobs;
	public Color[] colors;
}
	