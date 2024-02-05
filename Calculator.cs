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
        SetCurrentInput(); // Ensure the current input is set to either _input or _input2

        if (!string.IsNullOrEmpty(_operation))
        {
            // If there's already an operation, calculate the result with the current _input and _input2
            Calculate();
            // Set the result as the new _input for the next operation
            _input = _result;
            _input2 = 0;
        }
        
        _operation = val;
        _currentInput = "";
        //_equalIsPressed = false; // Reset this flag since we're starting a new operation
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
        Debug.Log($" ClickEqual val: {val}");
        Calculate();
        _equalIsPressed = true;
    }

    private void Calculate()
    {
        if (_input != 0 && !string.IsNullOrEmpty(_operation) )
        {
            SetCurrentInput();
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
                    _result = _input / _input2;
                    break;
            }
            
            // show the result
            InputText.SetText(_result.ToString());
            
            // save the last result for next calculation
            _input = _result;
        }
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
