using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.Rendering.DebugUI.Table;

public class NotificationsManager : MonoBehaviour
{
    private DateTime now = DateTime.Now;
    //private DateTime fireTime = new DateTime(now.Year, now.Month, now.Day, 10, 30, 0);

    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    private void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications for this class",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //Once the channel is set, start (or not) the daily reminder
        if (PlayerPrefs.GetInt("DailyAlreadySet") == 0)
        {
            //Set it to one so it never works again
            PlayerPrefs.SetInt("DailyAlreadySet", 1);
            PlayerPrefs.Save();
            //We call the notification function for the daily
            DailyReminder();
        }

        //CreateSimpleNotification("30s notif", "The good", 30);
        //CreateSimpleNotification("2m notif", "The bad", 120);
        //CreateSimpleNotification("5m notif", "The ugly", 300);
    }

    void CreateSimpleNotification(string title, string text, int secondsToWait)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(secondsToWait);
        var date = System.DateTime.Now;

        notification.SmallIcon = "icon_small";
        notification.LargeIcon = "icon_large";

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    void DailyReminder()
    {
        //Daily reminder set to 10:30
        var notification = new AndroidNotification();
        notification.Title = "Come play again!";
        notification.Text = "Try another run through the Fortress!";
        //notification.FireTime = System.DateTime.Now.AddDays(1);
        DateTime fireTime = new DateTime(now.Year, now.Month, now.Day, 10, 30, 0);
        notification.FireTime = fireTime;
        notification.RepeatInterval = new System.TimeSpan(1, 0, 0, 0);

        notification.SmallIcon = "smallBonfire";
        notification.LargeIcon = "largeBonfire";

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }



}
