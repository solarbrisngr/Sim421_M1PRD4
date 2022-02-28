using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;

public class getPlayfabItems : MonoBehaviour
{

    // Catalog info
    public string catalogName = "MoonStuff1.0";
    private List<CatalogItem> catalog;
    private List<MoonStuff> moon = new List<MoonStuff>();


    private void Update()
    {
        // Catalog button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetCatalog();
        }

    }
    public void GetCatalog()
    {
        GetCatalogItemsRequest getCatalogRequest = new GetCatalogItemsRequest
        {
            CatalogVersion = catalogName
        };

        PlayFabClientAPI.GetCatalogItems(getCatalogRequest,
            result =>
            {
                catalog = result.Catalog;
            },
            error => Debug.Log(error.ErrorMessage)
        );

        Invoke("SplitCatalog", 3f);
    }

    public void SplitCatalog()
    {
        foreach (CatalogItem item in catalog)
        {
            MoonStuff m = JsonUtility.FromJson<MoonStuff>(item.CustomData);
            m.name = item.DisplayName;
            m.url = item.ItemImageUrl;
            m.phase = item.Description;
            moon.Add(m);
        }

        ShowCatalog();
    }

    public void ShowCatalog()
    {
        foreach (MoonStuff m in moon)
        {
            string logMsg = "" + m.name + "";
            logMsg += "\nImage URL: " + m.url;
            logMsg += "\nPhase: " + m.phase;
           
            Debug.Log(logMsg);

        }
    }


    public class MoonStuff
    {
        public string name;
        public string url;
        public string phase;

    }
}