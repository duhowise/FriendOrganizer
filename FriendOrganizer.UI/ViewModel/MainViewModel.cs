﻿using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly IFriendDataService _friendDataService;

        public MainViewModel(IFriendDataService friendDataService)
        {
            _friendDataService = friendDataService;
            Friends=new ObservableCollection<Friend>();
        }
        public async Task LoadAsync()
        {
            var friends = await _friendDataService.GetAllAsync();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
        public ObservableCollection<Friend> Friends { get; set; }
        private Friend _selectedFriend;

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
            }
        }

        
    }
}