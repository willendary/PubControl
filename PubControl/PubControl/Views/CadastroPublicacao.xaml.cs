using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PubControl.ViewModels;

namespace PubControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPublicacao : ContentPage
    {
        public CadastroPublicacao()
        {
            InitializeComponent();
            BindingContext = new CadastroPublicacaoViewModel();
        }
        protected override async void OnAppearing()
        {
            var vm = (CadastroPublicacaoViewModel)BindingContext;

            if (vm.Id == 0)
            {
                vm.NovaPublicacao.Execute(null);
            }
        }
    }
}