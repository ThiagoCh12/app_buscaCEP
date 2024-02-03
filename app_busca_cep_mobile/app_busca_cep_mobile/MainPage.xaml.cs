using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using app_busca_cep_mobile.Servico;
using app_busca_cep_mobile.Servico.Modelo;

namespace app_busca_cep_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btCEP.Clicked += BuscarCEP;

        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            string cep = CEP.Text.Trim();
            if (isValidCep(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);
                    if (end != null)
                    {
                        lblResultado.Text = string.Format("Endereço: {2}, {3}, {0} {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                        CEP.Text = "";
                    }
                    else
                    {
                        DisplayAlert("Erro", "O CEP" + cep + "nao foi encontrado", "Ok");
                    }
                }
                catch (Exception er)
                {

                    DisplayAlert("Erro crítico", er.Message, "Ok");
                }
            }
        }

        private bool isValidCep (string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                DisplayAlert("Erro", "Cep inválido! O CEP contem apenas 8 números.", "Ok");
                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP contem apenas numeros", "Ok");
                valido = false;
            }
            return valido;
        }

    }
}
