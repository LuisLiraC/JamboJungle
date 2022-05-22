using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class GameManager : MonoBehaviour
{
    public int collectedObjects = 0;
    public static GameManager sharedInstance;
    private string playerIdentity;
    private FirebaseFirestore db;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        db = FirebaseFirestore.DefaultInstance;

        if (PlayerPrefs.GetString("identity", "unknown") == "unknown")
        {
            playerIdentity = System.Guid.NewGuid().ToString();
            collectedObjects = 0;
            PlayerPrefs.SetString("identity", playerIdentity);
            DocumentReference newUser = db.Collection("users").Document(playerIdentity);
            var userInfo = new Dictionary<string, object>
            {
                { "score", collectedObjects },
                { "test", 10 }
            };
            newUser.SetAsync(userInfo);
            Debug.Log("Player Created Successfully");
            UIManager.instance.UpdateScore(collectedObjects);
        } 
        else
        {
            playerIdentity = PlayerPrefs.GetString("identity");
            DocumentReference userInfo = db.Collection("users").Document(playerIdentity);
            userInfo.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    var playerInfo = snapshot.ToDictionary();
                    collectedObjects = Convert.ToInt32(playerInfo["score"]);
                    UIManager.instance.UpdateScore(collectedObjects);
                }
            });
        }
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObjects += collectable.value;
        StoreCoins();
        UIManager.instance.UpdateScore(collectedObjects);
    }

    private void StoreCoins()
    {
        DocumentReference userInfo = db.Collection("users").Document(playerIdentity);
        userInfo.UpdateAsync("score", collectedObjects);
    }
}
