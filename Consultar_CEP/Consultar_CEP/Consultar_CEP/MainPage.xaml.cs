using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Consultar_CEP.Servico;
using Consultar_CEP.Servico.Modelo;

namespace Consultar_CEP
{
    
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            BOTAO.Clicked += BuscarCEP;

        }

        private void BuscarCEP(object sender, EventArgs args) 
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0}, {1} {2} ", end.localidade, end.uf, end.logradouro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço nao foi encontrado para p CEP informado " + end, "OK");
                    }
                    
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRITICO", e.Message, "OK");
                }
            }
            
        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                
                DisplayAlert("ERRO", "CEP Invalido! O CEP deve conter 8 caracteres ", "OK");

                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {

                DisplayAlert("ERRO", "CEP Invalido! O CEP deve ser composto apenas por numeros", "OK");

                valido = false;
            }


            return valido;
        }
    }
}
