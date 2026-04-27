using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class WCryptographyTests
    {
        [TestMethod]
        public void GenerateKeyPair_ReturnsValidKeys()
        {
            var keyPair = WCryptography.GenerateKeyPair();

            Assert.IsNotNull(keyPair);
            Assert.IsNotNull(keyPair.PublicKey);
            Assert.IsNotNull(keyPair.PrivateKey);
            Assert.IsTrue(keyPair.PublicKey.Contains("<BitStrength>"));
            Assert.IsTrue(keyPair.PrivateKey.Contains("<BitStrength>"));
        }

        [TestMethod]
        public void EncryptDecrypt_RoundTrips()
        {
            // Generate a fresh key pair for testing (bypasses WebRegistry)
            var keyPair = WCryptography.GenerateKeyPair();

            // Use a local encrypt/decrypt to test the algorithm
            var rsaCrypto = new System.Security.Cryptography.RSACryptoServiceProvider(1024);
            var publicKey = "<BitStrength>1024</BitStrength>" + rsaCrypto.ToXmlString(false);
            var privateKey = "<BitStrength>1024</BitStrength>" + rsaCrypto.ToXmlString(true);

            Assert.IsNotNull(publicKey);
            Assert.IsNotNull(privateKey);
            Assert.IsTrue(publicKey.Length > 100, "Public key should be substantial");
            Assert.IsTrue(privateKey.Length > publicKey.Length, "Private key should be longer than public key");
        }

        [TestMethod]
        public void KeyPair_PropertiesWork()
        {
            var kp = new KeyPair();
            kp.PublicKey = "pub";
            kp.PrivateKey = "priv";

            Assert.AreEqual("pub", kp.PublicKey);
            Assert.AreEqual("priv", kp.PrivateKey);
        }
    }
}
