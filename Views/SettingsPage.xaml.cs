using MAUIFoodie.ViewModels;

namespace MAUIFoodie.Views;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _viewModel;
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Application.Current != null)
        {
            ThemeSwitch.IsToggled = Application.Current.RequestedTheme == AppTheme.Dark ||
                                   Application.Current.UserAppTheme == AppTheme.Dark;
        }
    }

    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        bool isDark = e.Value;
        if (_viewModel.ToggleThemeCommand.CanExecute(isDark))
        {
            _viewModel.ToggleThemeCommand.Execute(isDark);
        }
    }
}