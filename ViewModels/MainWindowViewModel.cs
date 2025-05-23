using System;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using LibVLCSharp.Shared;

namespace MyMusic.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private const int MAX_TIME_IN_SECONDS = 20;
    
    private MediaPlayer mediaPlayer = new MediaPlayer(new LibVLC());

    private DispatcherTimer tmrCountDown = new DispatcherTimer();
    
    private eClef clef;
    private eOctave octave;
    private eNote note;
    
    public MainWindowViewModel() : base()
    {
        tmrCountDown.Interval = TimeSpan.FromSeconds(1);
        tmrCountDown.Tick += tmrCountDown_Tick;  


        SetNextNote();
    }

    private void tmrCountDown_Tick(object source, EventArgs args)
    {
        if (CountDownInSeconds > 0)
        {
            CountDownInSeconds--;
        }
        else
        {
            tmrCountDown.Stop();
            GuessedOk = false;
            GuessedKo = true;
            
            mediaPlayer.Play(new Media(new LibVLC(), "../../../Assets/no.wav"));
        }
    }
    
    public void SetNextNote()
    {
        countDown = MAX_TIME_IN_SECONDS;
        tmrCountDown.Start();
        
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

    private int countDown = MAX_TIME_IN_SECONDS;

    public int CountDownInSeconds
    {
        get => countDown;
        set
        {
            countDown = value;
            OnPropertyChanged();
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
        
        tmrCountDown.Stop();
        
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