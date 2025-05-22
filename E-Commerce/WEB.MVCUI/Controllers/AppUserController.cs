using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WEB.MVCUI.ActionFilters;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;
using WEB.MVCUI.Areas.Admin.Models.Validators;
using MimeKit;
using Google.Apis.Gmail.v1.Data;
using WEB.MVCUI.Hashing.VektorelMvcApp.Hashing;
using FluentValidation;

namespace WEB.MVCUI.Controllers
{
    public class AppUserController : Controller
    {
        private static string _clientId = "682714689156-t3h8nnpmlhgrdo6ji5lplk9obrg1vh9m.apps.googleusercontent.com";
        private static string _clientSecret = "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";
        private static string _refreshToken = "1//049YDC7wG41beCgYIARAAGAQSNwF-L9IrQXji4u65rmjz2KM95z3CJ1nrPDYnmj0ggukWYYhx5gxmFhRgr48_szxK2ygyHpSGJks";

        [HttpGet]
        public IActionResult Register()
        {
            using(var ctx = new TradewndContext())
            {
                var cities = ctx.Cities.ToList();
                return View(cities);
            }

           
        }
        [HttpPost]
        public IActionResult Register(RegisterAppUserDto dto)
        {
          var validator = new RegisterAppUserDtoValidator();
          var result = validator.Validate(dto);
            if (!result.IsValid)
            {
                var errorMessages = "";
                foreach (var item in result.Errors)
                {
                    errorMessages += item.ErrorMessage + "<br />";

                }

                return Json(new { IsOk = false, ErrorMessages = errorMessages });
            }
            else 
            {
                using (var ctx = new TradewndContext())
                {
                    var appUser = new AppUser();
                    appUser.IsEmailApproved = false;
                    appUser.IsActive = false;
                    appUser.Email = dto.Email;
                    appUser.Address = dto.Address;
                    appUser.Password = EncryptionUtility.Encrypt(dto.Password,true);
                    appUser.Phone = dto.Phone;
                    appUser.CityId = dto.CityId;
                    appUser.DistrictId = dto.DistrictId;
                    appUser.FullName = dto.FullName;
                    appUser.EmailActivationCode = Guid.NewGuid().ToString();

                    ctx.AppUsers.Add(appUser);
                    ctx.SaveChanges();

                    var credential = new UserCredential(
                 new GoogleAuthorizationCodeFlow(
                     new GoogleAuthorizationCodeFlow.Initializer
                     {
                         ClientSecrets = new ClientSecrets
                         {
                             ClientId = _clientId,
                             ClientSecret = _clientSecret
                         }
                     }),
                 "user",
                 new TokenResponse { RefreshToken = _refreshToken });

                    var gmailService = new GmailService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "Tradewnd Consumer Application"
                    });

                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress("Tradewnd Consumer Application", "tradewndconsumerapp@gmail.com"));
                    emailMessage.To.Add(new MailboxAddress(dto.FullName, dto.Email));
                    emailMessage.Subject = "Tradewnd App Yeni Kayıt Onay Maili";
                    emailMessage.Body = new TextPart("html")
                    {
                        Text = $"Hoşgeldiniz sayın {dto.FullName} <br />. Kayıt işlemlerinizin tamamlanması ve mail aktivasyonunuz için lütfen <a href='http://localhost:5028/AppUser/activateemail/{appUser.EmailActivationCode}'>tıklayınız</a>"
                    };

                    using (var stream = new MemoryStream())
                    {
                        emailMessage.WriteTo(stream);
                        var rawMessage = Convert.ToBase64String(stream.ToArray())
                            .Replace('+', '-')
                            .Replace('/', '_')
                            .Replace("=", "");

                        var gmailMessage = new Message { Raw = rawMessage };
                        gmailService.Users.Messages.Send(gmailMessage, "me").Execute();
                    }

                    return Json(new { IsOk = true, Message = "Kaydınız Yapılmıştır. Lütfen Aktivasyon Mailini Kontrol Ediniz" });

                }

            }

        }


        [HttpGet]
        public IActionResult ActivateEmail(string id)
        {
            using(var ctx = new TradewndContext())
            {
                var appuser = ctx.AppUsers.SingleOrDefault(x => x.EmailActivationCode == id);

                if (appuser != null)
                {
                    appuser.IsEmailApproved = true;
                    appuser.IsActive = true;

                    ctx.SaveChanges();
                    HttpContext.Session.SetString("ActiveAppUser", JsonSerializer.Serialize(appuser));

                    return RedirectToAction("Index","Home");

                }
                else
                {
                    return RedirectToAction("EmailCannotBeApproved", "ErrorPages");
                }

            }
        }



        [HttpGet]
        public IActionResult LogIn() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInAppUserDto dto)
        {
            using (var ctx = new TradewndContext())
            {
               var appUser = ctx.AppUsers.SingleOrDefault(x=> x.Email == dto.Email && x.Password == EncryptionUtility.Encrypt(dto.Password,true));

                if (appUser != null)
                {
                    if (!appUser.IsEmailApproved) 
                    
                        return Json(new {IsOk = false, ErrorMessage= "Bu kullanıcının Email'i henüz onaylanmamıştır. Lütfen gelen maile tıklayarak email onayınızı yapınız." });
                    
                    if (!appUser.IsActive)
                    
                        return Json(new { IsOk = false, ErrorMessage = "Bu kullanıcının hesabı henüz onaylanmamıştır. Lütfen yönetici onayını bekleyiniz." });
                    

                }
                else
                
                    return Json(new { IsOk = false, ErrorMessage = "Böyle Bir Kullanıcı Bulunamadı." });

                HttpContext.Session.SetString("ActiveAppUser", JsonSerializer.Serialize(appUser));

                   return Json(new { IsOk = true });
            }
            
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("ActiveAppUser");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [CheckSession]
        public IActionResult Profile() 
        {
        
            return View();

        }
        [HttpGet]
        [CheckSession]
        public IActionResult ProfileUpdateUserInfo()
        {
            return View();
        }


        [HttpGet]
        [CheckSession]
        public IActionResult ProfileUpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [CheckSession]
        public IActionResult ProfileUpdatePassword(ProfileUpdatePasswordDto dto)
        {
            var validator = new ProfileUpdatePasswordDtoValidator();
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                return Json(new
                {
                    IsOk = false,
                    ErrorMessage = string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage))
                });
            }

            var email = JsonSerializer.Deserialize<AppUser>(HttpContext.Session.GetString("ActiveAppUser"))?.Email;

            using (var ctx = new TradewndContext())
            {
                var appUser = ctx.AppUsers.SingleOrDefault(x => x.Email == email && x.Password == dto.Password);

                if (appUser != null)
                {
                    appUser.Password = dto.NewPassword;
                    ctx.AppUsers.Update(appUser);
                    ctx.SaveChanges();

                    return Json(new { IsOk = true, Message = "Şifreniz değiştirildi. Şimdi çıkış yapacaksınız. Yeni şifrenizle giriş yapınız" });
                }
                else
                {
                    return Json(new { IsOk = false, ErrorMessage = "Şu anki şifrenizi yanlış girdiniz" });
                }
            }
        }


    }

}
