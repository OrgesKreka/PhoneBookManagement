using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PhoneBookManagement.JsonPersistence;
using PhoneBookManagement.Library.Domain;

namespace PhoneBookManager.Test
{
    [TestClass]
    public class JsonPhoneBookManagerTest
    {

        private static JSonPhoneBookStorage _jsonManagement;
        private static string _filePath;
        private static Contact _testContat;

        [ClassInitialize()]
        public static void TestInitialize(TestContext context)
        {
             _filePath = Path.Combine(Environment.CurrentDirectory, "PhoneBook.json");

            if (File.Exists(_filePath))
                File.Delete(_filePath);

            _jsonManagement = new JSonPhoneBookStorage(_filePath);
            _testContat = new Contact
            {
                FirstName = "Kasem",
                LastName = "Trebeshina",
                Number = new PhoneNumber { PhoneNumberRaw = "123456789" },
                PhoneType = PhoneType.HOME,
                Guid = new Guid()
            };
        }

        [TestMethod]
        public void JsonManager_AddContact_AssertThatFileContainsAtLeastOneContact()
        {
            _jsonManagement.Add(_testContat);
            var listOfContacts = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(_filePath));

            Assert.IsTrue(listOfContacts.Count >= 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JsonManager_AddContact_AssertThatManagerThrowsExceptionIfSameContactIsAdded()
        {
            _jsonManagement.Add(_testContat);
            
        }

        [TestMethod]
        public void JsonManager_GetContact_AssertThatFileContainsTestContact()
        {
            var listOfContacts = _jsonManagement.ContactsOrdered();
        
            Assert.IsTrue(listOfContacts.Any(x => x.Guid == _testContat.Guid));
        }

        [TestMethod]
        public void JsonManager_EditContact_AssertThatContacIsEdited()
        {
            _jsonManagement.Add(new Contact
            {
                FirstName = "User1Name",
                LastName = "User1Surname",
                Number = new PhoneNumber { PhoneNumberRaw = "123456789" },
                PhoneType = PhoneType.CELLPHONE,
                Guid = Guid.NewGuid()
            }) ;


            var listOfContacts = _jsonManagement.ContactsOrdered();
            var contact = listOfContacts.First(x => x.Guid == _testContat.Guid);
            contact.FirstName = "ChangedName";
            _jsonManagement.Edit(contact);

             listOfContacts = _jsonManagement.ContactsOrdered();
             contact = listOfContacts.First(x => x.Guid == _testContat.Guid);

            CompareContacts(contact, _testContat, (x,y)=> x.Guid == y.Guid && x.FirstName != y.FirstName);
            
        }

        [TestMethod]
        public void JsonManager_GetContacts_AssertThatContactsAreOrdered()
        {
            var listOfContacts = _jsonManagement.ContactsOrdered();

            var orderedContacts = listOfContacts.OrderBy(x => x.FirstName);

            CompareIEnumerable<Contact>(listOfContacts, orderedContacts, (x, y) => x.Guid == y.Guid);
        }

        private static void CompareContacts(Contact one, Contact two, Func<Contact, Contact, bool> comparisionFunction)
        {
            Assert.IsTrue( comparisionFunction(one, two));
        }

        private static void CompareIEnumerable<T>(IEnumerable<T> one, IEnumerable<T> two, Func<T, T, bool> comparisonFunction)
        {
            var oneArray = one as T[] ?? one.ToArray();
            var twoArray = two as T[] ?? two.ToArray();

            if (oneArray.Length != twoArray.Length)
            {
                Assert.Fail("Collections are not same length");
            }

            for (int i = 0; i < oneArray.Length; i++)
            {
                var isEqual = comparisonFunction(oneArray[i], twoArray[i]);
                Assert.IsTrue(isEqual);
            }
        }

    }
}
