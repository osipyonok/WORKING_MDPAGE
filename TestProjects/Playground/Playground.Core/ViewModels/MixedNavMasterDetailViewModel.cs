using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.ViewModels;
using Xamarin.Forms;

namespace Playground.Core.ViewModels
{
    public class MixedNavMasterDetailViewModel : MvxMasterDetailViewModel<MixedNavMasterRootContentViewModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private MenuItem _menuItem;
        private IMvxAsyncCommand<MenuItem> _onSelectedChangedCommand;

        public class MenuItem
        {
            public string Title { get; set; }

            public string Description { get; set; }
            public Type ViewModelType { get; set; }

        }

        public MixedNavMasterDetailViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Menu = new[] {
                new MenuItem { Title = "Root", Description = "The root page", ViewModelType = typeof(MixedNavMasterRootContentViewModel) },
                new MenuItem { Title = "Tabs", Description = "Tabbed detail page", ViewModelType = typeof(MixedNavTabsViewModel)},
            };
        }

        public IEnumerable<MenuItem> Menu { get; set; }

        public MenuItem SelectedMenu {
            get => _menuItem;
            set {
                if (SetProperty(ref _menuItem, value))
                    OnSelectedChangedCommand.Execute(value);
            }
        }

        private IMvxAsyncCommand<MenuItem> OnSelectedChangedCommand {
            get {
                return _onSelectedChangedCommand ?? (_onSelectedChangedCommand = new MvxAsyncCommand<MenuItem>(async (item) => 
                {
                    if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
                    {
                        masterDetailPage.IsPresented = false;
                    }
                    else if (Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
                    {
                        nestedMasterDetail.IsPresented = false;
                    }

                    if (item == null)
                        return;

                    var vmType = item.ViewModelType;

                    await _navigationService.Navigate(vmType);
                }));
            }
        }
    }
}
