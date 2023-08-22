namespace Effects.bin_Editor.Effects_File;

class CEffectsDescription
{
    public ushort effectSgn;
    public uint effectSize;
    public byte[] unknown0;
    public float effectPositionX;
    public float effectPositionY;
    public float effectPositionZ;
    public float unknown1;
    public uint effectId;

    public CEffectsDescription(ushort effectSgn, uint effectSize, byte[] unknown0, float effectPositionX, float effectPositionY, float effectPositionZ, float unknown1, uint effectId)
    {
        this.effectSgn = effectSgn;
        this.effectSize = effectSize;
        this.unknown0 = unknown0;
        this.effectPositionX = effectPositionX;
        this.effectPositionY = effectPositionY;
        this.effectPositionZ = effectPositionZ;
        this.unknown1 = unknown1;
        this.effectId = effectId;
    }
}
