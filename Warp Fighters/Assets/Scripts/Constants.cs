using UnityEngine;

public static class Constants
{
    // Primary key
    public const string ID_KEY = "ID"; // Unique integer key, order of entry into file

    public const string DATE_KEY = "date"; // The datetime this entry was recorded, stored as string

    // The 3 main columns display on the leaderboards are Rank, Score, Name
    // Rank will not be stored, as it will continuously be recalculated at the end of a game session
    public const string SCORE_KEY = "score"; // Player's time in seconds, stored as float
    public const string NAME_KEY = "name"; // 3 letter chosen name like in old school arcades

    // More info columns
    public const string KILLS_KEY = "kills"; // num enemies killed
    public const string WARPS_KEY = "warps"; // num times warped


}
