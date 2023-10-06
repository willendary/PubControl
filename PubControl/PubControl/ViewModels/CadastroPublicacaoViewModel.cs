using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PubControl.Models;

namespace PubControl.ViewModels
{
    [QueryProperty("PegarIdDaNavegacao", "parametro_id")]
    class CadastroPublicacaoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        string descricao, codigoReferencia, observacao;
        int id, tipoPublicacao;
        DateTime data;

        public string PegarIdDaNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));
                VerPublicacao.Execute(id_parametro);
            }
        }
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
            get => new Command(async () =>
            {
                try
                {
                    Publicacao model = new Publicacao()
                    {
                        Descricao = this.Descricao,
                        CodigoReferencia = this.CodigoReferencia,
                        Data = this.Data,
                        IdTipoPublicacao = this.TipoPublicacao,
                        Observacao = this.Observacao
                    };
                    if (this.Id == 0)
                    {
                        await App.Database.Insert(model);
                    }
                    else
                    {
                        model.Id = this.Id;
                        await App.Database.Update(model);
                    }
                    await Application.Current.MainPage.DisplayAlert("Tudo certo", "Registro salvo", "OK");
                    await Shell.Current.GoToAsync("//ListaPublicacoes");
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }

            });
        }
        public ICommand VerPublicacao
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    Publicacao model = await App.Database.GetById(id);
                    this.Id = model.Id;
                    this.Descricao = model.Descricao;
                    this.CodigoReferencia = model.CodigoReferencia;
                    this.TipoPublicacao = model.IdTipoPublicacao;
                    this.Data = model.Data;
                    this.Observacao = model.Observacao;

                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });

        }
    }
        
}
