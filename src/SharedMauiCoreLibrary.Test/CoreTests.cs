using AndreasReitberger.Shared.Core.Utilities;

namespace SharedMauiCoreLibrary.Test;

public class LicenseTests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestLicenseServerCommunication()
    {
        Assert.Pass();
    }

    [Test]
    public void EncryptionTests()
    {
        try
        {
            string base64Key = EncryptionManager.GenerateBase64Key();
            Assert.That(!string.IsNullOrEmpty(base64Key));

            byte[] base64KeyBates = EncryptionManager.GetBytesFromBase64Key(base64Key);

            string plainText = "This text will be encrypted";
            string encryptedWithKey = EncryptionManager.EncryptStringToBase64String(plainText, base64Key, 256);
            Assert.That(!string.IsNullOrEmpty(encryptedWithKey));

            string decrytpedTextWithKey = EncryptionManager.DecryptStringFromBase64String(encryptedWithKey, base64Key, 256);
            Assert.That(plainText == decrytpedTextWithKey);

            string saltHexString = EncryptionManager.GetHexStringFromSalt(EncryptionManager.CreateRandomSalt(7));
            Assert.That(saltHexString.Length == 7 * 2);
            byte[] salt = EncryptionManager.GetSaltFromHexString(saltHexString);

            string userpassword = "This is a secret text";
            byte[] hashedPassword = EncryptionManager.SaltWithPasswordString(userpassword, salt, 32);
            Assert.That(hashedPassword.Length, Is.EqualTo(32));

            string encryptedText = EncryptionManager.EncryptStringToBase64String(plainText, hashedPassword, 256);

            string decrytpedText = EncryptionManager.DecryptStringFromBase64String(encryptedText, hashedPassword, 256);
            Assert.That(plainText == decrytpedText);

            // Recreate hash from user password
            salt = EncryptionManager.GetSaltFromHexString(saltHexString);
            hashedPassword = EncryptionManager.SaltWithPasswordString(userpassword, salt, 32); 
            decrytpedText = EncryptionManager.DecryptStringFromBase64String(encryptedText, hashedPassword, 256);
            Assert.That(plainText == decrytpedText);
        }
        catch (Exception ex) { 
        
            Assert.Fail(ex.Message);
        }
    }
}
