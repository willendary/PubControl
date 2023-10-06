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
    public partial class ListaPublicacoes : ContentPage
    {
        public ListaPublicacoes()
        {
            BindingContext = new ListaPublicacoesViewModel();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var vm = (ListaPublicacoesViewModel)BindingContext;
            vm.AtualizarLista.Execute(null);
        }
    }
}