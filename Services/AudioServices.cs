using System;
using System.Collections.Generic;
using System.Text;

namespace MAUIFoodie.Services
{
    public class AudioServices
    {
        private readonly IAudioManager _audioManager;

        public AudioServices(IAudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public async Task PlayClickSoundAsync()
        {
            try
            {
                var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("click.mp3"));
                player.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Sound didn't play: {ex.Message}");
            }
        }
    }
}
