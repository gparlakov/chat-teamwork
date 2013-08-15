using AttributeRouting.Web.Http;
using Chat.Models;
using Chat.Repositories;
using Chat.Repository;
using Chat.Services.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Chat.FileExtensions;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class UsersController : ApiController
    {
        private UserRepository repository;
        private const string imgUrl = "https://www.dropbox.com/s/bbr08j0u7iuhxvp/default_person_large.jpg?dl=1";

        public UsersController(IRepository<User> repo)
        {
            this.repository = repo as UserRepository;
        }

        [GET("api/users/{sessionKey}")]
        public IEnumerable<UserModel> GetUsers(string sessionKey)
        {
            var users = this.repository.All();

            var userModels =
                                from user in users
                                where user.SessionKey != sessionKey
                                select new UserModel()
                                {
                                    Id = user.Id,
                                    Nickname = user.Nickname
                                };

            return userModels.ToList();
        }

        [HttpGet]
        [GET("api/users/logout/{sessionKey}")]
        public HttpResponseMessage LogoutUser(string sessionKey)
        {
            this.repository.LogoutUser(sessionKey);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [POST("api/users/{sessionKey}/image")]
        public HttpResponseMessage PostProfileImage(string sessionKey)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                string imageUrl = string.Empty;

                var postedFile = httpRequest.Files[0];

                if (!ImageValidator.CheckImageFormat(postedFile.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                imageUrl = DropboxUploader.DropboxShareFile(postedFile.InputStream, postedFile.FileName);

                imageUrl = imageUrl.Substring(0, imageUrl.Length - 5);

                this.repository.UpdateImageUrl(sessionKey, imageUrl);

                result = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        [POST("api/users/login")]
        public HttpResponseMessage LoginUser(UserModelLogin user)
        {
            Chat.Models.User userFull = new Chat.Models.User()
            {
                Username = user.Username,
                Password = user.Password
            };

            var userLog = this.repository.LoginUser(userFull);

            var loggedUser = new UserModelLogged()
            {
                Nickname = userLog.Nickname,
                SessionKey = userLog.SessionKey,
                ImageUrl = userLog.ImageUrl ?? imgUrl
            };


            return Request.CreateResponse(HttpStatusCode.OK, loggedUser);
        }

        [POST("api/users/register")]
        public HttpResponseMessage RegisterUser(UserModelRegister user)
        {
            Chat.Models.User userFull = new Chat.Models.User()
            {
                Username = user.Username,
                Password = user.Password,
                Nickname = user.Nickname,
                ImageUrl = imgUrl
            };

            var userReg = this.repository.Add(userFull);
            var userLog = this.repository.LoginUser(userReg);

            var loggedUser = new UserModelLogged()
            {
                Nickname = userLog.Nickname,
                SessionKey = userLog.SessionKey,
                ImageUrl = userLog.ImageUrl
            };

            return Request.CreateResponse(HttpStatusCode.OK, loggedUser);
        }
    }
}