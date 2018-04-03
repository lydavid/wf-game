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


    /*
     * Given 1, returns 1st, 2, 2nd, etc.
     */
    public static string FormatRank(int rank)
    {
        string rankString = rank.ToString();
        int lastTwoDigits = rank % 100;
        string suffix = "th";

        int lastDigit = lastTwoDigits % 10;
        if (lastTwoDigits != 11 && lastTwoDigits != 12 && lastTwoDigits != 13)
        {
            if (lastDigit == 1)
            {
                suffix = "st";
            } else if (lastDigit == 2)
            {
                suffix = "nd";
            } else if (lastDigit == 3)
            {
                suffix = "rd";
            }
        }
        return rankString + suffix;
    }
}
