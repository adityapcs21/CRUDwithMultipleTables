using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWithTwoTables.Models
{
    public class MultipleClass
    {
        //public UserDetail UserDetails { get; set; }
        //public CarDetail CarDetails { get;set; }
        public int UserId { get; set; }
       
        public string FullName { get; set; }
        public string UserEmail { get; set; }
      
        public string CivilIdNumber { get; set; }
        public string CarLicense { get; set; }
    }
}


//public IEnumerable<UserDetail> userDetails { get; set; }
//public IEnumerable<CarDetail> carDetails { get; set; }