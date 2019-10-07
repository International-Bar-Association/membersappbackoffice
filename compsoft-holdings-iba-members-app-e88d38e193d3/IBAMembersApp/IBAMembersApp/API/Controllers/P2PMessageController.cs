using IBAMembersApp.API.Models.Request;
using IBAMembersApp.API.Models.Response;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IBAMembersApp.API.Controllers
{
    public class P2PMessageController : BaseApiController
    {

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/P2P")]
        public HttpResponseMessage Get(int id, int take, int skip = 0)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = P2PMessagingLayer.GetMessagesBetweenUsers(CmsDb, (int)access.Session.record_id, id, take, skip);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/P2P")]
        public HttpResponseMessage Post(P2PRequestModel message)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var controllerContext = CreateController<HomeController>().ControllerContext;
            var result = P2PMessagingLayer.SendMessageToUser(CmsDb, Db, (int)access.Session.record_id, message.UserId, message.Message,controllerContext,message.UUID);
           
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [HttpPost]
        [Route("api/v2/P2P/{id}/read")]
        public HttpResponseMessage Read(int id)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var response = P2PMessagingLayer.SetMessageToRead(CmsDb, (int)access.Session.record_id, id);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/P2PConnections")]
        //Returns a list of user ids which we have had connections with
        public HttpResponseMessage GetConnections()
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = P2PMessagingLayer.GetconnectedUserIds(CmsDb, Db, (int)access.Session.record_id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [DateHeaderCheckWebApi]
        [XAuthHeaderUsingToken]
        [Route("api/v2/P2P")]
        public HttpResponseMessage Put(P2PThreadHideRequestModel request)
        {
            var access = UserRights.V2Login(Db, Request);
            if (access.HasErrors)
            {
                return Request.CreateErrorResponse(access.ErrorCode, access.ErrorMessage);
            }
            var result = P2PMessagingLayer.HideMessageThread(CmsDb, request.ThreadId, (int)access.Session.record_id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private static T CreateController<T>(RouteData routeData = null)
    where T : System.Web.Mvc.Controller, new()
        {
            T controller = new T();

            // Create an MVC Controller Context
            var wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}
