using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PubControl.ViewModels
{
    class CadastroPublicacaoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        string descricao, codigoReferencia, observacao;
        int id, tipoPublicacao;
        DateTime data;
        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Descricao"));
            } 
        }
        public string CodigoReferencia
        {
            get => codigoReferencia;
            set
            {
                codigoReferencia = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CodigoReferencia"));
            }
        }
        public string Observacao
        {
            get => observacao;
            set
            {
                observacao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Observacao"));
            }
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
        public int TipoPublicacao
        {
            get => tipoPublicacao;
            set
            {
                tipoPublicacao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TipoPublicacao"));
            }
        }
        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }



        public ICommand NovaPublicacao
        {
            get => new Command(() =>
            {
                Id = 0;
                Descricao = string.Empty;
                CodigoReferencia = string.Empty;
                TipoPublicacao = 0;
                Data = DateTime.Now;
                Observacao = string.Empty;
            });
        }
        public ICommand SalvarPublicacao
        {
            get => new Command(() =>
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Você clicou", "OK");
            });
        }        
    }
}
