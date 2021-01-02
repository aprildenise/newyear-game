using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEditor;
using UnityEngine;

public class DatabaseController : MonoBehaviour
{

    public DatabaseTest test;
    private int count = -1;

    private void Start()
    {
        GetDatabaseCount();
    }

    public bool GetDatabaseCount()
    {
        RestClient.Get<CountData>(Secrets.COUNT_URI).Then(response =>
        {
            // TODO: Handle on failure.
            count = response.count;
            Debug.Log("count on get:" + count);
        });
        return true;
    }

    /// <summary>
    /// Init the database count by adding that item to the database.
    /// Should only be used in testing.
    /// </summary>
    /// <returns></returns>
    public bool InitDatabaseCount()
    {
        CountData countData = new CountData(0);
        RestClient.Put(Secrets.COUNT_URI, countData);
        count = 0;
        return true;
    }

    public bool PutToDatabase(string to, string from, string body)
    {
        // Create a letter data.
        LetterData letterData = new LetterData(to, from, body);

        // Attempt to place it into the database using a unique index.
        if (count == -1)
        {
            // If failed to get the database count, try again.
            GetDatabaseCount();
        }
        // TODO: Handle here when we can't get the count at all.
        RestClient.Put(Secrets.LETTERS_URL + count + "/.json", letterData);

        // Update the count.
        count++;
        CountData countData = new CountData(count);
        RestClient.Put(Secrets.COUNT_URI, countData);
        Debug.Log("count after post:" + count);

        return true;
    }

    public LetterData GetRandomLetter()
    {
        // Attempt to place it into the database using a unique index.
        if (count == -1)
        {
            // If failed to get the database count, try again.
            GetDatabaseCount();
        }
        // TODO: Handle on failure.

        int random = Random.Range(0, count);
        RestClient.Get<LetterData>(Secrets.LETTERS_URL + random + "/.json").Then(response =>
        {
            // Test only.
            test.UpdateUI(response.toAddress, response.fromAddress, response.letterBody);
        });

        // TODO: Handle on failure.

        return null;
    }


}


[System.Serializable]
public class LetterData
{
    public string toAddress;
    public string fromAddress;
    public string letterBody;

    public LetterData(string toAddress, string fromAddress, string letterBody)
    {
        this.toAddress = toAddress;
        this.fromAddress = fromAddress;
        this.letterBody = letterBody;
    }

}

[System.Serializable]
public class CountData
{
    public int count;

    public CountData(int count)
    {
        this.count = count;
    }
}