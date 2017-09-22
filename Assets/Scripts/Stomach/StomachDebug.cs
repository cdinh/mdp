using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Debugger logic
 */
public class StomachDebug : MonoBehaviour 
{
	/**
	 * Debugger items to pull values from
	 */
	public UnityEngine.UI.InputField maxFoodInput;
	public UnityEngine.UI.InputField timeToBurnInput;
	public UnityEngine.UI.InputField timeForSlimeDecayInput;
	public UnityEngine.UI.InputField timeToReviveInput;
	public UnityEngine.UI.InputField timeToDieInput;
	public UnityEngine.UI.InputField maxCellDeathsInput;
	public UnityEngine.UI.InputField acidityThresholdInput;
	public UnityEngine.UI.InputField basicThresholdInput;
	public UnityEngine.UI.InputField acidSpeedInput;
	public UnityEngine.UI.InputField baseSpeedInput;
	public UnityEngine.UI.InputField idleDecaySpeedInput;
	public UnityEngine.UI.Toggle useDebuggerToggle;

	/**
	 * Remember the original values of all variables
	 */
	private int originalMaxFood;
	private float originalTimeToBurn;
	private float originalTimeForSlimeDecay;
	private float originalTimeToRevive;
	private float originalTimeToDie;
	private int originalMaxCellDeaths;
	private float originalAcidityThreshold;
	private float originalBasicThreshold;
	private float originalAcidSpeed;
	private float originalBaseSpeed;
	private float originalIdleDecaySpeed;

	/**
	 * Actual variables holding the values we are using
	 * parsed from the debugger
	 */
	private int maxFood;
	private float timeToBurn;
	private float timeForSlimeDecay;
	private float timeToRevive;
	private float timeToDie;
	private int maxCellDeaths;
	private float acidityThreshold;
	private float basicThreshold;
	private float acidSpeed;
	private float baseSpeed;
	private float idleDecaySpeed;
	private bool useDebugger;

	/**
	 * References to scripts we need to change the values in if the debugger is used
	 */
	public PhBar phbar;
	public StomachGameManager stomachGameManager;
	public StomachChymeNew stomachChyme;
	public StomachGameOver stomachGameOver;

	/**
	 * Get the original values of potentially modified values to remember later
	 */
	void Start()
	{
		originalAcidSpeed = phbar.startingAcidSpeed;
		originalBaseSpeed = phbar.startingBaseSpeed;
		originalIdleDecaySpeed = phbar.decaySpeed;
		originalTimeToBurn = stomachGameManager.TIME_TO_BURN;
		originalTimeForSlimeDecay = stomachGameManager.TIME_FOR_SLIME_FADE;
		originalTimeToRevive = stomachGameManager.TIME_TO_REVIVE;
		originalTimeToDie = stomachGameManager.TIME_TO_DIE;
		originalMaxCellDeaths = stomachGameManager.MAX_CELL_DEATHS;
		originalAcidityThreshold = stomachChyme.ACIDIC;
		originalBasicThreshold = stomachChyme.BASIC;
		originalMaxFood = stomachGameOver.maxFood;
	}

	/**
	 * Function that describes what happens when the debug menu is submitted
	 */
	public void onSubmit()
	{
		//check if debugger is toggled
		useDebugger = useDebuggerToggle.isOn;

		// check for missing fields in text entry
		if(useDebugger &&
		   (maxFoodInput.text.Trim().Length == 0 ||
		    timeToBurnInput.text.Trim().Length == 0 ||
		    timeForSlimeDecayInput.text.Trim().Length == 0 ||
		    timeToReviveInput.text.Trim().Length == 0 ||
		    timeToDieInput.text.Trim().Length == 0 ||
		    maxCellDeathsInput.text.Trim().Length == 0 ||
		    acidityThresholdInput.text.Trim().Length == 0 ||
		    basicThresholdInput.text.Trim().Length == 0 ||
		    acidSpeedInput.text.Trim().Length == 0 ||
		    baseSpeedInput.text.Trim().Length == 0 ||
		    idleDecaySpeedInput.text.Trim().Length == 0))
	    {
			// just ignore all values because this is easiest and isn't in release builds so some bugs ok
			return;
		}

		// parse out the actual values if they are there
		if (useDebugger)
		{
			maxFood = int.Parse(maxFoodInput.text);
			timeToBurn = float.Parse(timeToBurnInput.text);
			timeForSlimeDecay = float.Parse(timeForSlimeDecayInput.text);
			timeToRevive = float.Parse(timeToReviveInput.text);
			timeToDie = float.Parse(timeToDieInput.text);
			maxCellDeaths = int.Parse(maxCellDeathsInput.text);
			acidityThreshold = float.Parse(acidityThresholdInput.text);
			basicThreshold = float.Parse(basicThresholdInput.text);
			acidSpeed = float.Parse(acidSpeedInput.text);
			baseSpeed = float.Parse(baseSpeedInput.text);
			idleDecaySpeed = float.Parse(idleDecaySpeedInput.text);

			// now set the fields
			phbar.startingAcidSpeed = acidSpeed;
			phbar.startingBaseSpeed = baseSpeed;
			phbar.decaySpeed = idleDecaySpeed;
			stomachGameManager.TIME_TO_BURN = timeToBurn;
			stomachGameManager.TIME_FOR_SLIME_FADE = timeForSlimeDecay;
			stomachGameManager.TIME_TO_REVIVE = timeToRevive;
			stomachGameManager.TIME_TO_DIE = timeToDie;
			stomachGameManager.MAX_CELL_DEATHS = maxCellDeaths;
			stomachChyme.ACIDIC = acidityThreshold;
			stomachChyme.BASIC = basicThreshold;
			stomachGameOver.maxFood = maxFood;
		} else
		{
			phbar.startingAcidSpeed = originalAcidSpeed;
			phbar.startingBaseSpeed = originalBaseSpeed;
			phbar.decaySpeed = originalIdleDecaySpeed;
			stomachGameManager.TIME_TO_BURN = originalTimeToBurn;
			stomachGameManager.TIME_FOR_SLIME_FADE = originalTimeForSlimeDecay;
			stomachGameManager.TIME_TO_REVIVE = originalTimeToRevive;
			stomachGameManager.TIME_TO_DIE = originalTimeToDie;
			stomachGameManager.MAX_CELL_DEATHS = originalMaxCellDeaths;
			stomachChyme.ACIDIC = originalAcidityThreshold;
			stomachChyme.BASIC = originalBasicThreshold;
			stomachGameOver.maxFood = originalMaxFood;
		}
	}
}
