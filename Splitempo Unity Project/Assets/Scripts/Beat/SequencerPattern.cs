using UnityEngine;

[System.Serializable]
public class Sequence
{
    [SerializeField] private bool[] notes = new bool[8];
    public bool HasNoteThisBeat => notes[BeatManager.I.CurrentBeatInBar];

    public int BeatsBeetweenCurrentNotes {
        get
        {
            var _previousNote = GetPreviousNote;
            var _nextNote = GetNextNote;            

            if(_previousNote == null) { return 0;}

            if(_previousNote == _nextNote){
                return 8;
            }

            if(_previousNote > _nextNote){
                return 8 -(int)_previousNote + (int)_nextNote;
            }

            return (int)_nextNote - (int)_previousNote;
        }
    }



    public int? GetLastNote
    {
        get{
            for (int i = notes.Length - 1; i >= 0; i--)
            {
                if (notes[i])
                {
                    return i;
                }
            }
            return null;            
        }
    }

    public int? GetFirstNote
    {
        get{
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i])
                {
                    return i;
                }
            }
            return null;            
        }
    }


    public int? GetPreviousNote
    {
        get{
            int? time = null;
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i] && i <= BeatManager.I.CurrentBeatInBar)
                {
                    time = i;
                }
            }
            return time == null ? GetLastNote : time;
        }
    }

    public int? GetNextNote
    {
        get{
            int? time = null;
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i] && i >= BeatManager.I.CurrentBeatInBar)
                {
                    time = i;
                }
            }
            return time == null ? GetFirstNote : time;
        }
    }
}

