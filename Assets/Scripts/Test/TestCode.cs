using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{


	void Start ()
    {
		
	}
    
	
	void OnGUI()
    {


        if(GUI.Button(new Rect(0,0,100,100), "Save Data"))
        {
            PlayerPrefs.SetInt("TowerSp0" , 0);
            PlayerPrefs.SetInt("TowerSp1", 1);
            PlayerPrefs.SetInt("TowerSp2", 2);

            PlayerPrefs.SetInt("Armor0", 0);
            PlayerPrefs.SetInt("Armor1", 1);
            PlayerPrefs.SetInt("Armor2", 2);
        }

        if (GUI.Button(new Rect(0, 200, 100, 100), "Load Data"))
        {
            Debug.LogFormat("TowerSp0 : {0}", PlayerPrefs.GetInt("TowerSp0"));
            Debug.LogFormat("TowerSp1 : {0}", PlayerPrefs.GetInt("TowerSp1"));
            Debug.LogFormat("TowerSp2 : {0}", PlayerPrefs.GetInt("TowerSp2"));

            Debug.LogFormat("Armor0 : {0}", PlayerPrefs.GetInt("Armor0"));
            Debug.LogFormat("Armor1 : {0}", PlayerPrefs.GetInt("Armor1"));
            Debug.LogFormat("Armor2 : {0}", PlayerPrefs.GetInt("Armor2"));            
        }

    }
}
