using System.Windows.Media;

namespace EffectsBinEditorWPF.Effects_File;
public class CEffectsParser
{
    static string sPath;

    static List<CEffectsDescription> effectsDescriptionList = new List<CEffectsDescription>();
    static ushort headSgn;
    static uint headSize;
    static uint effectsCount;
    static ushort effectSgn;
    static uint effectSize;
    static byte[] unknown0;
    static float effectPositionX;
    static float effectPositionY;
    static float effectPositionZ;
    static float unknown1;
    static uint effectId;

    public static void CreateFile(ListBox effectsListBox, MenuItem insert, Button apply, MenuItem saveFile, MenuItem saveFileAs, Label StatusLabel, MenuItem deleteButton, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        sPath = SetPath();
        if (sPath == null)
            return;

        ClearFields(effectsListBox, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);

        using (FileStream fileStream = new FileStream(sPath, FileMode.Create))
        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
        {
            headSgn = BitConverter.ToUInt16(CEffectsConsts.FILESGN, 0);
            headSize = BitConverter.ToUInt16(CEffectsConsts.FILESIZE, 0);

            binaryWriter.Write(headSgn);
            binaryWriter.Write(headSize);

            binaryWriter.Seek(2, SeekOrigin.Begin);

            FileInfo fileInfo = new FileInfo(sPath);
            long size = fileInfo.Length;
            byte[] sizeout = BitConverter.GetBytes(size);
            binaryWriter.Write(sizeout, 0, 4);

            VisualEffects(effectsListBox);
            EnableButtons(insert, apply, saveFile, saveFileAs, deleteButton, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
            StatusLabel.Content = $"New file created, its location - ({sPath})";
        }
    }

    public static void OpenFile(ListBox effectsListBox, MenuItem insert, Button apply, MenuItem saveFile, MenuItem saveFileAs, Label StatusLabel, MenuItem deleteButton, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        sPath = GetPath();
        if (sPath == null)
            return;

        ClearFields(effectsListBox, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);

        using (FileStream fileStream = new FileStream(sPath, FileMode.Open))
        using (BinaryReader binaryReader = new BinaryReader(fileStream))
        {
            headSgn = binaryReader.ReadUInt16(); if (headSgn != 100) { MessageBox.Show("Невозможно прочитать файл!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); Error(effectsListBox, insert, apply, saveFile, saveFileAs, StatusLabel, deleteButton, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox); return; }
            headSize = binaryReader.ReadUInt32();

            effectsCount = (headSize - 6) / 74;

            for (int i = 0; i < effectsCount; i++)
            {
                effectSgn = binaryReader.ReadUInt16();
                effectSize = binaryReader.ReadUInt32();
                unknown0 = binaryReader.ReadBytes(48);
                effectPositionX = binaryReader.ReadSingle();
                effectPositionY = binaryReader.ReadSingle();
                effectPositionZ = binaryReader.ReadSingle();
                unknown1 = binaryReader.ReadSingle();
                effectId = binaryReader.ReadUInt32();

                effectsDescriptionList.Add(new CEffectsDescription(effectSgn, effectSize, unknown0, effectPositionX, effectPositionY, effectPositionZ, unknown1, effectId));
            }
            VisualEffects(effectsListBox);
            EnableButtons(insert, apply, saveFile, saveFileAs, deleteButton, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
            StatusLabel.Content = $"File opened - ({sPath})";
        }
    }

    public static void SaveFile(Label StatusLabel)
    {
        if (effectsDescriptionList == null)
            return;

        using (FileStream fileStream = new FileStream(sPath, FileMode.Create))
        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
        {
            binaryWriter.Write(CEffectsConsts.FILESGN);
            binaryWriter.Write(CEffectsConsts.FILESIZE);

            for (int i = 0; i < effectsDescriptionList.Count; i++)
            {
                binaryWriter.Write(effectsDescriptionList[i].effectSgn);
                binaryWriter.Write(effectsDescriptionList[i].effectSize);
                binaryWriter.Write(effectsDescriptionList[i].unknown0);
                binaryWriter.Write(effectsDescriptionList[i].effectPositionX);
                binaryWriter.Write(effectsDescriptionList[i].effectPositionY);
                binaryWriter.Write(effectsDescriptionList[i].effectPositionZ);
                binaryWriter.Write(effectsDescriptionList[i].unknown1);
                binaryWriter.Write(effectsDescriptionList[i].effectId);
            }

        }

        FileInfo fileInfo = new FileInfo(sPath);
        long size = fileInfo.Length;
        byte[] sizeout = BitConverter.GetBytes(size);

        using (FileStream fileStream = new FileStream(sPath, FileMode.OpenOrCreate))
        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
        {
            binaryWriter.Seek(2, SeekOrigin.Begin);
            binaryWriter.Write(sizeout, 0, 4);
        }

        StatusLabel.Content = $"File saved - ({sPath})";
    }

    public static void SaveFileAs(Label StatusLabel)
    {
        string newPath = NewPath(sPath);
        if (newPath == null || newPath == "Canceled")
        {
            return;
        }

        sPath = newPath;
        SaveFile(StatusLabel);
    }

    public static void InsertEffect(ListBox effectsListBox, Label StatusLabel)
    {
        if (effectsListBox == null)
            return;

        effectsDescriptionList.Add(new CEffectsDescription(CEffectsConsts.EFFECTSGN, CEffectsConsts.EFFECTSIZE, CEffectsConsts.UNKNOWN0, 0, 0, 0, CEffectsConsts.UNKNOWN1, 0));
        VisualEffects(effectsListBox);
        StatusLabel.Content = $"File changed - ({sPath}*)";
    }

    public static void ApplyProperties(ListBox effectsListBox, TextBox XCoordinate, TextBox YCoordinate, TextBox ZCoordinate, TextBox EffectID)
    {
        int selectedIndex = effectsListBox.SelectedIndex;

        if (selectedIndex < 0)
            return;

        try
        {
            effectSgn = effectsDescriptionList[selectedIndex].effectSgn;
            effectSize = effectsDescriptionList[selectedIndex].effectSize;
            unknown0 = effectsDescriptionList[selectedIndex].unknown0;
            effectPositionX = float.Parse(XCoordinate.Text);
            effectPositionY = float.Parse(YCoordinate.Text);
            effectPositionZ = float.Parse(ZCoordinate.Text);
            unknown1 = effectsDescriptionList[selectedIndex].unknown1;
            effectId = uint.Parse(EffectID.Text);

            effectsDescriptionList[selectedIndex] = new CEffectsDescription(effectSgn, effectSize, unknown0, effectPositionX, effectPositionY, effectPositionZ, unknown1, effectId);
            VisualEffects(effectsListBox);
            VisualProperties(effectsListBox, selectedIndex, XCoordinate, YCoordinate, ZCoordinate, EffectID);
            effectsListBox.SelectedIndex = selectedIndex;
        }

        catch
        {
            VisualProperties(effectsListBox, selectedIndex, XCoordinate, YCoordinate, ZCoordinate, EffectID);
        }
    }

    public static void DeleteEffect(ListBox effectsListBox)
    {
        int selectedIndex = effectsListBox.SelectedIndex;

        if (selectedIndex < 0 || effectsDescriptionList == null)
            return;

        effectsDescriptionList.RemoveAt(selectedIndex);
        VisualEffects(effectsListBox);

        if (selectedIndex - 1 < 0)
            effectsListBox.SelectedIndex = selectedIndex;

        else
            effectsListBox.SelectedIndex = selectedIndex - 1;
    }

    private static void VisualEffects(ListBox effectsListBox)
    {
        effectsListBox.Items.Clear();

        if (effectsDescriptionList == null)
            return;

        for (int i = 0; i < effectsDescriptionList.Count; i++)
        {
            effectsListBox.Items.Add($"№{i + 1}. (Effect ID: {effectsDescriptionList[i].effectId.ToString()})");
        }
    }

    public static void VisualProperties(ListBox effectsListBox, int selectedIndex, TextBox XCoordinate, TextBox YCoordinate, TextBox ZCoordinate, TextBox EffectID)
    {
        if (effectsDescriptionList.Count == 0)
        {
            XCoordinate.Text = null;
            YCoordinate.Text = null;
            ZCoordinate.Text = null;
            EffectID.Text = null;
            return;
        }

        if (selectedIndex < 0) { return; }

        XCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionX.ToString();
        YCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionY.ToString();
        ZCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionZ.ToString();
        EffectID.Text = effectsDescriptionList[selectedIndex].effectId.ToString();
    }

    private static void EnableButtons(MenuItem insert, Button apply, MenuItem saveFile, MenuItem saveFileAs, MenuItem deleteButton, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        insert.IsEnabled = true;
        apply.IsEnabled = true;
        saveFile.IsEnabled = true;
        saveFileAs.IsEnabled = true;
        deleteButton.IsEnabled = true;
        XCoordTextBox.IsEnabled = true;
        YCoordTextBox.IsEnabled = true;
        ZCoordTextBox.IsEnabled = true;
        EffectIDTextBox.IsEnabled = true;
    }
    private static void DisableButtons(MenuItem insert, Button apply, MenuItem saveFile, MenuItem saveFileAs, MenuItem deleteButton, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        insert.IsEnabled = false;
        apply.IsEnabled = false;
        saveFile.IsEnabled = false;
        saveFileAs.IsEnabled = false;
        deleteButton.IsEnabled = false;
        XCoordTextBox.IsEnabled = false;
        YCoordTextBox.IsEnabled = false;
        ZCoordTextBox.IsEnabled = false;
        EffectIDTextBox.IsEnabled = false;
    }

    private static void Error(ListBox effectsListBox, MenuItem insert, Button apply, MenuItem saveFile, MenuItem saveFileAs, Label StatusLabel, MenuItem deleteButton, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        effectsDescriptionList.Clear();
        effectsListBox.Items.Clear();
        DisableButtons(insert, apply, saveFile, saveFileAs, deleteButton, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
        StatusLabel.Content = string.Empty;
    }

    private static void ClearFields(ListBox effectsListBox, TextBox XCoordTextBox, TextBox YCoordTextBox, TextBox ZCoordTextBox, TextBox EffectIDTextBox)
    {
        effectsDescriptionList.Clear();
        effectsListBox.Items.Clear();
        XCoordTextBox.Clear();
        YCoordTextBox.Clear();
        ZCoordTextBox.Clear();
        EffectIDTextBox.Clear();
    }

    private static string GetPath()
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Effects.bin file(*.bin)|*.bin|All Files(*.*)|*.*";
        fileDialog.Title = "Открытие файла";

        if (fileDialog.ShowDialog() != true)
            return null;

        return fileDialog.FileName;
    }

    private static string SetPath()
    {
        CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
        commonFileDialog.IsFolderPicker = true;
        commonFileDialog.Title = "Создание файла";

        if (commonFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
            return null;

        return commonFileDialog.FileName + "\\effects.bin";
    }

    private static string NewPath(string oldPath)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Effects.bin file(*.bin)|*.bin|All Files(*.*)|*.*";
        saveFileDialog.Title = "Сохранение";

        if (saveFileDialog.ShowDialog() == true)
            return saveFileDialog.FileName;

        return "Canceled";
    }
}
