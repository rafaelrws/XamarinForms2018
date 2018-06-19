using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
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
            //TODO - Validações
            string cep = CEP.Text.Trim();

            if(isValidCEP(cep))
            { 
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0} " + "\r\n" + "Bairro: {1} " + "\r\n" + "Cidade: {2} " + "\r\n" + "Estado: {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O CEP " + cep + " não foi localizado em nossa base de dados!", "OK");
                    }
                    
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }


        private bool isValidCEP(string cep)
        {
            bool valido = true;
            int NovoCEP = 0;

            if (cep.Length !=8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres! ", "OK");
                valido = false;
            }
            
            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números! ", "OK");

                valido = false;
            }

            return valido;
        }

	}
}
