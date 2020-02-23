using PhoneBookManagement.Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookManagement.Library.UseCases.Queries
{
    internal interface IContract
    {
        bool Execute(Contact contact);
    }
}
