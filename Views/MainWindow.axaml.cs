using Avalonia.Controls;
using Avalonia.Interactivity;
using MyMusic.ViewModels;

namespace MyMusic.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowState = WindowState.FullScreen;
    }
    
    private void Note_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var viewModel=DataContext as MainWindowViewModel;
        viewModel.GuessNote((eNote)(sender as Button).Tag);
    }

    private void Result_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var viewModel=DataContext as MainWindowViewModel;
        viewModel.SetNextNote();
    }
}