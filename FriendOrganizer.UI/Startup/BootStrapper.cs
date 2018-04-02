using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {

                var builder=new ContainerBuilder();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            builder.RegisterType<View.MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<FriendDataService>().As<IFriendDataService>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();


            return builder.Build();
        }
    }
}