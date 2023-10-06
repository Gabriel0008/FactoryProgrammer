using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engenhoca", menuName = "Factory/Level")]
public class LevelsSO : ScriptableObject
{

    List<Level> levels = null;

    public void SetLevel(int atualLevel,int atualStars,bool atualOpen)
    {
        
        Level newLevel = new Level(atualLevel, atualStars, atualOpen);
        if (levels!=null)
        {
            int index = levels.FindIndex(x => x.ID == newLevel.ID);
            levels[index].RefreshLevel(newLevel);
        }
        else
        {
            levels = new List<Level>();
            levels.Add(newLevel);
        }
    }

    public void ShowLevels()
    {
        if (levels != null) {
            for (int i =0; i< levels.Count; i++)
            {
                Debug.Log(levels[i].ToString());
            }
        }

    }

    public class Level
    {
        public int ID { get; private set; }
        public int Stars { get; private set; }
        public bool Open { get; private set; }

        public Level()
        {

        }

        public Level(int number, int stars, bool open)
        {
            ID = number;
            Stars = stars;
            Open = open;
        }

        public void RefreshLevel(Level newLevel)
        {
            ID = newLevel.ID;
            Stars = newLevel.Stars;
            Open = newLevel.Open;
        }

        public override string ToString()
        {
            return ""+ID+":"+Stars+" - "+Open;
        }
    }
}
