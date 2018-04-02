using UnityEngine;

public static class Constants
{
    // Primary key
    public const int ID_INDEX = 0;
    public const string ID_KEY = "ID"; // Unique integer key, order of entry into file

    public const int DATE_INDEX = 1;
    public const string DATE_KEY = "Date"; // The datetime this entry was recorded, stored as string

    // The 3 main columns display on the leaderboards are Rank, Score, Name
    // Rank will not be stored, as it will continuously be recalculated at the end of a game session
    public const int SCORE_INDEX = 2;
    public const string SCORE_KEY = "Elapsed Time"; // Player's time in seconds, stored as float
    public const int NAME_INDEX = 3;
    public const string NAME_KEY = "Initials"; // 3 letter chosen name like in old school arcades

    // More info columns
    public const int WARPS_INDEX = 4;
    public const string WARPS_KEY = "Warp Count"; // num times warped
    public const int KILLS_INDEX = 5;
    public const string KILLS_KEY = "Enemies Killed"; // num enemies killed


    public const string SCENE_TO_LOAD = "Scene";
}
