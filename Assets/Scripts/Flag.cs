[System.Serializable]
public class Flag
{
    public string NameFlag;
    public ScrollType ScrollType;
    public float NormalizateValue;

    public Flag() { }
    public Flag(Flag _f)
    {
        NameFlag = _f.NameFlag;
        ScrollType = _f.ScrollType;
        NormalizateValue = _f.NormalizateValue;
    }
}