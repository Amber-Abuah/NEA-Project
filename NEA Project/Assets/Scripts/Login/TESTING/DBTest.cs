using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    string conn;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;

    void Start()
    {
        PerformInsertIntoPlayerActivityCommand(3, 400, 10, 10, 11, 6.5f);
    }

    void SetUpConnection()
    {
        conn = "URI=file:" + Application.streamingAssetsPath + @"/Nea.db";
        dbconn = (IDbConnection)new SqliteConnection(conn); // Create a new connection based on the connection path just located
        dbconn.Open(); // Open the connection
    }

    void CloseConnection()
    {
        // Nullify and dispose of reader and connection
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }


    public int PerformIntSelectCommand(string selectClause, string tableClause)
    {
        try
        {
            SetUpConnection();

            dbcmd = dbconn.CreateCommand();
            int returnInt = 0;

            string sqlQuery = string.Format("SELECT {0} FROM {1} ORDER BY {0} DESC LIMIT 1", selectClause, tableClause);

            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                returnInt = reader.GetInt32(0);
            }

            CloseConnection();

            return returnInt;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return 0;
        }
    }

    public string PerformInsertIntoPlayerActivityCommand(int playerID, int score, int numOfQuestions, int numOfCorrectAnswers, int numOfIncorrectAnswers, float averageAttempts)
    {
        try
        {
            int currentActivityID = PerformIntSelectCommand("ActivityID", "Activity");
            Debug.Log(currentActivityID);

            SetUpConnection();

            dbcmd = dbconn.CreateCommand();
            string returnInfo = "";

            string sqlQuery = string.Format("INSERT INTO Activity(ScoreValue, NumOfQuestions, NumOfCorrectAnswers, NumOfIncorrectAnswers, AverageAttempts, Date) VALUES ({0}, {1}, {2}, {3}, {4}, '{5}')", score, numOfQuestions, numOfCorrectAnswers, numOfIncorrectAnswers, averageAttempts, DateTime.Now.ToString("yyyy-MM-dd"));
            string sqlQuery2 = string.Format("INSERT INTO PlayerActivity (PlayerID, ActivityID) VALUES ('{0}', {1})", playerID, currentActivityID);

            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                returnInfo = reader.GetString(0);
            }

            reader.Close();

            dbcmd.CommandText = sqlQuery2;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                returnInfo = reader.GetString(0);
            }

            CloseConnection();

            return returnInfo;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return "";
        }
    }
}
