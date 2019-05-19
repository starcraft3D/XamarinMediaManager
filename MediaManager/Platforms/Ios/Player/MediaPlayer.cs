﻿using System;
using AVFoundation;
using AVKit;
using Foundation;
using MediaManager.Platforms.Apple.Media;
using MediaManager.Platforms.Ios.Video;
using MediaManager.Video;
using MediaPlayer;
using UIKit;

namespace MediaManager.Platforms.Ios.Media
{
    public class MediaPlayer : AppleMediaPlayer, IMediaPlayer<AVPlayer, VideoView>
    {
        public VideoView PlayerView { get; set; }
        public override IVideoView VideoView => PlayerView;

        protected override void Init()
        {
            base.Init();
            var audioSession = AVAudioSession.SharedInstance();
            try
            {
                audioSession.SetCategory(AVAudioSession.CategoryPlayback);
                audioSession.SetActive(true, out NSError activationError);
                if (activationError != null)
                    Console.WriteLine("Could not activate audio session {0}", activationError.LocalizedDescription);
            }
            catch
            {
            }

            InvokeOnMainThread(() => {
                UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();
            });

            var commandCenter = MPRemoteCommandCenter.Shared;
            commandCenter.TogglePlayPauseCommand.Enabled = true;
            commandCenter.TogglePlayPauseCommand.AddTarget(TogglePlayPauseCommand);
        }

        private MPRemoteCommandHandlerStatus TogglePlayPauseCommand(MPRemoteCommandEvent arg)
        {
            MediaManager.PlayPause();
            return MPRemoteCommandHandlerStatus.Success;
        }

        protected override void Dispose(bool disposing)
        {
            InvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.EndReceivingRemoteControlEvents();
            });

            var audioSession = AVAudioSession.SharedInstance();
            audioSession.SetActive(false);
            base.Dispose(disposing);
        }
    }
}