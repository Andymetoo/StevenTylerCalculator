using System;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI InputText;
    private double _result;
    private double _input;
    private double _input2;
    private string _operation;
    private string _currentInput;
    private bool _equalIsPressed;
    #endregion Fields
    
    #region Methods
    public void ClickNumber(int val)
    {
        Debug.Log($" check val: {val}");
        if (!string.IsNullOrEmpty(_currentInput))
        {
            if (_currentInput.Length < 10)
            {
                _currentInput += val;
            }
        }
        else
        {
            _currentInput = val.ToString();
        }
        InputText.text = $"{_currentInput}";
    }
    
    public void ClickOperation(string val)
    {
        Debug.Log($"ClickOperation val: {val}");
        if (!string.IsNullOrEmpty(_currentInput))
        {
            SetCurrentInput(); // Parse and store the current input
        }

        if (_input != 0 && !string.IsNullOrEmpty(_operation) && _input2 != 0)
        {
            // Perform the pending calculation if there's an ongoing operation
            Calculate();
            // Prepare for the next operation
            _input = _result; // Use the result as the first input for the next operation
            _input2 = 0; // Reset _input2 for the next input number
            _currentInput = ""; // Clear current input to accept new number
        }

        _operation = val; // Set the new operation
    }


    public void ClickDecimal()
    {
        if (!_currentInput.Contains("."))
        {
            if (string.IsNullOrEmpty(_currentInput))
            {
                _currentInput = "0.";
            }
            else
            {
                _currentInput += ".";
            }
        }

        InputText.text = _currentInput;
    }

    public void ClickEqual(string val)
    {
        Debug.Log($"ClickEqual val: {val}");
        if (!string.IsNullOrEmpty(_currentInput))
        {
            SetCurrentInput(); // Ensure the second number is set
        }

        Calculate(); // Perform the calculation

        _input = _result; // Ready _input for a new calculation or continuation
        _input2 = 0; // Reset _input2 for safety
        _operation = ""; // Clear the operation as calculation is done
        _currentInput = _result.ToString(); // Optionally, allow chaining operations on the result
        _equalIsPressed = true; // Reset the flag if you're using it elsewhere
    }

    private void Calculate()
    {
        switch (_operation)
        {
            case "+":
                _result = _input + _input2;
                break;
            case "-":
                _result = _input - _input2;
                break;
            case "*":
                _result = _input * _input2;
                break;
            case "/":
                if (_input2 != 0) // Prevent division by zero
                {
                    _result = _input / _input2;
                }
                else
                {
                    Debug.LogError("Division by zero.");
                    return;
                }
                break;
        }
        // Display the result
        InputText.SetText(_result.ToString());

        // Prepare for next input or operation
        _currentInput = "";
        _input2 = 0; // Reset _input2 in case of consecutive operations without '='
    }

    private void SetCurrentInput()
    {
        if (!string.IsNullOrEmpty(_currentInput))
        {
            if (_input == 0)
            {
                _input = double.Parse(_currentInput); // Use double.Parse
            }
            else
            {
                _input2 = double.Parse(_currentInput);
            }
            _currentInput = "";
        }
    }


    // clear all the inputs
    public void ClearInput()
    {
        _currentInput= "";
        _input = 0;
        _input2 = 0;
        _result = 0;
        InputText.SetText("");
    }
    #endregion Methods
}