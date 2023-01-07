namespace EffectsBinEditorWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static RoutedCommand NewFileCommand = new RoutedCommand();
    public static RoutedCommand OpenFileCommand = new RoutedCommand();
    public static RoutedCommand SaveFileCommand = new RoutedCommand();
    public static RoutedCommand InsertEffectCommand = new RoutedCommand();
    public static RoutedCommand AboutCommand = new RoutedCommand();
    public static RoutedCommand SaveAsCommand = new RoutedCommand();
    public static RoutedCommand ExitProgramCommand = new RoutedCommand();
    public static RoutedCommand DeleteEffectCommand = new RoutedCommand();

    public MainWindow()
    {
        InitializeComponent();

        NewFileCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        OpenFileCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
        SaveFileCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        InsertEffectCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
        AboutCommand.InputGestures.Add(new KeyGesture(Key.F1));
        ExitProgramCommand.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
        DeleteEffectCommand.InputGestures.Add(new KeyGesture(Key.Delete));
    }

    #region Shortcut keys
    private void NewFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void OpenFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void SaveFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (SaveFile.IsEnabled != true)
        {
            e.CanExecute = false;
            return;
        }
        e.CanExecute = true;
    }

    private void SaveFileAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (SaveFile.IsEnabled != true)
        {
            e.CanExecute = false;
            return;
        }
        e.CanExecute = true;
    }

    private void InsertEffect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (SaveFile.IsEnabled != true)
        {
            e.CanExecute = false;
            return;
        }
        e.CanExecute = true;
    }
    private void DeleteEffect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (SaveFile.IsEnabled != true)
        {
            e.CanExecute = false;
            return;
        }
        e.CanExecute = true;
    }

    private void Authors_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void ExitProgram_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        NewFile.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void OpenFile_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        OpenFile.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void SaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        SaveFile.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void SaveFileAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        SaveFileAs.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void InsertEffect_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        InsertEffect.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void DeleteEffect_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        DeleteEffect.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void Authors_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Authors.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }

    private void ExitProgram_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        ExitProgram.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
    }
    #endregion

    private void CreateFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.CreateFile(this, EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs, StatusLabel, DeleteEffect, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.OpenFile(this, EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs, StatusLabel, DeleteEffect, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    private void SaveFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.SaveFile(StatusLabel);
    }

    private void SaveFileAs_Click(object sender, EventArgs e)
    {
        CEffectsParser.SaveFileAs(StatusLabel);
    }

    private void InsertEffect_Click(object sender, EventArgs e)
    {
        CEffectsParser.InsertEffect(EffectsList, StatusLabel);
    }

    private void Apply_Click(object sender, EventArgs e)
    {
        CEffectsParser.ApplyProperties(this, EffectsList, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        CEffectsParser.DeleteEffect(EffectsList);
    }

    private void EffectsList_IndexChanged(object sender, EventArgs e)
    {
        CEffectsParser.VisualProperties(EffectsList, EffectsList.SelectedIndex, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    private void EffectsList_MouseDown(object sender, MouseEventArgs e)
    {
        EffectsList.SelectedIndex = -1;
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        e.Handled = (e.Key == Key.Space);
        base.OnPreviewKeyDown(e);
    }

    private void FloatOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !FloatOrDoubleCharChecker(Convert.ToChar(e.Text));
        base.OnTextInput(e);
    }

    private void NumericOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IntegerCharChecker(Convert.ToChar(e.Text));
        base.OnTextInput(e);
    }

    private bool FloatOrDoubleCharChecker(char str)

    {
        if (char.IsDigit(str) || str == '-' || str == '.' || str == ',')
            return true;
        else
            return false;
    }

    private bool IntegerCharChecker(char str)

    {
        if (char.IsDigit(str))
            return true;
        else
            return false;
    }

    private void About_Click(object sender, EventArgs e)
    {
        CMessageBoxShow.MessageShow(this, "About", $"Effects.bin Editor\nAuthors: Smelson and Legion.\n(С) {DateTime.Now.Year}. From Russia and Kazakhstan with love!", 255, 46, 169, 218);
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        Application.Current.MainWindow.Close();
    }
}

