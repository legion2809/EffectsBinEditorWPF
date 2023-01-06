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
    public static void CreateFile()
    {

    }

    public static void OpenFile(ListBox effectsListBox, MenuItem insert, Button apply)
    {
        sPath = GetPath();
        if (sPath == null)
            return;

        effectsDescriptionList.Clear();

        using (FileStream fileStream = new FileStream(sPath, FileMode.Open))
        using (BinaryReader binaryReader = new BinaryReader(fileStream))
        {
            headSgn = binaryReader.ReadUInt16(); if (headSgn != 100) { MessageBox.Show("Невозможно прочитать файл!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); Error(effectsListBox, insert, apply); return; }
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
            EnableButtons(insert, apply);
        }    
    }

    private static void VisualEffects(ListBox effectsListBox)
    {
        effectsListBox.Items.Clear();

        if (effectsDescriptionList == null)
            return;

        for (int i = 0; i < effectsDescriptionList.Count; i++)
        {
            effectsListBox.Items.Add($"№{i+1}. (Effect ID: {effectsDescriptionList[i].effectId.ToString()})");
        }
    }

    public static void VisualProperties(ListBox effectsListBox, int selectedIndex, TextBox XCoordinate, TextBox YCoordinate, TextBox ZCoordinate, TextBox EffectID)
    {
        if (effectsDescriptionList.Count == 0) {
            XCoordinate.Text = null;
            YCoordinate.Text = null;
            ZCoordinate.Text = null;
            EffectID.Text = null;
            return;
        }

        XCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionX.ToString();
        YCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionY.ToString();
        ZCoordinate.Text = effectsDescriptionList[selectedIndex].effectPositionZ.ToString();
        EffectID.Text = effectsDescriptionList[selectedIndex].effectId.ToString();
    }

    private static void EnableButtons(MenuItem insert, Button apply)
    {
        insert.IsEnabled = true;
        apply.IsEnabled= true;
    }
    private static void DisableButtons(MenuItem insert, Button apply)
    {
        insert.IsEnabled = false;
        apply.IsEnabled = false;
    }

    private static void Error(ListBox effectsListBox, MenuItem insert, Button apply)
    {
        effectsDescriptionList.Clear();
        effectsListBox.Items.Clear();
        DisableButtons(insert, apply);
    }

    private static string GetPath()
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Effects.bin file(*.bin)|*.bin|All Files(*.*)|*.*";
        fileDialog.Title = "Открытие файла";
        if (fileDialog.ShowDialog() != true)
        {
            return null;
        }

        return fileDialog.FileName;
    }
}
