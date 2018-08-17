using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

// Help for this class:
// https://github.com/playgameservices/play-games-plugin-for-unity

public class LeaderBoardManagerScript : MonoBehaviour {

    public static LeaderBoardManagerScript instance;

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }  
    //}

    void Start()
    {
        PlayGamesPlatform.Activate();
        Login();
        
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) => {
            if (!success)
            {

            }
            else
            {

            }
        });

    }

    public void AddScoreToLeaderBoard(int score)
    {
        Social.ReportScore(score, LeaderBoard.leaderboard_luckiest_of_all_time, (bool success) =>
        {

        });
    }

    public void ShowLeaderBoard()
    {
        if (Social.localUser.authenticated)
        {
            // This will show all Leaderboards
            //Social.ShowLeaderboardUI();

            // This will show a specific leader board
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoard.leaderboard_luckiest_of_all_time);
        }
        else
        {
            Login();
        }
        
    }
}
