using UnityEngine;
using UnityEditor;

public class FlagDataAsset : MonoBehaviour
{
    [MenuItem("Assets/Create/Flag Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<FlagData>();
    }
}
