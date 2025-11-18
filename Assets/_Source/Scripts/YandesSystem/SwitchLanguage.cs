using UnityEngine;
using YG;

public class SwitchLanguage : MonoBehaviour
{
    private readonly string Russian = "ru";
    private readonly string Englisgh = "en";
    private readonly string Turkish = "tr";

    public void ChangeRussian()
    {
        YG2.SwitchLanguage(Russian);
    }

    public void ChangeEnglish()
    {
        YG2.SwitchLanguage(Englisgh);
    }

    public void ChangeTurkish()
    {
        YG2.SwitchLanguage(Turkish);
    }
}
