using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using Button = System.Windows.Controls.Button;

using Excel = Microsoft.Office.Interop.Excel;

namespace calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //variables generales de la aplicación
        private double numero1 = 0.0;

        private double numero2 = 0.0;
        private double resultado;
        private string operacion = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        /*metodo para recoger el valor de los botones numericos e
        insertarlo en pantalla*/

        private void btns_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.Content is string buttonText)
            {
                if (double.TryParse(buttonText, out double valor))
                {
                    if (operacion == "")
                    {
                        numero1 = (numero1 * 10.0) + valor;
                       
                        textoResultado.Text += valor;
                        textOperaciones.Text += buttonText;
                    }
                    else
                    {
                        numero2 = (numero2 * 10.0) + valor;

                        textoResultado.Text = textoResultado.Text + valor;
                        textOperaciones.Text += buttonText;
                    }
                }
            }
        }

        //metodo para comprobar si el numero lelva coma
        private void coma(object sender, RoutedEventArgs e)
        {
            if (!textoResultado.Text.Contains(","))
            {
                textoResultado.Text += ",";
                textOperaciones.Text += ",";
            }
        }

        /*metodo para recoger el valor de los botones de operaciones e
        insertarlo en pantalla*/

        private void operaciones_Click(object sender, RoutedEventArgs e)
        {
            if (numero1 != 0 && numero2 != 0)
            {
                btnResultado_Click(sender, e);
            }
            if (double.TryParse(textoResultado.Text, out numero1))
            {
                if (sender is Button button && button.Content is string buttonText)
                {
                    operacion = buttonText;
                    textoResultado.Text = "";
                    textOperaciones.Text += " " + operacion + " ";
                    if (textOperaciones.Text.Contains("="))
                    {
                        textOperaciones.Text = textOperaciones.Text.Replace("=", "");
                    }
                }
            }
        }

        //metodo para hacer las operaciones
        private void btnResultado_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textoResultado.Text, out numero2))

            {
                switch (operacion)
                {
                    case "+":
                        resultado = numero1 + numero2;
                        break;

                    case "-":
                        resultado = numero1 - numero2;
                        break;

                    case "x":
                        resultado = numero1 * numero2;
                        break;

                    case "÷":
                        resultado = numero1 / numero2;
                        break;

                }
                numero1 = 0;
                numero2 = 0;

                if (!textOperaciones.Text.Contains("="))
                {
                    textOperaciones.Text += " =";
                    textoResultado.Text = resultado.ToString();
                }
            }
        }

        private void btnPorcentaje_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textoResultado.Text, out double valor))
            {
                double porcentaje = valor / 100.0;

                // Si hay una operación pendiente, aplica el porcentaje al número1
                if (!string.IsNullOrEmpty(operacion))
                {
                    switch (operacion)
                    {
                        case "+":
                            numero2 = numero1 * porcentaje;
                            break;
                        case "-":
                            numero2 = numero1 * porcentaje;
                            break;
                        case "x":
                            numero2 = numero1 * porcentaje;
                            break;
                        case "÷":
                            numero2 = numero1 * porcentaje;
                            break;
                    }

                    resultado = numero1 + numero2;
                    numero1 = 0;
                    operacion = "";
                }
                //else
                //{
                //    // Si no hay una operación pendiente, aplica el porcentaje al número actual
                //    resultado = porcentaje;
                //}

                // Actualiza la pantalla con el resultado del porcentaje
                textoResultado.Text = resultado.ToString();
                textOperaciones.Text = $"{valor} % ";
            }
        }

        //metodo para crear el valor de la pantalla en negativo o positivo
        private void btnPositivoNegativo_Click(object sender, RoutedEventArgs e)
        {
            textoResultado.Text = (-1 * int.Parse(textoResultado.Text)).ToString();
        }

        //metodo para borrar las operciones y empezar de nuevo a operar
        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            if (operacion == "")
            {
                numero1 = 0.00;
            }
            else
            {
                operacion = "";
                numero1 = 0.00;
                numero2 = 0.00;
                
            }
            textoResultado.Text = "";
            textOperaciones.Text = "";
        }

    }
}