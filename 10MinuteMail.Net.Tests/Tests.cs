using System;
using NUnit.Framework;

namespace _10MinuteMail.Net.Tests
{
    [TestFixture]
    public class Tests
    {
        private TenMinuteMailNet _tenMinuteMail;

        [SetUp]
        public void SetUp()
        {
            _tenMinuteMail = new TenMinuteMailNet();
        }

        [Test]
        public void ValidateGenerateNewEmail()
        {
            var email = _tenMinuteMail.EmailAddress;
            _tenMinuteMail.GenerateNewEmailAddress();
            StringAssert.AreNotEqualIgnoringCase(email, _tenMinuteMail.EmailAddress, "Email shouldn't match");
        }

        [Test]
        public void ValidateReset10Minutes()
        {           
            Console.WriteLine($"Before: {_tenMinuteMail.SecondsLeft}");
            Console.WriteLine("Resetting 10 minutes");
            _tenMinuteMail.Reset10Minutes();
            Console.WriteLine($"After: {_tenMinuteMail.SecondsLeft}");
            Assert.Greater(_tenMinuteMail.SecondsLeft, 595);
        }

        [Test]
        public void ValidateReset100Minutes()
        {
            Console.WriteLine($"Before: {_tenMinuteMail.SecondsLeft}");
            Console.WriteLine("Resetting 100 minutes");
            _tenMinuteMail.Reset100Minutes();
            Console.WriteLine($"After: {_tenMinuteMail.SecondsLeft}");
            Assert.Greater(_tenMinuteMail.SecondsLeft, 5990);
        }

        [Test]
        public void ValidateHasWelcomeEmail()
        {
            Assert.AreEqual(1, _tenMinuteMail.EmailCount, "Empty email box");
        }

        [Test]
        public void ValidateLastEmail()
        {
            Console.WriteLine(_tenMinuteMail.LastEmail.Html);
        }
    }
}