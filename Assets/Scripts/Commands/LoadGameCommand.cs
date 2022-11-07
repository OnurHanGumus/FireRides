using Enums;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Commands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {

            if (!ES3.FileExists(fileName + ".es3")) {  return 0; }
            return ES3.Load<int>(saveLoadStates.ToString(), fileName+ ".es3", 0);
        }

        public List<int> OnLoadList(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {
            if (!ES3.FileExists(fileName + ".es3")) {Debug.Log("çalışmadı"); return new List<int>(); }
            return ES3.Load(saveLoadStates.ToString(), fileName + ".es3", new List<int>());
        }

        public int[] OnLoadArray(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {
            if (!ES3.FileExists(fileName + ".es3")) { Debug.Log("çalışmadı"); return new int[5] { -1, -1, -1, -1 , -1}; }
            return ES3.Load(saveLoadStates.ToString(), fileName + ".es3", new int[5] { -1, -1, -1, -1 , -1});
        }
    }
}
