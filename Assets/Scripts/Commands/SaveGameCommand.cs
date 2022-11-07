using Enums;
using Keys;
using System.Collections.Generic;

namespace Commands
{
    public class SaveGameCommand
    {

        public void OnSaveData(SaveLoadStates states, int newValue, string fileName = "SaveFile")
        {
            ES3.Save(states.ToString(), newValue, fileName+ ".es3");
        }

        public void OnSaveListAddElement(SaveLoadStates states, int newValue, string fileName = "SaveFile")
        {
            List<int> tempList = ES3.Load(states.ToString(), fileName + ".es3", new List<int>());
            if (tempList.Contains(newValue))
            {
                return;
            }
            tempList.Add(newValue);
            ES3.Save(states.ToString(), tempList, fileName + ".es3");
        }

        public void OnSaveList(SaveLoadStates states, List<int> listToSave, string fileName = "SaveFile")
        {
            ES3.Save(states.ToString(), listToSave, fileName + ".es3");
        }

        public void OnSaveArray(SaveLoadStates states, int[] array, string fileName = "SaveFile")
        {
            ES3.Save(states.ToString(), array, fileName + ".es3");
        }

        public void OnResetList(SaveLoadStates states, string fileName = "SaveFile")
        {
            List<int> tempList = new List<int>();
            ES3.Save(states.ToString(), tempList, fileName + ".es3");
        }

        public void OnResetArray(SaveLoadStates states, string fileName = "SaveFile")
        {
            int[] tempArray = new int[4] { -1, -1, -1, -1 };
            ES3.Save(states.ToString(), tempArray, fileName+".es3");
        }
    }
}
