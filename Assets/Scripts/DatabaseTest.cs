using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatabaseTest : MonoBehaviour
{

    [SerializeField] TMP_InputField to = null;
    [SerializeField] TMP_InputField from = null;
    [SerializeField] TMP_InputField body = null;
    [SerializeField] public TextMeshProUGUI toText = null;
    [SerializeField] public TextMeshProUGUI fromText = null;
    [SerializeField] public TextMeshProUGUI bodyText = null;
    [SerializeField] DatabaseController database = null;

    public void SendToDatabase()
    {
        database.PutToDatabase(to.text, from.text, body.text);
    }


    public void InitDatabaseCount()
    {
        database.InitDatabaseCount();
    }


    public void GetRandomLetter()
    {
        LetterData data = database.GetRandomLetter();
    }

    public void UpdateUI(string to, string from, string body)
    {
        toText.text = to;
        fromText.text = from;
        bodyText.text = body;
    }
}
