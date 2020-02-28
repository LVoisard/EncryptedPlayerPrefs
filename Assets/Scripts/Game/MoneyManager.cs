using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] double balance;
    [SerializeField] Text balanceUI;   

    /// <summary>
    /// On the start of all activities with this script attached to them,
    /// depending on their name, (encrypted or regular) will load the balance
    /// with the correct method. If decrypting the balance throws an error, 
    /// it means that the game files are corrupted, it could be an application error
    /// or someone tried to change the registry
    /// </summary>
    private void Start()
    {
        try
        {
            if (SceneManager.GetActiveScene().name == "Encrypted Scene")
            {
                if (EncryptPlayerPrefs.HasKey("EncryptedBalance"))
                    balance = Double.Parse(EncryptPlayerPrefs.GetString("EncryptedBalance", Keys.MY_KEY, Keys.IV_KEY));
                else
                    balance = 0;
                UpdateBalanceUI();
            }
            else if (SceneManager.GetActiveScene().name == "Regular Scene")
            {
                if (PlayerPrefs.HasKey("EncryptedBalance"))
                    balance = Double.Parse(PlayerPrefs.GetString("balance"));
                else
                    balance = 0;
                UpdateBalanceUI();
            }
        }
        catch (Exception e)
        {
            print("Game Files Corrupted");
        }
    }

    /// <summary>
    /// Method to Update the balance text in the unity scene
    /// </summary>
    public void UpdateBalanceUI()
    {
        balanceUI.text = "Balance: " + balance;
    }

    /// <summary>
    /// Method to add to balance
    /// </summary>
    public void GiveMoney()
    {
        balance += 100;
        UpdateBalanceUI();
    }


    /// <summary>
    /// Method to subtract from balance
    /// </summary>
    public void TakeMoney()
    {
        balance -= 10;
        UpdateBalanceUI();
    }

    /// <summary>
    /// method to retrieve the encrypted value of the 
    /// balance key and to assign it to our current value
    /// </summary>
    public void CancelEncryptedTransactions()
    {
        balance = Double.Parse(EncryptPlayerPrefs.GetString("EncryptedBalance", Keys.MY_KEY, Keys.IV_KEY));
        UpdateBalanceUI();
    }


    /// <summary>
    /// method to retreive the balance from regular 
    /// playerprefs and to assign it to our blance
    /// </summary>
    public void CancelTransactions()
    {
        balance = Double.Parse(PlayerPrefs.GetString("balance"));
        UpdateBalanceUI();
    }

    /// <summary>
    /// Method to enmcrypt our transactions
    /// </summary>
    public void EncryptTransactions()
    {
        try
        {
            EncryptPlayerPrefs.SetString("EncryptedBalance", balance.ToString(), Keys.MY_KEY, Keys.IV_KEY);
            EncryptPlayerPrefs.Save();
        }
        catch (Exception e)
        {
            print(e);
        }
    }


    /// <summary>
    /// Method that save our balance values in plaintext
    /// </summary>
    public void SaveTransactions()
    {
        PlayerPrefs.SetString("balance", balance.ToString());
        PlayerPrefs.Save();
    }
}
