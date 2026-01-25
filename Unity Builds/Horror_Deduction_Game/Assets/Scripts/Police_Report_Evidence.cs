using UnityEngine;
using TMPro;
public class Police_Report_Evidence : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Evidence_Data evidenceSO;

    public GameObject policeReportUI;

    public void InitializePoliceReport()
    {
        title.text = evidenceSO.policeReportTitle;
        description.text = evidenceSO.policeReportDescription;
    }

    public void CloseReportMenu()
    {
        policeReportUI.SetActive(false);
    }

    public void OpenReportMenu()
    {
        policeReportUI.SetActive(true);
    }
}
