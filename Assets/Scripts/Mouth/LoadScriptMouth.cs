using UnityEngine;
using System.Collections;
using System.IO;

/**
 * class that handles loading scripts for the mouth game
 */
public class LoadScriptMouth 
{
	/**
	 * Constructor
	 */
	public LoadScriptMouth() {}

	/**
	 * function that handles the majority of script loading for mouth game
	 */
	public MouthWave[] loadMouthLevel(int level)
	{
		TextAsset lev = Resources.Load ("MouthLevel" + level) as TextAsset;  // for ipad we need to load resources instead of using a file
		
		StringReader reader = new StringReader (lev.text);
		
		string[] lines = lev.text.Split("\n"[0]);							// split just to count number of lines to create waves array
		
		MouthWave[] waves = new MouthWave[lines.Length];					
		
		string line;	// store the content of the current line
		int j = 0;  	// index for lines array

		// parse through the file
		while((line = reader.ReadLine()) != null)
		{
			MouthWave wave = new MouthWave();					// object that holds all information about a given wave
			
			string[] waveInfo = line.Split("/"[0]);				// split the current wave line for easier parsing
			wave.startDelay = float.Parse(waveInfo[0]);			// the first field in the split is the startDelay
			wave.runTime = float.Parse(waveInfo[1]);			// the second field in the split is the runTime
			wave.foodSpeed = float.Parse(waveInfo[2]);			// the third field in the split is the foodSpeed
			wave.foodSpawnInterval = float.Parse(waveInfo[3]);	// the fourth field in the split is the foodSpawnInterval
			
			waves[j] = wave;									// store the parsed data in the waves array
			j++;												// increment the index
		}

		return waves;											// return the parsed script data as mouthWaves
	}
}

/**
 * class that defines a mouth wave
 */
public class MouthWave
{
	public float startDelay;
	public float runTime;
	public float foodSpeed;
	public float foodSpawnInterval;
}
