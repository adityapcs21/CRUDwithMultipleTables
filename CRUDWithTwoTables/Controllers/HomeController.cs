using CRUDWithTwoTables.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace CRUDWithTwoTables.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ExampleDbModel db = new ExampleDbModel();
        public ActionResult Index(int Page = 1,int PageSize=10)
        {

            List<UserDetail> USER = db.UserDetails.Where(y=>y.IsActive==true).ToList();
            List<CarDetail> Car = db.CarDetails.ToList();
            List<MultipleClass> result = new List<MultipleClass>();

            foreach (var user in USER)
            {
                var data = new MultipleClass
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    UserEmail = user.UserEmail,
                    CivilIdNumber = user.CivilIdNumber,
                    
                    
                };

                //var multipletable = from c in USER
                //                    join st in Car on c.UserId equals st.Id

                //                    select new MultipleClass { UserDetails = c, CarDetails = st };



                var cardetails = string.Join(",", Car.Where(x => x.UserId == user.UserId).Select(y => y.CarLicense).ToList());
                result.Add(data);
                data.CarLicense = cardetails;
                

               
            }

            //ViewBag.TotalPages = Math.Ceiling(result.Count() / 20.0);
            //result = result.Skip((PageNumber - 1) * 20).Take(20).ToList();
            PagedList<MultipleClass> result1 = new PagedList<MultipleClass>(result, Page, PageSize);
            return View(result1);
        }


            public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateTable viewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(viewModel.ImageFile.FileName);
            string extension = Path.GetExtension(viewModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + "-" + fileName.Trim() + extension;
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            viewModel.ProfilePic = UploadPath + fileName;
            viewModel.ImageFile.SaveAs(viewModel.ProfilePic);


            var user = new UserDetail()
            {
                FullName = viewModel.FullName,
                UserEmail = viewModel.UserEmail,
                PasswordHash = viewModel.PasswordHash,
                CivilIdNumber = viewModel.CivilIdNumber,
                DOB = viewModel.DOB,
                MobileNo = viewModel.MobileNo,
                Address = viewModel.Address,
                RoleId = viewModel.RoleId,
                ProfilePic = viewModel.ProfilePic,
                CreatedDate = viewModel.CreatedDate,
                ModifiedDate = viewModel.ModifiedDate,
                IsNotificationActive = viewModel.IsNotificationActive,
                IsActive = viewModel.IsActive,
                DeviceId = viewModel.DeviceId,
                DeviceType = viewModel.DeviceType,
                FcmToken = viewModel.FcmToken,
                verify = viewModel.verify,
                VerifiedBy = viewModel.VerifiedBy,
                Area = viewModel.Area,
                Block = viewModel.Block,
                Street = viewModel.Street,
                Housing = viewModel.Housing,
                Floor = viewModel.Floor,
                NewPass = viewModel.NewPass,
                ConPass = viewModel.ConPass,
                Jadda = viewModel.Jadda,
                Reason = viewModel.Reason,
                ActivatedBy = viewModel.ActivatedBy,
                ActivatedDate = viewModel.ActivatedDate

            };

            var car = new CarDetail()

            {
                UserId = viewModel.UserId,
                CarLicense = viewModel.CarLicense

            };
            db.UserDetails.Add(user);
            db.CarDetails.Add(car);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }


        public ActionResult Edit(int? id)
        {
            var viewModel = (from a in db.UserDetails
                             where a.UserId == id
                             select new Models.EditView
                             {
                                 UserId = a.UserId,
                                 FullName = a.FullName,
                                 UserEmail = a.UserEmail,
                                 PasswordHash = a.PasswordHash,
                                 CivilIdNumber = a.CivilIdNumber,
                                 DOB = a.DOB,
                                 MobileNo = a.MobileNo,
                                 Address = a.Address,
                                 RoleId = a.RoleId,
                                 ProfilePic = a.ProfilePic,
                                 CreatedDate = a.CreatedDate,
                                 ModifiedDate = a.ModifiedDate,
                                 IsNotificationActive = a.IsNotificationActive,
                                 IsActive = a.IsActive,
                                 DeviceId = a.DeviceId,
                                 DeviceType = a.DeviceType,
                                 FcmToken = a.FcmToken,
                                 verify = a.verify,
                                 VerifiedBy = a.VerifiedBy,
                                 Area = a.Area,
                                 Block = a.Block,
                                 Street = a.Street,
                                 Housing = a.Housing,
                                 Floor = a.Floor,
                                 NewPass = a.NewPass,
                                 ConPass = a.ConPass,
                                 Jadda = a.Jadda,
                                 Reason = a.Reason,
                                 ActivatedBy = a.ActivatedBy,
                                 ActivatedDate = a.ActivatedDate
                             }).FirstOrDefault();

            var cars = db.CarDetails.Where(x => x.UserId == id).Select(y => y.CarLicense).ToList();

            viewModel.CarLicense.AddRange(cars);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(EditView insert)
        {

            var viewModel = db.UserDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();
            var viewModel1 = db.CarDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();

            viewModel.FullName = insert.FullName;
            viewModel.UserEmail = insert.UserEmail;
            viewModel.PasswordHash = insert.PasswordHash;
            viewModel.CivilIdNumber = insert.CivilIdNumber;
            viewModel.DOB = insert.DOB;
            viewModel.MobileNo = insert.MobileNo;
            viewModel.Address = insert.Address;
            viewModel.RoleId = insert.RoleId;
            viewModel.ProfilePic = insert.ProfilePic;
            viewModel.CreatedDate = insert.CreatedDate;
            viewModel.ModifiedDate = insert.ModifiedDate;
            viewModel.IsNotificationActive = insert.IsNotificationActive;
            viewModel.IsActive = insert.IsActive;
            viewModel.DeviceId = insert.DeviceId;
            viewModel.DeviceType = insert.DeviceType;
            viewModel.FcmToken = insert.FcmToken;
            viewModel.verify = insert.verify;
            viewModel.VerifiedBy = insert.VerifiedBy;
            viewModel.Area = insert.Area;
            viewModel.Block = insert.Block;
            viewModel.Street = insert.Street;
            viewModel.Housing = insert.Housing;
            viewModel.Floor = insert.Floor;
            viewModel.NewPass = insert.NewPass;
            viewModel.ConPass = insert.ConPass;
            viewModel.Jadda = insert.Jadda;
            viewModel.Reason = insert.Reason;
            viewModel.ActivatedBy = insert.ActivatedBy;
            viewModel.ActivatedDate = insert.ActivatedDate;

            db.Entry(viewModel).State = EntityState.Modified;
            db.Entry(viewModel1).State = EntityState.Modified;


            db.SaveChanges();
            return View();

        }

        public ActionResult Delete(int id)
        {
            var viewModel = db.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
            var viewModel1 = db.CarDetails.Where(x => x.UserId == id).ToList();
            viewModel.IsActive = false;
            db.Entry(viewModel).State = EntityState.Modified;
            if (viewModel1.Count() > 0)
                db.Entry(viewModel1).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

 