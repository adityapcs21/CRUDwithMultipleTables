using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWithTwoTables.Models
{
    public class CreateTable
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string PasswordHash { get; set; }
        public string CivilIdNumber { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string ProfilePic { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsNotificationActive { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string FcmToken { get; set; }
        public Nullable<bool> verify { get; set; }
        public string VerifiedBy { get; set; }
        public string Area { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Housing { get; set; }
        public string Floor { get; set; }
        public string NewPass { get; set; }
        public string ConPass { get; set; }
        public string Jadda { get; set; }
        public string Reason { get; set; }
        public string ActivatedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public Nullable<System.DateTime> ActivatedDate { get; set; }
        public string CarLicense { get; set; }

    }
}