using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTextMenu : MonoBehaviour
{
    public void ReloadText()
    {
        LocalizationManager.Instance.Reload();
    }
}
