using LimitlessSoft.AspNet.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMagacin.Models;

namespace WebMagacin.Extensions
{
    public static class WebMagacinExtensions
    {
        public static UserModel GetUser(this HttpRequest request)
        {
            Client currentSession = Client.Get(request.HttpContext);
            return UserModel.Get(currentSession.Identifier);
        }
        public static UserModel GetUser(this HttpResponse response)
        {
            Client currentSession = Client.Get(response.HttpContext);
            return UserModel.Get(currentSession.Identifier);
        }
        public static UserModel GetUser(this HttpContext context)
        {
            Client currentSession = Client.Get(context);
            return UserModel.Get(currentSession.Identifier);
        }
    }
}
