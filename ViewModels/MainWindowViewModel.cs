using System;
using Avalonia.Media.Imaging;
using LibVLCSharp.Shared;

namespace MyMusic.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private MediaPlayer mediaPlayer = new MediaPlayer(new LibVLC());
    
    private eClef clef;
    private eOctave octave;
    private eNote note;
    
    public MainWindowViewModel() : base()
    {
        SetNextNote();
    }

    public void SetNextNote()
    {
        GuessedOk = false;
        GuessedKo = false;
        
        clef = NoteChooser.GetNextClef();
        octave = NoteChooser.GetNextNoteOctave();
        note = NoteChooser.GetNextNote();

        CurrentImage = (clef == eClef.Bass ? Fscore : GScore);

        if (clef == eClef.Treble)
        {
            NotePosition = (80 + 40 * 5) - (int)note * 20 - 140 * (int)octave;
        }
        else
        {
            NotePosition = (80 + 40 * 6) - (int)note * 20; // - 140 * (int)octave;
        }
    }

    private bool guessedOk = false;

    public bool GuessedOk
    {
        get => guessedOk;
        set
        {
            guessedOk = value;
            OnPropertyChanged();
        }
    }

    private bool guessedKo = false;
    public bool GuessedKo
    {
        get => guessedKo;
        set
        {
            guessedKo = value;
            OnPropertyChanged();
        }
    }
    
    public bool GuessNote(eNote note)
    {
        GuessedKo = false;
        GuessedOk = (note == this.note);
        GuessedKo = !GuessedOk;

        if (guessedOk)
        {
            mediaPlayer.Play(new Media(new LibVLC(), "../../../Assets/yes.wav"));    
        }
        else
        {
            mediaPlayer.Play(new Media(new LibVLC(), "../../../Assets/no.wav"));
        }
        
        return GuessedOk;
    }

    private int notePosition = 0;
    
    // 80 means first line of the score
    // 80 + 40 * 4 means last line of the score
    public int NotePosition
    {
        get => notePosition;
        set
        {
            notePosition = value;
            OnPropertyChanged();
        }
    }
    
    private Bitmap currentImage;

    public Bitmap CurrentImage
    {
        get => currentImage;
        set
        {
            currentImage = value;
            OnPropertyChanged();
        }
    }
    public Bitmap? GScore { get; } = new Bitmap("../../../Assets/Gscore.png"); 
    
    public Bitmap? Fscore { get; } = new Bitmap("../../../Assets/Fscore.png");
}