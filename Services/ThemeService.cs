using System;
using System.Collections.Generic;
using System.Text;

namespace MAUIFoodie.Services
{
    public class ThemeService
    {
        private const string ThemeKey = "AppTheme";

        public void SetTheme(AppTheme theme)
        {
            Preferences.Set(ThemeKey, (int)theme);
            ApplyTheme(theme);
        }

        public void LoadAndApplyTheme()
        {
            if (Preferences.ContainsKey(ThemeKey))
            {
                var savedTheme = (AppTheme)Preferences.Get(ThemeKey, (int)AppTheme.Unspecified);
                ApplyTheme(savedTheme);
            }
        }

        private void ApplyTheme(AppTheme theme)
        {
            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = theme;
            }
        }
    }
}
