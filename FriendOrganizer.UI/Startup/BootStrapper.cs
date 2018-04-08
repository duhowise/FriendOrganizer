using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {

                var builder=new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();


            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            builder.RegisterType<View.MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();


            return builder.Build();
        }
    }
}