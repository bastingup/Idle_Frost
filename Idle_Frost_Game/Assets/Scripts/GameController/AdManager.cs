using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

[System.Serializable]
public class Reward
{
    public enum Type { uranium, radiation, campfire, wood, playerHealth };
    public Type typeOfReward;

    public string rewardMsg;
    public int rewardValue;
    public GameObject target;
}

public class AdManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject player, gameController, campfirePrefab;
    private string placementId = "rewardedVideo", gameId;
    private bool testMode = true, adViewable;

    private int rewardIndex;

    [SerializeField]
    private Button adButton;

    public Reward[] rewards;
    #endregion

    private void Start()
    {
        // Check in the beginning for OS
        gameId = CheckForOSAndReturnGameID();

        // TODO disable test mode before deploy
        Monetization.Initialize(gameId, testMode);

        adViewable = true;
        rewardIndex = DetermineReward();
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
            return "Unidentified mobile OS";
        }
    }

    public void ShowAd()
    {
        if (adViewable)
        {
            adViewable = false;
            StartCoroutine(WaitForAd());
        }
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
            StartCoroutine(AdCountDown());
            RewardPlayer(rewardIndex);
        }
    }
    IEnumerator AdCountDown()
    {
        int countdown = 30;
        adButton.GetComponentInChildren<Text>().text = countdown.ToString() + " seconds";
        while (countdown > 0)
        {
            adButton.GetComponentInChildren<Text>().text = countdown.ToString() + " seconds";
            yield return new WaitForSeconds(1);
            countdown--;
        }
        rewardIndex = DetermineReward();
        adViewable = true;
        ChangeButtonColor(Color.white);
    }
    
    void RewardPlayer(int i)
    {
        // TODO make a reward system that actually makes sense
        switch (rewards[i].typeOfReward)
        {
            case Reward.Type.radiation:
                rewards[i].target.GetComponent<EcoStats>().radiation -= rewards[i].rewardValue;
                break;
            case Reward.Type.uranium:
                rewards[i].target.GetComponent<PlayerInventory>().uranium += rewards[i].rewardValue;
                break;
            case Reward.Type.campfire:
                Transform t = rewards[i].target.transform;
                Vector2 spawnAt = new Vector2(t.position.x + 1, t.position.y);
                Instantiate(campfirePrefab, spawnAt, new Quaternion(0, 0, 0, 1));
                break;
            case Reward.Type.wood:
                rewards[i].target.GetComponent<PlayerInventory>().wood += rewards[i].rewardValue;
                break;
            case Reward.Type.playerHealth:
                rewards[i].target.GetComponent<PlayerHealth>().playerHealth += rewards[i].rewardValue;
                break;
        }
        ChangeButtonColor(Color.grey);
    }
    
    // TODO better reward determination
    int DetermineReward()
    {
        /*
        int i;
        // Check for radiation
        if (gameController.GetComponent<EcoStats>().radiation >= 30)
            i = 0;
        // Check for player temp
        if (player.GetComponent<PlayerInventory>().uranium <= 1)
            i = 1;
        // Check for player temp
        if (player.GetComponent<PlayerHealth>().playerTemp <= 10)
            i = 2;
        else
            i = UnityEngine.Random.Range(3, 4);
        */

        int i = (int)UnityEngine.Random.Range(0, 5);

        adButton.GetComponentInChildren<Text>().text = rewards[i].rewardMsg;
        return i;
    }

    void ChangeButtonColor(Color col)
    {
        adButton.GetComponent<Image>().color = col;
    }
}

