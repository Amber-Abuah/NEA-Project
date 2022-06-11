using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UsernamePasswordValidator : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField, passwordField;
    public int numOfRequiredCharacters, numOfRequiredCapitals, numOfRequiredSymbols, numOfRequiredNumbers;


    public void CheckUsername()
    {
        string username =  usernameField.text;

        Debug.Log("User name checked!");
    }

    public void CheckPassword()
    {
        int numOfCapitals = 0, numOfSymbols = 0 , numOfNumbers = 0;
        string password = passwordField.text;

        for (int i = 0; i < password.Length; i++)
        {
            if (Char.IsUpper(password[i]))
                numOfCapitals++;
            else if (Char.IsSymbol(password[i]))
                numOfSymbols++;
            else if (Char.IsNumber(password[i]))
                numOfNumbers++;
        }

        if (password.Length < 8)
            Debug.Log("Not enough characters!");
        if (numOfCapitals < 1)
            Debug.Log("Not enough capitals!");
        if (numOfSymbols < 2)
            Debug.Log("Not enough symbols!");
        if (numOfNumbers < 1)
            Debug.Log("Not enough numbers!");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            CheckUsername();

        if (Input.GetKeyDown(KeyCode.Backspace))
            CheckPassword();
    }
}
