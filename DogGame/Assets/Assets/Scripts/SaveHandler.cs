using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OVRSimpleJSON;

public class SaveHandler : MonoBehaviour
{

    public string couchBool;
    public string bedBool;
    public string firePlaceBool;
    public string stoveBool;


    public void Save()
    {


        JSONObject saveJson = new JSONObject();
        saveJson.Add("couchBool", couchBool);
        saveJson.Add("bedBool", bedBool);
        saveJson.Add("firePlaceBool", firePlaceBool);
        saveJson.Add("stoveBool", stoveBool);

        Debug.Log(saveJson.ToString());

        //Save JSON IN COMPUTER
        string path = Application.persistentDataPath + "/Save.json";
        File.WriteAllText(path, saveJson.ToString());
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/Save.json";
        string jsonString = File.ReadAllText(path);
        JSONObject saveJson = (JSONObject)JSON.Parse(jsonString);

        //Set Values
        couchBool = saveJson["couchBool"];
        bedBool = saveJson["bedBool"];
        firePlaceBool = saveJson["firePlaceBool"];
        stoveBool = saveJson["stoveBool"];
    }
}
