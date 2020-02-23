using PhoneBookManagement.Library.Domain;
using PhoneBookManagement.Library.UseCases;
using PhoneBookManagement.Library.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBookManagement.Library.Persistence.Queries
{
    /// <summary>
    ///     Klasa qe kontrollon nese nje kontakt gjendet apo jo ne baze te emrit dhe mbiemrit
    /// </summary>
    class ContactExistsByNumber : IContactExistsByNumber
    {
        private IStorage _persistenceStorage;

        public ContactExistsByNumber(IStorage storage)
        {
            if (_persistenceStorage == null) throw new ArgumentException(nameof(storage));

            _persistenceStorage = storage;
        }

        /// <summary>
        ///     Kontrollon nese nje kontakt me emer dhe mbiemer gjenden ne formatin e persistences
        /// </summary>
        /// <param name="contact">Kontakti qe do te kontrollohet</param>
        /// <returns>true nese gjendet false ne te kundert</returns>
        public bool Execute(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));

            var existingContact = _persistenceStorage.ContactsOrdered()?.FirstOrDefault(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName);

            return existingContact == null;
        }
    }
}
