using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_de_IMC
{
    public partial class frmIMC : Form
    {
        public frmIMC()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtPeso.Clear();
            txtAltura.Clear();
            txtNome.Focus();
        }
        private string ValidarCampos()
        {
            string msgErro = string.Empty;

            if(txtNome.Text == string.Empty )
            {
                msgErro += "Campo NOME em branco!\n";
            }
            if(txtPeso.Text == string.Empty )
            {
                msgErro += "Campo Peso em branco!\n";
            }
            else
            {
                decimal aux = decimal.Parse(txtPeso.Text);
                if (aux < 1.2m || aux > 2.5m)
                {
                    msgErro += "Altura Inválida! Deve estar entre 1,20m e 2,50m!\n";
                }
            }
            if (txtAltura.Text == string.Empty)
            {
                msgErro += "Campo Altura em branco!\n";
            }
            else
            {
                decimal aux = decimal.Parse(txtPeso.Text);
                if (aux < 40 || aux > 250)
                {
                    msgErro += "Peso Inválida! Deve estar entre 40kg e 250kg!\n";
                }
            }

            return msgErro;
        }
        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SomenteNumeros(e.KeyChar, txtAltura.Text, ",");
        }
        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SomenteNumeros(e.KeyChar, txtAltura.Text, ",");
        }
        private bool SomenteNumeros(char tecla, string texto, string excecao)
        {
            if ((!char.IsDigit(tecla) && tecla != (char)Keys.Back
                && excecao.IndexOf(tecla) == -1)
                || (excecao.IndexOf(tecla) != -1 &&
                texto.IndexOf(excecao) != -1))
            {
                return true;
            }
            return false;
        }
        private decimal CalcularIMC(decimal p, decimal a)
        {
            decimal resultado = p / (decimal)Math.Pow((double)a, 2);

            return resultado;
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            string mensagem = ValidarCampos();
            if (mensagem != string.Empty)
            {
                MessageBox.Show(mensagem, "Erro de Preenchimento!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal peso = decimal.Parse(txtPeso.Text);
            decimal altura = decimal.Parse(txtAltura.Text);
            decimal imc = CalcularIMC(peso, altura);
            string nome = txtNome.Text;
            mensagem = RetornarMensagem(nome, imc);

            MessageBox.Show(mensagem, "Cálculo IMC!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimparCampos();
        }
        private string RetornarMensagem(string nm, decimal result)
        {
            string msg = nm + " Seu IMC calculado é " + result.ToString("#0.0")+"\n";
            msg += "Você está ";

            if (result < 17)
            {
                msg += "muito abaixo do peso.";
            }
            else if (result < 18.5m)
            {
                msg += "abaixo do peso.";
            }
            else if (result < 25)
            {
                msg += "com o peso normal.";
            }
            else if (result < 30)
            {
                msg += "acima do peso.";
            }
            else if (result < 35)
            {
                msg += "com obesidade grau I.";
            }
            else if (result < 40)
            {
                msg += "com obesidade grau II.";
            }
            else
            {
                msg += "com obesidade grau III.";
            }

            return msg;
        }
    }
}
