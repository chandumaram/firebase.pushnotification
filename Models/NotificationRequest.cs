﻿namespace firebase.pushnotification.Models
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string DeviceToken { get; set; }
    }
}
