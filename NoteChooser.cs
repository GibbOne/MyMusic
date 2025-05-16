using System;

namespace MyMusic;

public enum eNote
{
    C,
    D,
    E,
    F,
    G,
    A,
    B,
}

public enum eOctave
{
    O1,
    O2
}

public enum eClef
{
    Treble,
    Bass
}

public static class NoteChooser
{
    private static Random rnd = new Random();
   
    public static eNote GetNextNote()
    {
        return (eNote)rnd.Next((int)eNote.C, (int)eNote.B + 1);
    }
    
    public static eOctave GetNextNoteOctave()
    {
        return (eOctave)rnd.Next((int)eOctave.O1, (int)eOctave.O2 + 1);
    }

    public static eClef GetNextClef()
    {
        return (eClef)rnd.Next((int)eClef.Treble, (int)eClef.Bass + 1);
    }

}