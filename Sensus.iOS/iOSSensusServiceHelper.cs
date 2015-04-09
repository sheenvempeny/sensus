﻿// Copyright 2014 The Rector & Visitors of the University of Virginia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using SensusService;
using Xamarin.Geolocation;
using Xamarin;
using SensusService.Probes.Location;
using SensusService.Probes;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Sensus.iOS
{
    public class iOSSensusServiceHelper : SensusServiceHelper
    {
        public const string SENSUS_CALLBACK_REPEAT_DELAY = "SENSUS-CALLBACK-REPEAT-DELAY";

        private Dictionary<int, UILocalNotification> _callbackIdNotification;

        public override bool IsCharging
        {
            get
            {
                return false;  // TODO:  Check status
            }
        }

        public override bool WiFiConnected
        {
            get
            {
                return false;  // TODO:  Check status
            }
        }

        public override string DeviceId
        {
            get
            {
                return "device";  // TODO:  Get ID
            }
        }

        public override string OperatingSystem
        {
            get
            {
                return "ios";  // TODO:  Get version
            }
        }

        protected override Geolocator Geolocator
        {
            get
            {
                return new Geolocator();
            }
        }

        public iOSSensusServiceHelper()
        {
            _callbackIdNotification = new Dictionary<int, UILocalNotification>();
        }

        protected override void InitializeXamarinInsights()
        {
            Insights.Initialize(XAMARIN_INSIGHTS_APP_KEY);
        }

        public override bool Use(Probe probe)
        {
            return true;
        }

        protected override void ScheduleRepeatingCallback(int callbackId, int initialDelayMS, int repeatDelayMS)
        {
            ScheduleCallback(callbackId, initialDelayMS, true, repeatDelayMS);
        }

        protected override void ScheduleOneTimeCallback(int callbackId, int delayMS)
        {
            ScheduleCallback(callbackId, delayMS, false, -1);
        }

        private void ScheduleCallback(int callbackId, int delayMS, bool repeating, int repeatDelayMS)
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    UILocalNotification notification = new UILocalNotification
                    {
                        FireDate = DateTime.UtcNow.AddMilliseconds((double)delayMS).ToNSDate(),
                        SoundName = UILocalNotification.DefaultSoundName
                    };

                    notification.UserInfo = new NSDictionary(
                        SENSUS_CALLBACK_KEY, true, 
                        SENSUS_CALLBACK_ID_KEY, callbackId,
                        SENSUS_CALLBACK_REPEATING_KEY, repeating,
                        SENSUS_CALLBACK_REPEAT_DELAY, repeatDelayMS);

                    if(repeating)
                        _callbackIdNotification.Add(callbackId, notification);

                    UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                });
        }

        protected override void UnscheduleCallback(int callbackId, bool repeating)
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    UIApplication.SharedApplication.CancelLocalNotification(_callbackIdNotification[callbackId]);
                });
        }

        public void RescheduleCallbackNotifications()
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (UILocalNotification notification in _callbackIdNotification.Values)
                        UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                });
        }

        public override void PromptForAndReadTextFileAsync(string promptTitle, Action<string> callback)
        {
        }

        public override void ShareFileAsync(string path, string subject)
        {
        }

        public override void TextToSpeechAsync(string text, Action callback)
        {
        }

        public override void PromptForInputAsync(string prompt, bool startVoiceRecognizer, Action<string> callback)
        {
        }

        public override void FlashNotificationAsync(string message, Action callback)
        {
        }

        public override void KeepDeviceAwake()
        {
        }

        public override void LetDeviceSleep()
        {
        }

        public override void UpdateApplicationStatus(string status)
        {
        }                           
    }
}

