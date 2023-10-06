using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using PubControl.Models;
using Xamarin.Forms;

namespace PubControl.ViewModels
{
    class ListaPublicacoesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /**
         * Pegando o que foi digitado pelo usuário
         * */
        public string ParametroBusca { get; set; }

        /**
         * Gerencia se mostra ao usuário o RefreshView
         * */
        bool estaAtualizando = false;
        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }

        ObservableCollection<Publicacao> listaPublicacoes = new ObservableCollection<Publicacao>();

        public ObservableCollection<Publicacao> ListaPublicacoes
        {
            get => listaPublicacoes;
            set => listaPublicacoes = value;
        }
        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;

                        List<Publicacao> tmp = await App.Database.GetAllRows();
                        ListaPublicacoes.Clear();
                        tmp.ForEach(i => ListaPublicacoes.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }
        public ICommand Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;

                        List<Publicacao> tmp = await App.Database.Search(ParametroBusca);
                        ListaPublicacoes.Clear();
                        tmp.ForEach(i => ListaPublicacoes.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }
        public ICommand AbrirDetalhesPublicacao
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroPublicacao?parametro_id={id}");      
                });
            }
        }
        public ICommand Remover
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        bool confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmar?", "Excluir", "Sim", "Não");

                        if (confirmacao)
                        {
                            await App.Database.Delete(id);
                            AtualizarLista.Execute(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }
    }
}
