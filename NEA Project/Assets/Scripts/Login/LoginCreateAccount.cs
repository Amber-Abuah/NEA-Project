using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LoginCreateAccount : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField, firstNameField, lastNameField, passwordField;
    [SerializeField] TextMeshProUGUI createAccountErrorText;

    [SerializeField] TMP_InputField loginUsernameField, loginPasswordField;
    [SerializeField] TextMeshProUGUI loginErrorText;

    [SerializeField] int numOfRequiredCapitals, numOfRequiredSymbols, numOfRequiredNumbers, numOfRequiredCharacters;
    [SerializeField] PlayerInformation playerInformation;

    DatabaseManipulator databaseManipulator;
    LoadLevel levelLoader;

    void Start()
    {
        databaseManipulator = new DatabaseManipulator(); // Initialize database manipulator
        levelLoader = new LoadLevel(); // Initialize the level loader
    }

    public void CreateAnAccount()
    {
        createAccountErrorText.text = ""; // Reset the error message box

        // If any fields are empty, give the user an error message telling them to enter all their details
        if (usernameField.text == "" || firstNameField.text == "" || lastNameField.text == "" || passwordField.text == "")
        {
            createAccountErrorText.text = "Not all details have been entered!";
        }
        else
        {
            // Retrieve the password from the database under the entered username
            string password = databaseManipulator.PerformSelectCommand("Password", "Player", "Username", usernameField.text);

            // If no password was retrieved, then the username does not exist. An error is displayed to the user telling them this.
            if (password != "")
            {
                createAccountErrorText.text = "The username you entered is already in use!\n";
            }
            else // If a password was retrieved from the database, display a message telling the user their username is valid
            {
                createAccountErrorText.text = "Valid username!\n";

                // If the password has all the requirements, write their details to the database
                if (CheckPasswordStrength())
                {
                    databaseManipulator.PerformInsertCommand(usernameField.text, passwordField.text, firstNameField.text, lastNameField.text);

                    // Retrieve the player's ID from the database
                    int playerID = databaseManipulator.PerformIntSelectCommand("PlayerID", "Player", "Username", usernameField.text);

                    // Save their ID in the Player Information script
                    playerInformation.SavePlayerInfo(usernameField.text, playerID);

                    Debug.Log("Details written to database!");
                    levelLoader.LoadChoiceLevel(); // Load the next level
                }
            }
        }
    }

    bool CheckPasswordStrength()
    {
        bool isStrongPassword = true;

        int capitalCount = 0, numberCount = 0, symbolCount = 0;
        string password = passwordField.text;

        // Loop through all characters of the password
        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsUpper(password[i]))
                capitalCount++;
            else if (char.IsSymbol(password[i]))
                symbolCount++;
            else if (char.IsNumber(password[i]))
                numberCount++;
        }

        // Print out error messages, telling the user what password requirements they haven't met
        if (password.Length < numOfRequiredCharacters)
        {
            createAccountErrorText.text += "Not enough characters in password!\n";
            isStrongPassword = false;
        }
        if (capitalCount < numOfRequiredCapitals)
        {
            createAccountErrorText.text += "Not enough capitals in password!\n";
            isStrongPassword = false;
        }
        if (numberCount < numOfRequiredNumbers)
        {
            createAccountErrorText.text += "Not enough numbers in password!\n";
            isStrongPassword = false;
        }
        if (symbolCount < numOfRequiredSymbols)
        {
            createAccountErrorText.text += "Not enough symbols in password!\n";
            isStrongPassword = false;
        }

        return isStrongPassword;
    }

    public void CheckLoginDetails()
    {

        // If any fields are empty, display error message to user
        if (loginUsernameField.text == "" || loginPasswordField.text == "")
        {
            loginErrorText.text = "Atleast one of the fields are empty!";
        }
        else
        { 
            // Retrive password from database, under username entered
            string password = databaseManipulator.PerformSelectCommand("Password", "Player", "Username", loginUsernameField.text);

            if (password == "") // If no password was returned, then the username must not exist
            {
                loginErrorText.text = "Username does not exist!";
            }
            else // If a password was found, the username exists
            {
                if (loginPasswordField.text == password) // If the password matches the one retrieved from the database
                {
                    Debug.Log("Password correct! We can now login!"); // Username and password are valid
                    loginErrorText.text = "Logging in...";

                    // Retrieve the player's ID from the database
                    int playerID = databaseManipulator.PerformIntSelectCommand("PlayerID", "Player", "Username", loginUsernameField.text);

                    // Save their ID in the Player Information script
                    playerInformation.SavePlayerInfo(loginUsernameField.text, playerID);

                    // Load the next level
                    levelLoader.LoadChoiceLevel();
                }
                else
                {
                    loginErrorText.text = "Incorrect password!";
                }
            }
        }
    }
}

