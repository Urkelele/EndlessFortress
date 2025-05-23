using TMPro;
using UnityEngine;

public class ExternalDataManager : MonoBehaviour
{
    public int m_AmountTomes = 0;

    public TextMeshProUGUI m_TomesText;

    private void Start()
    {
        m_TomesText.text = m_AmountTomes.ToString();
    }

    public void AddTomes(int numberTomes)
    {
        m_AmountTomes += numberTomes;
        m_TomesText.text = m_AmountTomes.ToString();
    }
}
