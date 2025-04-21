using System.Globalization;
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
        CultureInfo nonInvariantCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = nonInvariantCulture;
    }
    
    private string currentOperator = "";
    private double? firstNumber = null;
    private bool isNewEntry = false;
    private double result = 0;
    private bool chaining = false;

    /// <summary>
    /// When a button is clicked this function processes it 
    /// for the corresponding task
    /// </summary>
    /// <param name="sender">The clicked button</param>
    /// <param name="e">Event data for the button</param>
    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        Button clickedButton = sender as Button;

        // If a button is clicked
        if (clickedButton != null)
        {
            string buttonContent = clickedButton.Content.ToString();
            string tag = clickedButton.Tag.ToString();

            // This if else block reads the tag and acts corresponding to them
            if (tag == "num")
            {
                string content = result_label.Content.ToString();

                // If it's a new entry
                if (isNewEntry)
                {
                    content = buttonContent;
                }
                else if (content == "0" || content == "-0")
                {
                    // Replace the 0 with the new number
                    content = (content.StartsWith("-") ? "-" : "") + buttonContent;
                }
                else
                {
                    // Append the new digit to the current number
                    content += buttonContent;
                }

                result_label.Content = content;
                isNewEntry = false;
            }
            else if (tag == "dot" && currentOperator != "xⁿ")
            {
                string content = result_label.Content.ToString();

                // If its a new entry or there werent any number inputs yet
                if (isNewEntry)
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
            // If there werent any inputs yet or it changes the number to negative if it shouldnt be an operator
            else if (buttonContent == "-" && isNewEntry && (!firstNumber.HasValue || currentOperator != "") && !chaining)
            {
                result_label.Content = "-0";
                isNewEntry = false;
            }
            else if (tag == "op")
            {
                // Check if there was an error last time
                if (!double.TryParse(result_label.Content.ToString(), out double secondNumber))
                {
                    return;
                }

                // If we want to chain calculations then changes the first number to the last result
                if (isNewEntry)
                {
                    if (currentOperator != "")
                    {
                        // If in the chaining an error occurs
                        if (OperationValidation(secondNumber, currentOperator))
                        {
                            return;
                        }
                        firstNumber = Calculate(firstNumber.Value, secondNumber, currentOperator);
                        if (firstNumber == -0)
                        {
                            firstNumber = 0;
                        }
                        chaining = true;
                    }
                    else
                    {
                        firstNumber = result;
                    }
                }
                else
                {
                    // If we want to chain operations then this will calculate the previous and do print out the next one
                    if (currentOperator != "")
                    {
                        // If in the chaining an error occurs
                        if (OperationValidation(secondNumber, currentOperator))
                        {
                            return;
                        }
                        firstNumber = Calculate(firstNumber.Value, secondNumber, currentOperator);
                        if (firstNumber == -0)
                        {
                            firstNumber = 0;
                        }
                        chaining = true;
                    }
                    else
                    {
                        firstNumber = secondNumber;
                    }
                }

                currentOperator = buttonContent;

                // If else block for special operators that need different expressions
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
                    expression_label.Content = $"{firstNumber} {currentOperator}";
                }
                isNewEntry = true;
            }
            else if (tag == "equals")
            {
                // If we have two numbers and an operator
                if (firstNumber.HasValue && !string.IsNullOrEmpty(currentOperator))
                {
                    double secondNumber = double.Parse(result_label.Content.ToString());

                    // Validate operations like dividing by zero
                    if (OperationValidation(secondNumber, currentOperator))
                    {
                        return;
                    }

                    // If the operation is valid, calculate the result
                    result = Calculate(firstNumber.Value, secondNumber, currentOperator);

                    // For the UI change the -0 value to 0
                    if (result == -0)
                    {
                        result = 0;
                    }

                    result_label.Content = result.ToString();

                    // If else block for special operators that need different expressions
                    if (currentOperator == "xⁿ")
                    {
                        expression_label.Content += $" {secondNumber})";
                    }
                    else if (currentOperator == "ⁿ√")
                    {
                        expression_label.Content += $" {secondNumber})";
                    }
                    else
                    {
                        expression_label.Content = $"{firstNumber} {currentOperator} {secondNumber} =";
                    }

                    firstNumber = result;
                    currentOperator = "";
                    isNewEntry = true;
                    chaining = false;
                }
            }
            else if (tag == "opSpecial")
            {
                // Check if there was an error last time
                if(!double.TryParse(result_label.Content.ToString(), out double value))
                {
                    return;
                }


                // Validate if there are invalid operations like negative factorials
                if (OperationValidation(value, buttonContent))
                {
                    return;
                }

                // If the operatorin is valid calculate the result
                result = CalculateSpecial(value, buttonContent);

                // If we want chain with special operators
                if (firstNumber != null)
                {
                    result = Calculate(firstNumber.Value, result, currentOperator);
                    expression_label.Content = $"{SpecialOperatorLabel(value, buttonContent)}";
                }
                else
                {
                    expression_label.Content = $"{SpecialOperatorLabel(value, buttonContent)}";
                }

                result_label.Content = result.ToString();

                firstNumber = result;
                currentOperator = "";
                isNewEntry = true;
            }
            else if (tag == "Clr")
            {
                // Reset the calculator to initial state
                result_label.Content = "0";
                expression_label.Content = "";
                firstNumber = null;
                currentOperator = "";
                isNewEntry = true;
                chaining = false;
                result = 0;
            }
            else if (tag == "Del")
            {
                string content = result_label.Content.ToString();

                // If the number contains more than one character
                if (!isNewEntry && content.Length > 1)
                {
                    // And if it is a negative number with 1 number
                    if (content.Length == 2 && content.StartsWith("-"))
                    {
                        // Then rewrite it to zero
                        content = "0";
                    }
                    else
                    {
                        // If not take away one number from the end
                        content = content.Substring(0, content.Length - 1);
                    }
                }
                else
                {
                    // If it's 1 character or less, set it to 0
                    content = "0";
                }

                result_label.Content = content;
            }
        }
    }
    
    /// <summary>
    /// This function checks if there are invalid operations for
    /// deviding, factorials and logarithm
    /// </summary>
    /// <param name="value">Number which we want to work with</param>
    /// <param name="op">Which operation we are trying to calculate</param>
    /// <returns>Returns true if the operation is invalid and false if its valid</returns>
    private bool OperationValidation(double value, string op)
    {
        if (op == "/" && value == 0)
        {
            expression_label.Content = $"{firstNumber} / 0 =";
            result_label.Content = "Cannot divide by zero";
            firstNumber = null;
            currentOperator = "";
            isNewEntry = true;
            result = 0;
            return true;
        }
        else if (op == "!" && (value < 0 || value > 22 || value != (int)value))
        {
            expression_label.Content = SpecialOperatorLabel(value, op);
            result_label.Content = "Invalid";
            isNewEntry = true;
            firstNumber = null;
            result = 0;

            return true;
        }
        else if (op == "ln" && value <= 0)
        {
            expression_label.Content = SpecialOperatorLabel(value, op);
            result_label.Content = "Invalid";
            isNewEntry = true;
            firstNumber = null;
            result = 0;
            return true;
        }
        return false;
    }

    /// <summary>
    /// This function creates a label for special operators
    /// </summary>
    /// <param name="value">Number which we are working with</param>
    /// <param name="op">The special operator</param>
    /// <returns>String with the correct expression</returns>
    private string SpecialOperatorLabel(double value, string op)
    {
        return op switch
        {
            "!" => $"fac({value}) =",
            "ln" => $"ln({value}) =",
            _ => value.ToString()
        };
    }

    /// <summary>
    /// This function calculates operations that need only one number
    /// </summary>
    /// <param name="number">Number which we are working with</param>
    /// <param name="op">The special operator</param>
    /// <returns>Result of the calculation</returns>
    private double CalculateSpecial(double number, string op)
    {
        return op switch
        {
            "!" => MathLib.Fac((int)number),
            "ln" => MathLib.Log(number),
            _ => number
        };
    }

    /// <summary>
    /// This function calculates operations that need two numbers
    /// </summary>
    /// <param name="firstNumber">First operand</param>
    /// <param name="secondNumber">Second operand</param>
    /// <param name="op">The operator</param>
    /// <returns>Result of the calculation</returns>
    private double Calculate(double firstNumber, double secondNumber, string op)
    {
        return op switch
        {
            "+" => MathLib.Add(firstNumber, secondNumber),
            "-" => MathLib.Sub(firstNumber, secondNumber),
            "×" => MathLib.Mul(firstNumber, secondNumber),
            "/" => MathLib.Div(firstNumber, secondNumber),
            "xⁿ" => MathLib.Pow(firstNumber, (int)secondNumber),

            // If the root is 0th, automatically set it to square root
            "ⁿ√" => secondNumber == 0 ? MathLib.Root(firstNumber, 2) : MathLib.Root(firstNumber, (int)secondNumber),
            _ => secondNumber
        };
    }
}