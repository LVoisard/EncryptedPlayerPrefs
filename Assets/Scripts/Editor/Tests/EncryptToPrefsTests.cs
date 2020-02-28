using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EncryptToPrefsTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void EncryptDecryptStringTest()
        {           
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;


            string clearText = "TestValue";
            string cypherText;
            string ExpectedResult = clearText;

            //Act

            cypherText = EncryptPlayerPrefs.Encrypt(clearText, key, iv);
            clearText = EncryptPlayerPrefs.Decrypt(cypherText, key, iv);

            //Assert

            Assert.AreEqual(clearText, ExpectedResult);
        
        }

        [Test]
        public void EncryptDecryptIntTest()
        {
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;
            int value = 69;


            string clearText =value.ToString();
            string cypherText;
            int ExpectedResult = value;

            //Act

            cypherText = EncryptPlayerPrefs.Encrypt(clearText, key, iv);
            clearText = EncryptPlayerPrefs.Decrypt(cypherText, key, iv);

            //Assert

            Assert.AreEqual(Int32.Parse(clearText), ExpectedResult);
        }

        [Test]
        public void EncryptDecryptFloatTest()
        {
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;
            float value = 69f;


            string clearText = value.ToString();
            string cypherText;
            float ExpectedResult = value;

            //Act

            cypherText = EncryptPlayerPrefs.Encrypt(clearText, key, iv);
            clearText = EncryptPlayerPrefs.Decrypt(cypherText, key, iv);

            //Assert

            Assert.AreEqual(float.Parse(clearText), ExpectedResult);
        }

        [Test]
        public void EncryptSaveDecryptStringTest()
        {
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;
            string keyname = "key1";


            string clearText = "TestValue";
            string ExpectedResult = clearText;

            //Act
            EncryptPlayerPrefs.SetString(keyname, clearText, key, iv);
            EncryptPlayerPrefs.Save();
            clearText = EncryptPlayerPrefs.GetString(keyname, key, iv);

            //Assert

            Assert.AreEqual(clearText, ExpectedResult);

        }

        [Test]
        public void EncryptSaveDecryptIntTest()
        {
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;
            string keyname = "key2";


            int clearText = 69;
            int ExpectedResult = clearText;

            //Act

            EncryptPlayerPrefs.SetInt(keyname, clearText, key, iv);
            EncryptPlayerPrefs.Save();
            clearText = EncryptPlayerPrefs.GetInt(keyname, key, iv);

            //Assert

            Assert.AreEqual(clearText, ExpectedResult);
        }

        [Test]
        public void EncryptSaveDecryptFloatTest()
        {
            //Arrange
            string key = Keys.MY_KEY;
            string iv = Keys.IV_KEY;
            string keyname = "key3";

            float clearText = 69f;
            float ExpectedResult = clearText;

            //Act

            EncryptPlayerPrefs.SetFloat(keyname, clearText, key, iv);
            EncryptPlayerPrefs.Save();
            clearText = EncryptPlayerPrefs.GetFloat(keyname, key, iv);

            //Assert

            Assert.AreEqual(clearText, ExpectedResult);
        }
    }
}
