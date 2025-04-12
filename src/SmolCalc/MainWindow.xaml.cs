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

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        Button clickedButton = sender as Button;
        if (clickedButton != null)
        {
            string newExpression = (string)expression_label.Content;
            if (clickedButton.Tag != null)
            {
                string funcExpression = (string)clickedButton.Tag;
                newExpression += operators_[funcExpression];
                expression_label.Content = OperatorValidation(newExpression);
            }
            else
            {
                string buttonContent = (string)clickedButton.Content;
                newExpression += buttonContent;
                expression_label.Content = NumberValidation(newExpression);
            }
        }
    }

    private string OperatorValidation(string content)
    {
        if (string.IsNullOrEmpty(content))
            return content;

        string contentCleared = "";

        foreach (char value in content)
        {
            if (char.IsDigit(value) || value == ',' || value == '-')
                contentCleared += ";";
            else
                contentCleared += value.ToString();
        }

        string lastOperator = contentCleared.Split(';').LastOrDefault() ?? "";

        if(lastOperator.Length > 1)
        {
            string correctOperator = lastOperator[0].ToString();
            content = content.Replace(lastOperator, correctOperator);
        }

        return content;
    }
    private string NumberValidation(string content)
    {
        if (string.IsNullOrEmpty(content))
            return content;

        string contentCleared = content;

        foreach (var (key, expr) in operators_)
        {
            contentCleared = contentCleared.Replace(expr, ";");
        }

        string lastNumber = contentCleared.Split(';').LastOrDefault();

        if (lastNumber.Length > 1 && lastNumber.StartsWith("0") && !lastNumber.StartsWith("0,"))
        {
            string correctNumber = "0," + lastNumber.Substring(1);
            content = content.Replace(lastNumber, correctNumber);
        }

        return content;
    }

    private Dictionary<string, string> operators_ = new Dictionary<string, string>() 
    {
        { "add", "+" },
        { "sub", "-" },
        { "mult", "×" },
        { "div", "/" },
    }; 
}