The format of writing a script for the SI game is as follows:
WAVE DELAY/WAVE TIME/FOOD BLOB MOVEMENT SPEED/INTERVAL BETWEEN FOODBLOB SPAWNS/MIN NUTRIENTS PER BLOB/MAX NUTRIENTS PER BLOB/NUTRIENT COLORS PER WAVE

---------
Notes:

WAVE DELAY: The delay between two different waves in the same script in seconds.

WAVE TIME: The total time that a current wave with the specified values on the line runs for in seconds.

FOOD BLOB MOVEMENT SPEED: The speed that the blob moves down the ITween Path in the small intestine. Higher numbers = faster

INTERVAL BETWEEN FOODBLOB SPAWNS: Time between the spawns of each food blob in this specific wave. Higher number = longer delay

MIN NUTRIENTS PER BLOB: The minimum number of nutrients in a food blob. Choose a number between 1 and 6

MAX NUTRIENTS PER BLOB: The maximum bumber of nutrients in a food blob. Choose a number less than 6 that is equal to or greater than the min nutrients per blob value.

NUTRIENT COLORS PER WAVE: Colors of nutrients that can be put on the food blob. Choices are any combination of R (Protein), Y (Carbs), G (Fats)