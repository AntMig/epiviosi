using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//ADD NEW LANGUAGES IDENTIFIER HERE
public enum LANGUAGE
{
	KEY = 0,
	FR = 2,
	EN = 1

}

public class CSVLoader : Singleton<CSVLoader>
{

	List<List<string>> _strings = new List<List<string>>();

	private List<List<string>> strings
	{
		get
		{
			return this._strings;
		}
		set
		{
			this._strings = value;
		}
	}

	public LANGUAGE currentLanguage;

	// Use this for initialization
	public override void Awake () {
		base.Awake();
		// TODO 
		// 
		//Add new lines for new files
		//
		Debug.Log("CSVLoader - loading localisation files");
		fgCSVReader.LoadFromFile(Application.streamingAssetsPath + "/Texts/d3.csv", new fgCSVReader.ReadLineDelegate(ReadLineTest));

		Debug.Log("CSVLoader - localisation files loaded");
	}

	void ReadLineTest(int line_index, List<string> line, string fileName)
	{
		if (!KeyExist(line[0].ToLower()))
		{
			for (int i = 0; i < line.Count; i++)
			{
				if (strings.Count <= i)
				{
					strings.Add(new List<string>());
				}

				if (strings[i].Count == 0)
				{
					strings[i].Add(line[i].ToLower());
				}
				else
				{
					if (line[i].ToLower() != "")
					{
						string myValue = line[i];

						if (i == 0)
						{
							//We are handling a key value
							myValue = fileName + "_" + myValue;
							myValue = myValue.ToLower();
						}

						strings[i].Add(myValue);
					}
					else
					{
						strings[i].Add("");
					}
				}
			}
		}
		else
		{
			if(line[0].ToLower() != "key")
			{
				Debug.LogError("CSVLoader.cs - ReadLineTest Errpr = Duplicate entry for Key :" + line[0].ToLower());
			}
		}
	}

	private List<string> GetSpecificLanguageList(LANGUAGE lang)
	{
		for (int i = 0; i < strings.Count; i++)
		{
			if (strings[i][0] == lang.ToString().ToLower())
			{
				return strings[i];
			}
		}

		Debug.LogError("CSVLoader.cs - GetSpecificLanguageList - No list found for language :" + lang.ToString());
		return null;
	}

	public bool KeyExist(string key)
	{
		if (strings.Count >= 3)
		{
			if (strings[0].IndexOf(key.ToLower()) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	public void SetLanguage (LANGUAGE lang)
	{
		currentLanguage = lang;
	}

	public string GetString (string key)
	{
		return GetString(key, currentLanguage);
	}

	public string GetString(string key, LANGUAGE lang)
	{
		//lookup the index of the key
		int keyIndex = strings[0].IndexOf(key.ToLower());

		if(keyIndex < 0)
		{
			Debug.LogWarning("CSVLoader.cs - GetString - No match for key " + key);
			return "No match for key :" + key ;
		}

		//return the associated string in the right language list
		string rawKey = GetSpecificLanguageList(lang)[keyIndex];

		if (rawKey == "")
		{
			Debug.LogWarning("CSVLoader.cs - GetString - Key " + key + " exist but no translation is available in language :"+currentLanguage.ToString());
			return key + "/" + currentLanguage.ToString();
		}

		return rawKey;
	}

	// Update is called once per frame
	void Update () {

	}
}