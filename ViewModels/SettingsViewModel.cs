using MAUIFoodie.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace MAUIFoodie.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ThemeService _themeService;
        private readonly AudioServices _audioService;

        public ICommand ToggleThemeCommand { get; }
        public ICommand ChangeLanguageCommand { get; }

        public SettingsViewModel(ThemeService themeService, AudioServices audioService)
        {
            _themeService = themeService;
            _audioService = audioService;

            ToggleThemeCommand = new Command<bool>(OnToggleTheme);
            ChangeLanguageCommand = new Command<string>(OnChangeLanguage);
        }

        private async void OnToggleTheme(bool isDark)
        {
            await _audioService.PlayClickSoundAsync();
            _themeService.SetTheme(isDark ? AppTheme.Dark : AppTheme.Light);
        }

        private async void OnChangeLanguage(string langCode)
        {
            await _audioService.PlayClickSoundAsync();

            var culture = new CultureInfo(langCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}
