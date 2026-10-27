using MAUIFoodie.ViewModels;
using Microsoft.Maui.Layouts;
using MAUIFoodie.ViewModels;

namespace MAUIFoodie
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadFoodItemsAsync();
        }

        private void OnMoveUpClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is MAUIFoodie.Models.Food foodItem)
            {
                _viewModel.MoveUpCommand.Execute(foodItem);
            }
        }

        private void OnMoveDownClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is MAUIFoodie.Models.Food foodItem)
            {
                _viewModel.MoveDownCommand.Execute(foodItem);
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is MAUIFoodie.Models.Food foodItem)
            {
                _viewModel.DeleteFoodCommand.Execute(foodItem);
            }
        }
    }
}
