using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManipulator
{
    string conn;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;

    void SetUpConnection(string sqlQuery)
    {
        conn = "URI=file:" + Application.streamingAssetsPath + @"/Nea.db";
        dbconn = (IDbConnection)new SqliteConnection(conn); // Create a new connection based on the connection path just located
        dbconn.Open(); // Open the connection

        // Create the query for the database
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = sqlQuery;

        // Excute the reader
        reader = dbcmd.ExecuteReader();
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

    public string PerformSelectCommand(string selectClause, string tableClause, string whereClause, string compareClause)
    {
        try
        {
            string returnInfo = "";
            SetUpConnection(string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", selectClause, tableClause, whereClause, compareClause));

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

    /*public List<string> PerformListStringSelectCommand(string selectClause, string tableClause, string whereClause, string compareClause)
    {
        try
        {
            List<string> returnInfo = new List<string>();

            SetUpConnection(string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", selectClause, tableClause, whereClause, compareClause));

            while (reader.Read())
            {
                returnInfo.Add(reader.GetString(0));
            }

            CloseConnection();

            return returnInfo;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }*/

    public List<string> PerformRevisionLinksSelectCommand(int playerID, int revisionTopicID)
    {
        try
        {
            List<string> returnInfo = new List<string>();

            SetUpConnection(string.Format(@"SELECT RevisionLinkAddress
FROM Player, PlayerRevisionLinks, RevisionLinks
WHERE PlayerRevisionLinks.PlayerID = {0} AND Player.PlayerID = {0}
AND PlayerRevisionLinks.RevisionLinkID = RevisionLinks.RevisionLinkID
AND RevisionTopicID = {1}", playerID, revisionTopicID));

            while (reader.Read())
            {
                returnInfo.Add(reader.GetString(0));
            }

            CloseConnection();

            return returnInfo;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public int PerformIntSelectCommand(string selectClause, string tableClause, string whereClause, string compareClause)
    {
        try
        {
            int returnInt = 0;

            SetUpConnection(string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", selectClause, tableClause, whereClause, compareClause));

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


    public string PerformInsertCommand(string username, string password, string firstName, string lastName)
    {
        try
        {
            string returnInfo = "";

            SetUpConnection(string.Format("INSERT INTO Player(Username, Password, Firstname, Lastname) VALUES ('{0}', '{1}', '{2}', '{3}')", username, password, firstName, lastName));

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

    public string PerformInsertRevisionLinkCommand(int playerID, int revisionLinkID)
    {
        try
        {
            string returnInfo = "";

            SetUpConnection(string.Format("INSERT INTO PlayerRevisionLinks(PlayerID, RevisionLinkID) VALUES ({0}, {1})", playerID, revisionLinkID));

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

    public int PerformIntSelectCommand(string selectClause, string tableClause)
    {
        try
        {
            int returnInt = 0;

            SetUpConnection(string.Format("SELECT {0} FROM {1} ORDER BY {0} DESC LIMIT 1", selectClause, tableClause));

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

    public string PerformInsertIntoPlayerActivityCommand(int playerID, int score, int numOfCorrectAnswers, int numOfIncorrectAnswers, int revisionTopicID)
    {
        try
        {
            string returnInfo = "";

            int currentActivityID = PerformIntSelectCommand("ActivityID", "Activity");
            currentActivityID++;

            SetUpConnection(string.Format("INSERT INTO Activity(ScoreValue, NumOfCorrectAnswers, NumOfIncorrectAnswers, Date) VALUES ({0}, {1}, {2}, '{3}')", score, numOfCorrectAnswers, numOfIncorrectAnswers, DateTime.Now.ToString("yyyy-MM-dd")));

            while (reader.Read())
            {
                returnInfo = reader.GetString(0);
            }

            CloseConnection();

            SetUpConnection(string.Format("INSERT INTO PlayerActivity (PlayerID, ActivityID, RevisionTopicID) VALUES ('{0}', {1}, {2})", playerID, currentActivityID, revisionTopicID));

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

    public List<RevisionLink> RetrieveRevisionLinks(int revisionLinkId)
    {
        try
        {
            List<RevisionLink> revisionLinks = new List<RevisionLink>();

            SetUpConnection(string.Format("SELECT RevisionLinkAddress, RevisionLinkID FROM RevisionLinks WHERE RevisionTopicID = '" + revisionLinkId.ToString() + "'"));

            while (reader.Read())
            {
                revisionLinks.Add(new RevisionLink(reader.GetString(0), reader.GetInt32(1)));
            }

            CloseConnection();

            return revisionLinks;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public List<DatabaseActivityData> PerformSelectPlayerActivity(int playerID)
    {
        try
        {
            List<DatabaseActivityData> databaseActivityData = new List<DatabaseActivityData>();

            SetUpConnection(string.Format(@"SELECT ScoreValue, NumOfCorrectAnswers, NumOfIncorrectAnswers, Date, RevisionTopics.RevisionTopicID
FROM Player, PlayerActivity, Activity, RevisionTopics
WHERE Player.PlayerID = {0} AND PlayerActivity.PlayerID = {0}
AND Activity.ActivityID = PlayerActivity.ActivityID
AND RevisionTopics.RevisionTopicID = PlayerActivity.RevisionTopicID
ORDER BY Date DESC, ScoreValue DESC;", playerID));

            while (reader.Read())
            {
                databaseActivityData.Add(new DatabaseActivityData(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4)));
            }

            CloseConnection();

            return databaseActivityData;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }
}