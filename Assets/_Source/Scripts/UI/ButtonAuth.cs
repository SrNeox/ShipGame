using UnityEngine;
using YG;

public class ButtonAuth : MonoBehaviour
{
    public void Autorization()
    {
        if (!YG2.player.auth)
            YG2.OpenAuthDialog();
    }
}
