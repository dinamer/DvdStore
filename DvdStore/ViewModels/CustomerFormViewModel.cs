using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DvdStore.Models;

namespace DvdStore.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

    }
}