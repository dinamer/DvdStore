using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DvdStore.Models;

namespace DvdStore.Dtos
{
    public class RentalsDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}