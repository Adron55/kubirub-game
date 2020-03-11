using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using MiniJSON_Min;
public class onesignaltest : MonoBehaviour
{

    void Start()
    {
        // Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
        // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

        OneSignal.StartInit("503506cc-010c-4ad7-a999-3c2745ac9f37")
          .HandleNotificationReceived(HandleNotificationReceived)
          .HandleNotificationOpened(HandleNotificationOpened)
          .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        if (PlayerPrefs.GetString("uuid")!=null)
        {
            OneSignal.SendTag("uuid", PlayerPrefs.GetString("uuid"));
        }
    }

    // Gets called when the player opens the notification.
    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
    }
    private static void HandleNotificationReceived(OSNotification notification)
    {
    }
}