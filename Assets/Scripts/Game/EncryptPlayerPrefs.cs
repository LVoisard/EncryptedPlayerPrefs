using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EncryptPlayerPrefs{

    /// <summary>
    /// Method to Encrypt Plaintext to CypherText
    /// </summary>
    /// <param name="plainText">the string value we want to encrypt</param>
    /// <param name="key">the key we use to encrypt</param>
    /// <param name="iv">the IV we use to encrypt</param>
    /// <returns> the cypherText we use as a String</returns>
    public static string Encrypt(string plainText,string key,string iv)
    {
        byte[] encrypted;
        byte[] myKey = Encoding.ASCII.GetBytes(key);
        byte[] myIv = Encoding.ASCII.GetBytes(iv);
        Console.WriteLine(myKey.Length);

        // Create a new AesManaged.    
        using (AesManaged aes = new AesManaged())
        {
            // Create encryptor    
            ICryptoTransform encryptor = aes.CreateEncryptor(myKey, myIv);
            // Create MemoryStream    
            using (MemoryStream ms = new MemoryStream())
            {
                // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                // to encrypt    
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Create StreamWriter and write data to a stream    
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // Return encrypted data    
        return Convert.ToBase64String(encrypted);
    }


    /// <summary>
    /// The method we use to Decrypt the cypherText
    /// </summary>
    /// <param name="cipherText">string value we want to decrypt</param>
    /// <param name="key">string key we use to decrypt</param>
    /// <param name="iv">string iv we use to decrypt</param>
    /// <returns>String value of the plainText</returns>
    public static string Decrypt(string cipherText,string key,string iv)
    {
        string plaintext = null;
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        byte[] myKey = Encoding.ASCII.GetBytes(key);
        byte[] myIv = Encoding.ASCII.GetBytes(iv);

        // Create AesManaged    
        using (AesManaged aes = new AesManaged())
        {
            
            // Create a decryptor    
            ICryptoTransform decryptor = aes.CreateDecryptor(myKey, myIv);
            // Create the streams used for decryption.    
            using (MemoryStream ms = new MemoryStream(cipherBytes))
            {
                // Create crypto stream    
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Read crypto stream    
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }

    //======================== The Following Methods are simply to facilitate saving to the playerprefs file. ==========================//

    /// <summary>
    /// deletes all parameters from PlayerPrefs
    /// </summary>
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }


    /// <summary>
    /// deletes a scpecific parameter from PlayerPrefs
    /// </summary>
    /// <param name="key"></param>
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }


    /// <summary>
    /// verify id a key exists
    /// </summary>
    /// <param name="key">parameter we want to look for in PlayerPrefs</param>
    /// <returns></returns>
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }


    /// <summary>
    /// save changes to the PlayerPrefs
    /// </summary>
    public static void Save()
    {
        PlayerPrefs.Save();
    }



    //================== The following methods are to set and get from the player prefs ======================//

    /// <summary>
    /// methods to get the a encrypted string value from the playerprefs
    /// this method is used by others to simplyfy all encryption and decryption
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="defaultValue">value it returns if there is an exception</param>
    /// <param name="myKey">key to decrypt with</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns> value from player prefs or defaultvalue</returns>
    public static string GetString(string key, string defaultValue, string myKey, string iv)
    {
        try
        {
            return Decrypt(PlayerPrefs.GetString(key), myKey, iv);
        }
        catch (Exception e) 
        {
            return defaultValue;
        }
    }
    /// <summary>
    /// methods to get the a encrypted string value from the playerprefs
    /// this method is used by others to simplyfy all encryption and decryption
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="myKey">key to decrypt with</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns>value from playerprefs</returns>
    public static string GetString(string key, string myKey, string iv)
    {
        return Decrypt(PlayerPrefs.GetString(key), myKey, iv);
    }

    /// <summary>
    /// method that parses the string we get from getString to a float
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="defaultValue">default we return if Exception</param>
    /// <param name="mykey">key to decrypt with</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns>value from playerprefs</returns>
    public static float GetFloat(string key, float defaultValue, string mykey, string iv) {
        try
        {
            return float.Parse(GetString(key, mykey, iv));
        }
        catch (Exception e) 
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// method that parses the string we get from getString to a float
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="mykey">key to decrypt with</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns>value from playerprefs</returns>
    public static float GetFloat(string key, string mykey, string iv) {
        return float.Parse(GetString(key, mykey, iv));
    }

    /// <summary>
    /// method that parses the string we get from getString to an integer
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="myKey">key to decrypt with</param>
    /// <param name="defaultValue">Default we return if there is an Exception</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns>value from playerprefs</returns>
    public static int GetInt(string key, int defaultValue, string myKey, string iv) {
        try
        {
            return Int32.Parse(GetString(key, defaultValue.ToString(), myKey, iv));
        }
        catch (Exception e) 
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// method that parses the string we get from getString to an integer
    /// </summary>
    /// <param name="key">parameter name in playerprefs</param>
    /// <param name="myKey">key to decrypt with</param>
    /// <param name="iv">key to decrypt with</param>
    /// <returns>value from playerprefs</returns>
    public static int GetInt(string key, string myKey, string iv) {
        return Int32.Parse(GetString(key, myKey, iv));
    }



    /// <summary>
    /// The set float value that we save is changed to a string to use the
    /// SetString method
    /// </summary>
    /// <param name="key">name of the parameter in playerprefs</param>
    /// <param name="value">the value we save</param>
    /// <param name="mykey">the key for encrypting</param>
    /// <param name="iv">the IV for encrypting</param>
    public static void SetFloat(string key, float value, string mykey, string iv) { 
        SetString(key, value.ToString(),mykey,iv); 
    }

    /// <summary>
    /// changes the integervalue to a string to use the setString method
    /// </summary>
    /// <param name="key">name of the parameter in playerprefs</param>
    /// <param name="value">the value we save</param>
    /// <param name="mykey">the key for encrypting</param>
    /// <param name="iv">the IV for encrypting</param>
    public static void SetInt(string key, int value, string mykey, string iv) { 
        SetString(key, value.ToString(), mykey, iv); 
    }


    /// <summary>
    /// The method first encrypts the value and then saves it to the 
    /// Playerprefs
    /// </summary>
    /// <param name="key">name of the parameter in playerprefs</param>
    /// <param name="value">the value we save</param>
    /// <param name="mykey">the key for encrypting</param>
    /// <param name="iv">the IV for encrypting</param>
    public static void SetString(string key, string value,string mykey,string iv) { 
        PlayerPrefs.SetString(key, Encrypt(value,mykey,iv)); 
    }
}
