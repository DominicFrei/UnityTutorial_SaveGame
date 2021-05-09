using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class SqliteTest : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int hitCount = 0;

    void Awake()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();

        // Read all values from the table.
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        IDataReader dataReader;
        string query = "SELECT * FROM HitCountTable";
        dbCommandReadValues.CommandText = query;
        dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            // The `id` has index 0, our `value` has the index 1.
            hitCount = dataReader.GetInt32(1);
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private void OnDestroy()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();

        // Insert a value into the table.
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTable (id, value) VALUES (0, " + hitCount + ")";
        dbCommandInsertValue.ExecuteNonQuery();
    }

    private void OnMouseDown()
    {
        hitCount++;
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        string dbUri = "URI=file:" + Application.persistentDataPath + "/" + "MyDatabase";
        Debug.Log(Application.persistentDataPath);
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable;
        dbCommandCreateTable = dbConnection.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS HitCountTable (id INTEGER PRIMARY KEY, value INTEGER )";
        dbCommandCreateTable.CommandText = q_createTable;
        dbCommandCreateTable.ExecuteReader();

        return dbConnection;
    }
}