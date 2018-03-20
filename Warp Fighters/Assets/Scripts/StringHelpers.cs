using UnityEngine;

public static class StringHelpers
{
    // formats: 65.4321 -> 01′05′′43
    public static string TimeInSecondsToFormattedString(float timeInSeconds)
	{
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)(timeInSeconds * 100 % 100);

        string addZeroForMin = "";
        string addZeroForSec = "";
        string addZeroForMS = "";
        if (minutes < 10)
        {
            addZeroForMin = "0";
        }
        if (seconds < 10)
        {
            addZeroForSec = "0";
        }
        if (milliseconds < 10)
        {
            addZeroForMS = "0";
        }

        string timeText = addZeroForMin + minutes.ToString() + "′" + addZeroForSec + seconds.ToString() + "′′"
            + addZeroForMS + milliseconds.ToString();

        return timeText;
    }
}
