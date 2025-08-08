using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeaderUnderliner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] headers;

    public void SetHighlight(int thisIndex)
    {
        foreach (TextMeshProUGUI tmp in headers)
            tmp.fontStyle = FontStyles.Normal;
        headers[thisIndex].fontStyle = FontStyles.Underline;
    }
}
