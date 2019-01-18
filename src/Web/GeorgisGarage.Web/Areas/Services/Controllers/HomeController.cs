using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GeorgisGarage.Services.Contracts;
using GeorgisGarage.Web.Areas.Services.Models;
using GeorgisGarage.Web.Areas.Services.Models.ServicesIndex;
using GeorgisGarage.Web.Common;
using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Services.Controllers
{
    [Area(Constants.RoadsArea)]
    public class HomeController : Controller
    {
        private readonly IServicesService servicesService;
        private readonly IImageService imageService;
        private readonly IServicesIndexService servicesIndexService;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IMapper mapper;

        public HomeController(IServicesService servicesService, IImageService imageService, IServicesIndexService servicesIndexService, IUsersService usersService, ICommentsService commentsService, IMapper mapper)
        {
            this.servicesService = servicesService;
            this.imageService = imageService;
            this.servicesIndexService = servicesIndexService;
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            ServicesIndexViewModel model = MapCategories();

            return View(model);
        }



        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateServiceViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.servicesService.Create(model.ServiceName, model.StartedTime, model.EndTime, model.ServiceTime, model.Description,
                   model.Video, userId, model.CoverPhoto, model.Images, model.Material, model.Time, model.Performance);

            if (!result)
            {
                return this.RedirectToAction("Error");
            }


            return this.RedirectToAction("All", "Categories");
        }

        [Authorize]
        public IActionResult EditService(string id)
        {
            var service = this.servicesService.GetServiceById(id);

            if (service == null)
            {
                //TODO
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (!this.User.IsInRole(Constants.AdminRole))
            {
                if (!this.User.IsInRole(Constants.OwnerRole))
                {

                    if (userId != service.UserId)
                    {
                        return Unauthorized();
                    }

                }
            }


            var model = mapper.Map<EditServiceViewModel>(service);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditService(EditServiceViewModel model)
        {
            var road = this.servicesService.GetServiceById(model.Id);

            if (road == null) return NotFound();


            var result = this.servicesService.Edit(model.Id, model.ServiceName, model.StartedTime, model.EndTime,
                model.ServiceTime, model.Description, model.Video, model.CoverPhoto, model.TimeRating, model.PerformanceRating,
                model.MaterialRating);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult EditServicePictures(string id)
        {
            var service = this.servicesService.GetServiceById(id);

            var model = mapper.Map<EditServiceViewModel>(service);
            

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddImagesToService(EditServiceViewModel model)
        {
            var service = this.servicesService.GetServiceById(model.Id);

            if (model.NewImages == null)
            {
                return RedirectToAction("Error");
            }

            var images = this.servicesService.AddImagesToService(model.NewImages, service.Id);

            return this.Redirect($"/Services/Home/EditServicePictures/{service.Id}");

        }

        [Authorize]
        public IActionResult DeleteServicePictures(string id, string serviceId)
        {
            this.imageService.RemoveImage(id);

            return this.Redirect($"/Services/Home/EditServicePictures/{serviceId}");
        }

        [Authorize]
        public IActionResult DeleteService(string id)
        {
            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);
            var service = servicesService.GetServiceById(id);

            if (service == null)
            {
                return NotFound();
            }

            if (!this.User.IsInRole(Constants.AdminRole))
            {
                if (!this.User.IsInRole(Constants.OwnerRole))
                {

                    if (user.Id != service.UserId)
                    {
                        return Unauthorized();
                    }

                }
            }

            bool result = servicesService.DeleteService(id);

            if (result == false)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("DeltedRoadSuccesfully", "Home");
        }

        public IActionResult DeltedServiceSuccesfully()
        {
            return this.View();
        }

        public IActionResult Service(string id)
        {
            var model = this.servicesService.Details<DetailsServiceViewModel>(id);

            if (model == null)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult MyServices()
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserServices = this.servicesIndexService.GetCurrentUserServicesById(currentUser);

            var model = new List<ServiceViewModel>();

            foreach (var service in currentUserServices)
            {
                var coverPhoto = this.imageService.ReturnImage(service.CoverPhoto.Image);
                var token = mapper.Map<ServiceViewModel>(service);
                token.CoverPhoto = coverPhoto;
                model.Add(token);
            }
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Comment(string id, DetailsServiceViewModel model)
        {
            var commentViewModel = model.Comment;
            commentViewModel.Commentator = this.usersService.GetUserByUsername(this.User.Identity.Name);
            commentViewModel.ServiceId = id;

            var result = this.commentsService.AddCommentToService(id, commentViewModel.Commentator, commentViewModel.Rating,
                 commentViewModel.Comment);

            if (result == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Service", "Home", new { @id = id });
        }


        [HttpPost]
        [Authorize]
        public IActionResult ReplyToComment(string id, string serviceId, DetailsServiceViewModel model)
        {
            var replyViewModel = model.Reply;
            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);

            var result = this.commentsService.AddReplyToComment(id, replyViewModel.Content, user);

            return this.RedirectToAction("Service", "Home", new { @id = serviceId });
        }

        [Authorize]
        public IActionResult DeleteComment(string id, string serviceId)
        {
            var result = this.commentsService.DeleteCommentAndItsReplies(id);

            if (result == false)
            {
                //TODO
                return this.NotFound();

            }

            return this.RedirectToAction("Service", "Home", new { @id = serviceId });
        }

        [Authorize]
        public IActionResult DeleteReply(string id, string serviceId)
        {
            var result = this.commentsService.DeleteReply(id);

            if (result == false)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Service", "Home", new { @id = serviceId });
        }

        private ServicesIndexViewModel MapCategories()
        {
            var model = new ServicesIndexViewModel
            {
                AllServices = MapAllServices(this.servicesIndexService.GetAllServices().ToList()),
                LatestServices = MapAllServices(this.servicesIndexService.GetLatestServices().ToList()),
                LongestServices = MapAllServices(this.servicesIndexService.GetLongestServices().ToList()),
                TopServices = MapAllServices(this.servicesIndexService.GetTopServices().ToList()),
            };

            return model;
        }

        private List<ServiceViewModel> MapAllServices(List<Service> services)
        {
            var servicesModel = new List<ServiceViewModel>();

            foreach (var service in services)
            {
                var coverPhoto = this.imageService.ReturnImage(service.CoverPhoto.Image);
                var token = mapper.Map<ServiceViewModel>(service);
                token.CoverPhoto = coverPhoto;
                servicesModel.Add(token);
            }

            return servicesModel;
        }
    }
}