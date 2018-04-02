using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get;  }

        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;
        }
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
       

        
    }
}