using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RenCar.Service.Services.RentCompanies.Models
{
    public class RentCompanyCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone {  get; set; }
    }
}
