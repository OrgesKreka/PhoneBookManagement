using PhoneBookManagement.Library.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookManagement.Library.UseCases
{
    /// <summary>
    ///     Nderfaqja kryesore qe permban gjithe UseCase-t si metoda
    /// </summary>
    public interface IPhoneBook
    {
        /// <summary>
        ///     Shton nje kontakt
        ///     **Hedh exception nese ndodh gabim gjate shtimit!
        /// </summary>
        /// <param name="contact">Objekti me te dhenat e kontaktit qe do te shtohet</param>
        void Add(Contact contact);

        /// <summary>
        ///     Fshin nje kontakt
        ///     **Hedh exception nese ndodh gabim gjate fshirjes!
        /// </summary>
        /// <param name="contact">Kontakti qe do te fshihet</param>
        void Delete(Contact contact);

        /// <summary>
        ///     Ndrshon te dhenat e nje kontakti
        ///     **Hedh exception nese ndodh nje gabim gjate ndryshimit!
        /// </summary>
        /// <param name="contact">Kontakti qe do ndryshohet</param>
        /// <returns>Kthen kontaktin e ndryshuar.
        ///          null nese nuk arrin te ndrshoje kontaktin
        /// </returns>
        Contact Edit(Contact contact);

        /// <summary>
        ///     Kthen listen e kontakteve te renditura sipas rendit alfabetik
        ///     Supozon se renditja do te behet sipas Emrit
        ///     **Hedh exception nese ndodh ndonje gabim!
        /// </summary>
        /// <returns>Listen me kontakte</returns>
        IEnumerable<Contact> ContactsOrdered();

        /// <summary>
        ///     Kthen listen e kontakteve te renditura sipas emrit
        ///     Sipas supozimit te mesiperm, kjo metode kthen te njejtin rezultat si <see cref="ContactsOrdered"/>
        ///     **Hedh exception nese ndodh nje gabim!
        /// </summary>
        /// <returns>Listen me kontakte</returns>
        IEnumerable<Contact> ContactsOrderedByFirstName();

        /// <summary>
        ///     Kthen listen e kontakteve te renditura sipas mbiemrit
        ///     **Hedh exception nese ndodh nje gabim!
        /// </summary>
        /// <returns>Listen me kontakte</returns>
        IEnumerable<Contact> ContactsOrderedByLastName();

        /// <summary>
        ///     Ruan nje liste me kontakte ne menyre asinkrone
        ///     **Hedh exception nese ndodh gabim!
        /// </summary>
        /// <param name="contacts">Lista me kontakte</param>
        /// <returns>Task</returns>
        Task AddAsync(IEnumerable<Contact> contacts);

        /// <summary>
        ///     Kthen listen e kontakteve ne menyre asinkrone
        ///     **Hedh exception nese ndodh ndonje gabim gjate leximit
        /// </summary>
        /// <returns>Listen me kontakte</returns>
        Task<IEnumerable<Contact>> GetContactsAsync();

        /// <summary>
        ///     Ndryshon te dhenat e nje liste kontaktesh ne menyre asinkrone
        ///     **Hedh exception nese ndodh ndonje gabim gjate modifikimit
        /// </summary>
        Task EditContactsAsync(IEnumerable<Contact> contacts);

        /// <summary>
        ///     Fshin listen e kontakteve ne menyre asinkrone
        ///     **Hedh exception nese ndodh ndonje gabim gjate leximit
        /// </summary>
        Task DeleteContactsAsync(IEnumerable<Contact> contacts);

    }
}
