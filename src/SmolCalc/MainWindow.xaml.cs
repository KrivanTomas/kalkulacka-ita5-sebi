using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmolMathLib;

namespace SmolCalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private string currentOperator = "";
    private double? firstNumber = null;
    private bool isNewEntry = false;
    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        Button clickedButton = sender as Button;
        if (clickedButton != null)
        {
            string buttonContent = clickedButton.Content.ToString();
            string tag = clickedButton.Tag?.ToString();

            if (tag == "num")
            {
                string content = result_label.Content.ToString();
                if (isNewEntry || content == "0" || content == "-0")
                {
                    content = (content.StartsWith("-") ? "-" : "") + buttonContent;
                }
                else
                {
                    content += buttonContent;
                }

                result_label.Content = content;
                isNewEntry = false;
            }
            else if (tag == "dot")
            {
                string content = result_label.Content.ToString();
                if (isNewEntry || string.IsNullOrEmpty(content))
                {
                    content = "0.";
                }
                else if (!content.Contains("."))
                {
                    content += ".";
                }

                result_label.Content = content;
                isNewEntry = false;
            }
            else if (buttonContent == "-" && (isNewEntry || result_label.Content.ToString() == "0"))
            {
                string content = "0";
                if (!content.StartsWith("-"))
                {
                    result_label.Content = "-" + content;
                }
            }
            else if (tag == "op")
            {
                firstNumber = double.Parse(result_label.Content.ToString());
                currentOperator = buttonContent;
                if (currentOperator == "xⁿ")
                {
                    expression_label.Content = $"pow({firstNumber},";
                }
                else if (currentOperator == "ⁿ√")
                {
                    expression_label.Content = $"root({firstNumber},";
                }
                else
                {
                    expression_label.Content = $"{firstNumber} {buttonContent}";
                }

                isNewEntry = true;
            }
            else if (tag == "equals")
            {
                if (firstNumber.HasValue && !string.IsNullOrEmpty(currentOperator))
                {
                    double secondNumber = double.Parse(result_label.Content.ToString());
                    double result = Calculate(firstNumber.Value, secondNumber, currentOperator);
                    result_label.Content = result.ToString();
                    if (currentOperator == "xⁿ")
                    {
                        expression_label.Content += $"{secondNumber})";
                    }
                    else if (currentOperator == "ⁿ√")
                    {
                        expression_label.Content += $"{secondNumber})";
                    }
                    else
                    {
                        expression_label.Content = $"{firstNumber} {currentOperator} {secondNumber} =";
                    }
                    firstNumber = result;
                    currentOperator = "";
                    isNewEntry = true;
                }
            }
            else if (tag == "opSpecial")
            {
                double value = double.Parse(result_label.Content.ToString());
                double result = CalculateSpecial(value, buttonContent);
                result_label.Content = result.ToString();
                expression_label.Content = SpecialOperatorLabel(value, buttonContent);
                isNewEntry = true;
            }
            else if (tag == "Clr")
            {
                result_label.Content = "0";
                expression_label.Content = "";
                firstNumber = null;
                currentOperator = "";
                isNewEntry = true;
            }
            else if (tag == "Del")
            {
                string content = result_label.Content.ToString();
                if (!isNewEntry && content.Length > 1)
                {
                    if (content.Length == 2 && content.StartsWith("-"))
                    {
                        content = "0";
                    }
                    else
                    {
                        content = content.Substring(0, content.Length - 1);
                    }
                }
                else
                {
                    content = "0";
                }

                result_label.Content = content;
            }
        }
    }
    
    private string SpecialOperatorLabel(double value, string op)
    {
        return op switch
        {
            "!" => $"fac({value})",
            "ln" => $"ln({value})",
            _ => value.ToString()
        };
    }

    private double CalculateSpecial(double number, string op)
    {
        return op switch
        {
            "!" => MathLib.Fac((int)number),
            "ln" => MathLib.Log(number),
            _ => number
        };
    }
    private double Calculate(double firstNumber, double secondNumber, string op)
    {
        return op switch
        {
            "+" => MathLib.Add(firstNumber, secondNumber),
            "-" => MathLib.Sub(firstNumber, secondNumber),
            "×" => MathLib.Mul(firstNumber, secondNumber),
            "/" => MathLib.Div(firstNumber, secondNumber),
            "xⁿ" => MathLib.Pow(firstNumber, (int)secondNumber),
            "ⁿ√" => secondNumber == 0 ? MathLib.Root(firstNumber, 2) : MathLib.Root(firstNumber, (int)secondNumber),
            _ => secondNumber
        };
    }
}