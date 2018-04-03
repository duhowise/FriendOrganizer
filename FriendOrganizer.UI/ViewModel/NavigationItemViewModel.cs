namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel:ViewModelBase
    {
        private string _displayMember;

       

        public NavigationItemViewModel(int id,string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public int Id { get; }

        public string DisplayMember
        {
            get => _displayMember;
            set
            {
                if (value == _displayMember) return;
                _displayMember = value;
                OnPropertyChanged();
            }
        }
    }
}