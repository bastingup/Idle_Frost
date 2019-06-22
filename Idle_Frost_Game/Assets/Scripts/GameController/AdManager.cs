using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdManager : MonoBehaviour
{
    public string placementId = "rewardedVideo";
    string gameId = "3195839";
    // TODO disable test mode before deploy
    bool testMode = true;

    private void Start()
    {
        Monetization.Initialize(gameId, testMode);
        gameId = CheckForOSAndReturnGameID();
    }

    string CheckForOSAndReturnGameID()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return "3195839";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return "3195838";
        }
        else
        {
            return null;
        }
    }

    public void ShowAd()
    {
        StartCoroutine(WaitForAd());
    }

    IEnumerator WaitForAd()
    {


        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        if (ad != null)
        {
            ad.Show(AdFinished);
            yield return null;
        }
        yield return null;
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // TODO Implement actual reward system
            GameObject.FindObjectOfType<EcoStats>().co2Value -= 10f;
        }
    }
}

