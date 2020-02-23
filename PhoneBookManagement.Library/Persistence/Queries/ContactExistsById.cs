using PhoneBookManagement.Library.Domain;
using PhoneBookManagement.Library.UseCases;
using PhoneBookManagement.Library.UseCases.Queries;
using System;
using System.Linq;

namespace PhoneBookManagement.Library.Persistence.Queries
{
    /// <summary>
    ///     Klasa qe kontrollon nese nje kontakt gjendet apo jo ne baze te Guid
    /// </summary>
    public class ContactExistsById : IContactExistsById
    {
        /// <summary>
        ///     nderfaqja e klases qe ofron ruajtjen e te dhenave
        /// </summary>
        private IStorage _persistenceStorage;
        public ContactExistsById(IStorage storage)
        {
            if (_persistenceStorage == null) throw new ArgumentException(nameof(storage));

            _persistenceStorage = storage;
        }

        /// <summary>
        ///     Kontrollon ne baze te GUID nese nje kontakt ekziston ne formatin e persistences
        /// </summary>
        /// <param name="contact">Kontakti qe do kontrollohet</param>
        /// <returns>true nese gjendet, false ne te kundert</returns>
        public bool Execute(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));

            var existingContact = _persistenceStorage.ContactsOrdered()?.FirstOrDefault(c => c.Guid == contact.Guid);

            return existingContact == null;
        }
    }
}
