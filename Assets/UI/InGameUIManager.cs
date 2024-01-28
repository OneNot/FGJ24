using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Image dashIcon, vaultIcon;

    private void Awake()
    {
        SetDashState(false);
    }

    public void SetDashState(bool setEnabled)
    {
        dashIcon.color = new Color(dashIcon.color.r, dashIcon.color.g, dashIcon.color.b, setEnabled ? 1f : 0.45f);
    }

    public void SetVaultState(bool setEnabled)
    {
        vaultIcon.color = new Color(vaultIcon.color.r, vaultIcon.color.g, vaultIcon.color.b, setEnabled ? 1f : 0.45f);
    }
}
