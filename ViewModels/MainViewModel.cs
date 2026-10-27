using MAUIFoodie.Models;
using MAUIFoodie.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MAUIFoodie.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly DBServices? _databaseService;
        private readonly AudioServices? _audioService;

        private string? _newFoodName;
        private string? _newFoodCalories;

        public ObservableCollection<Food> FoodItems { get; } = new();
        public ObservableCollection<PlacedFoodIcon> PlacedIcons { get; } = new();

        public string NewFoodName
        {
            get => _newFoodName;
            set => SetProperty(ref _newFoodName, value);
        }

        public string NewFoodCalories
        {
            get => _newFoodCalories;
            set => SetProperty(ref _newFoodCalories, value);
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddFoodCommand { get; }
        public ICommand DeleteFoodCommand { get; }
        public ICommand PlaceIconCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public MainViewModel(DBServices databaseService, AudioServices audioService)
        {
            _databaseService = databaseService;
            _audioService = audioService;

            LoadDataCommand = new Command(async () => await LoadFoodItemsAsync());
            AddFoodCommand = new Command(async () => await AddFoodItemAsync());
            DeleteFoodCommand = new Command<Food>(async (item) => await DeleteFoodItemAsync(item));
            MoveUpCommand = new Command<Food>(MoveItemUp);
            MoveDownCommand = new Command<Food>(MoveItemDown);
            OpenSettingsCommand = new Command(async () => await OnOpenSettingsAsync());
        }

        public async Task LoadFoodItemsAsync()
        {
            var items = await _databaseService.GetFoodItemsAsync();
            FoodItems.Clear();
            foreach (var item in items)
            {
                FoodItems.Add(item);
            }
        }

        private async Task AddFoodItemAsync()
        {
            if (string.IsNullOrWhiteSpace(NewFoodName) || !int.TryParse(NewFoodCalories, out int calories))
            {
                return;
            }

            var newItem = new Food
            {
                Name = NewFoodName,
                Calories = calories,
                DateAdded = DateTime.Now
            };

            await _databaseService.SaveFoodItemAsync(newItem);
            FoodItems.Add(newItem);

            await _audioService.PlayClickSoundAsync();

            NewFoodName = string.Empty;
            NewFoodCalories = string.Empty;
        }

        private async Task DeleteFoodItemAsync(Food item)
        {
            if (item == null) return;

            await _databaseService.DeleteFoodItemAsync(item);
            FoodItems.Remove(item);
            await _audioService.PlayClickSoundAsync();
        }
        
        public class PlacedFoodIcon
        {
            public double X { get; set; }
            public double Y { get; set; }
            public string? ImageSource { get; set; }
        }
        private void MoveItemUp(Food item)
        {
            if (item == null) return;

            int index = FoodItems.IndexOf(item);
            if (index > 0)
            {
                FoodItems.Move(index, index - 1);
            }
        }

        private void MoveItemDown(Food item)
        {
            if (item == null) return;

            int index = FoodItems.IndexOf(item);
            if (index != -1 && index < FoodItems.Count - 1)
            {
                FoodItems.Move(index, index + 1);
            }
        }
        private async Task OnOpenSettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }
    }
}
