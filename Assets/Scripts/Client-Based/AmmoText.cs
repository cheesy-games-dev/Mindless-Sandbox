using TMPro;
using UnityEngine;

public class AmmoText : MonoBehaviour
{
    public static string text
    {
        get =>
            instance.tmp.text;
        set
        {
            instance.tmp.text = value;
        }
    }
    private static AmmoText instance;
    private TMP_Text tmp;

    void Start()
    {
        instance = this;
        tmp = GetComponent<TMP_Text>();
    }
}
