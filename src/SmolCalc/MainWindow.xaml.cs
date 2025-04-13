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
            string newExpression = (string)result_label.Content;
            if (clickedButton.Tag != null)
            {
                string buttonContent = (string)clickedButton.Content;
                newExpression = result_label.Content + " " + buttonContent;
                expression_label.Content = newExpression;
            }
            else
            {
                string expression = (string)result_label.Content;
                string buttonContent = (string)clickedButton.Content;
                if (expression.Length == 1 && expression == "0" && buttonContent != "0")
                {
                    newExpression = buttonContent;
                }
                else
                {
                    newExpression += buttonContent;
                    newExpression = NumberValidation(newExpression);
                }
                result_label.Content = newExpression;
            }
        }
    }
    
    private string NumberValidation(string content)
    {
        if (content.Length > 1 && content.StartsWith("0") && !content.StartsWith("0,"))
        {
            string correctNumber = "0," + content.Substring(1);
            content = content.Replace(content, correctNumber);
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