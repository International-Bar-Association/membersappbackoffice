using IBA_Common.Models;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace IBAMembersApp.Controllers
{
    internal class JsonNetModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            controllerContext.HttpContext.Request.InputStream.Position = 0;
            var stream = controllerContext.RequestContext.HttpContext.Request.InputStream;
            var readStream = new StreamReader(stream, System.Text.Encoding.UTF8);
            var json = readStream.ReadToEnd();
            var settings = new JsonSerializerSettings();
            settings.Culture = CultureInfo.InvariantCulture;
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.DeserializeObject(json, bindingContext.ModelType,settings);
        }
    }

    public class ScheduleController : BaseController
    {
        const int currentConference = 840;
        public ActionResult Index(int conferenceId = currentConference)
        {
            var conference = ConferenceLayer.GetConferenceById(IbaDb,conferenceId);
            var eventLocationData = Db.EventLocations.Where(r => r.ConferenceId == conferenceId);
            var eventLocations = new List<ScheduleEventLocationItemViewModel>();
            foreach(var evl in eventLocationData)
            {
                var floorName = Db.FloorNames.AsEnumerable().FirstOrDefault(r => r.Building == evl.Building && r.Floor == evl.Floor);
                eventLocations.Add(new ScheduleEventLocationItemViewModel()
                {
                    BuildingName = evl.Building.Name,
                    BuildingId = evl.Building.ScheduleEventBuildingId,
                    Id = evl.ScheduleEventLocationId,
                    RoomName = evl.LocationName,
                    CentreX = evl.CentreX,
                    CentreY = evl.CentreY,
                    Lat = evl.Lat,
                    Long = evl.Long,
                    TranslatedTitle = evl.TranslatedTitle,
                    Floor = evl.Floor,
                    FloorName = floorName != null ? floorName.Name : "",
                    IsOffsite = evl.IsOffsite
                });
            }
            var viewModel = new ScheduleEventLocationViewModel
            {
                EventName = conference.Name,
                Locations = eventLocations
            };

            return View(viewModel);
        }

        public JsonResult EditExternalLocation([ModelBinder(typeof(JsonNetModelBinder))]EditScheduleEventJsonViewModel model)
        {
            var existing = Db.EventLocations.SingleOrDefault(r => r.ScheduleEventLocationId == model.Id);
            if (existing != null)
            {
                existing.LocationName = model.linkedLocation;
                existing.Lat = model.lat;
                existing.Long = model.lon;
                existing.TranslatedTitle = model.TranslatedTitle;
                Db.SaveChanges();
            }
           return new JsonResult();

        }

        public JsonResult EditFloorName([ModelBinder(typeof(JsonNetModelBinder))]EditFloorNameJsonViewModel model)
        {
            var existing = Db.FloorNames.SingleOrDefault(r => r.Building.ScheduleEventBuildingId == model.id && r.Floor == model.index);

            if (existing != null)
            {
                existing.Name = model.floorName;
            } else
            {
                var newFloorName = new ScheduleEventFloorName() {
                    Building = Db.EventBuildings.SingleOrDefault(r=>r.ScheduleEventBuildingId == model.id),
                    Name = model.floorName,
                    Floor = model.index
                };
                Db.FloorNames.Add(newFloorName);
            }
            Db.SaveChanges();
            return new JsonResult();

        }

        public ActionResult MapSchedule(int conferenceId)
        {
            var result = ConferenceLayer.GetConfRoomsForConference(IbaDb, conferenceId);
            ConferenceLayer.CreateScheduleEventLocationsIfDoesntExist(Db, result);
            var vm = new MapScheduleViewModel()
            {
                Rooms = result,
                VenueName = "Sydney"
            };
            ViewBag.id = conferenceId;
            return View(vm);
        }


        [HttpPost]
        public JsonResult SaveConferenceEvents([ModelBinder(typeof(JsonNetModelBinder))]ScheduleEventMainJsonViewModel model)
        {
            var allConferenceRooms = ConferenceLayer.GetConfRoomsForConference(IbaDb, model.id);


            foreach (var building in model.locations)
            {
                var floorNumber = 0;
                foreach(var floor in building.floors)
                {
                    foreach(var ev in floor)
                    {

                        //NOTE: Create building if needed
                        var scheduleLocation = Db.EventLocations.SingleOrDefault(s => s.LocationName == ev.linkedLocation);
                        if (scheduleLocation == null || ev.isOffsite)
                        {
                            //NOTE: First check if this location is offsite
                            if(ev.isOffsite)
                            {
                                var eventBuilding = Db.EventBuildings.SingleOrDefault(r => r.Name == "Offsite" && r.ConferenceId == model.id);
                                if( eventBuilding == null)
                                {
                                    eventBuilding = new ScheduleEventBuilding()
                                    {
                                        Name = "Offsite",
                                        ConferenceId = model.id,
                                    };
                                }

                                if (scheduleLocation != null)
                                {
                                    scheduleLocation.ConferenceId = model.id;
                                    scheduleLocation.Long = ev.lon;
                                    scheduleLocation.Lat = ev.lat;
                                    scheduleLocation.TranslatedTitle = ev.TranslatedTitle;
                                    scheduleLocation.Building = eventBuilding;
                                } else
                                {
                                    var roomEvent = new ScheduleEventLocation
                                    {
                                        IsOffsite = true,
                                        Long = ev.lon,
                                        Lat = ev.lat,
                                        TranslatedTitle = ev.TranslatedTitle,
                                        LocationName = ev.linkedLocation,
                                        ConferenceId = model.id,
                                        Building = eventBuilding

                                    };
                                    Db.EventLocations.Add(roomEvent);
                                }

                                
                                Db.SaveChanges();

                            } else
                            {
                                //NOTE: If no location exists check it's is valid
                                if (allConferenceRooms.Select(t => t.RoomName).Contains(ev.linkedLocation))
                                {
                                    var eventBuilding = Db.EventBuildings.SingleOrDefault(r => r.Name == building.buildingName && r.ConferenceId == model.id);
                                    if (eventBuilding == null)
                                    {
                                        eventBuilding = new ScheduleEventBuilding()
                                        {
                                            Name = building.buildingName,
                                            ConferenceId = model.id,
                                        };
                                    }
                                    var roomEvent = new ScheduleEventLocation
                                    {
                                        Floor = floorNumber,
                                        CentreX = ev.centreX,
                                        CentreY = ev.centreY,
                                        LocationName = ev.linkedLocation,
                                        ConferenceId = model.id,
                                        Building = eventBuilding

                                    };
                                    Db.EventLocations.Add(roomEvent);
                                    Db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var eventBuilding = Db.EventBuildings.SingleOrDefault(r => r.Name == building.buildingName && r.ConferenceId == model.id);
                            if (eventBuilding == null)
                            {
                                eventBuilding = new ScheduleEventBuilding()
                                {
                                    Name = building.buildingName,
                                    ConferenceId = model.id,
                                };
                            }
                            scheduleLocation.Floor = floorNumber;
                            scheduleLocation.ConferenceId = model.id;
                            scheduleLocation.CentreX = ev.centreX;
                            scheduleLocation.CentreY = ev.centreY;
                            scheduleLocation.Building = eventBuilding;
                            Db.SaveChanges();

                        }
                    }
                    floorNumber += 1;
                }
            }
            return new JsonResult();
        }

        public void LoadInSchedule(int conferenceId)
        {
            var allConferenceRooms = ConferenceLayer.GetConfRoomsForConference(IbaDb, conferenceId);
            for (var level = 2; level < 6; level++)
            {
                var path = Server.MapPath("~/App_Data/level" + level + ".json");
                using (var r = new StreamReader(path))
                {
                    var json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<List<ScheduleEventJsonViewModel>>(json);
                    foreach (var item in items)
                    {
                        var scheduleLocation = Db.EventLocations.SingleOrDefault(s => s.LocationName == item.linkedLocation);
                        if (scheduleLocation == null)
                        {
                            //NOTE: If no location exists check it's is valid
                            if(allConferenceRooms.Select(t=>t.RoomName).Contains(item.linkedLocation))
                            {
                                var roomEvent = new ScheduleEventLocation
                                {
                                    Floor = level,
                                    CentreX = item.centreX,
                                    CentreY = item.centreY,
                                    LocationName = item.linkedLocation,
                                    ConferenceId = conferenceId,

                                };
                                Db.EventLocations.Add(roomEvent);
                            }
                        } else
                        {
                            scheduleLocation.Floor = level;
                            scheduleLocation.ConferenceId = conferenceId;
                            scheduleLocation.CentreX = item.centreX;
                            scheduleLocation.CentreY = item.centreY;
                        }
                        
                        Db.SaveChanges();
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(IdOnlyRequestModel idOnly)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var scheduleEventLocation = Db.EventLocations.SingleOrDefault(eventLocation => eventLocation.ScheduleEventLocationId == idOnly.Id);
            if (scheduleEventLocation != null)
            {
                Db.EventLocations.Remove(scheduleEventLocation);
                Db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }

    public class ScheduleEventMainJsonViewModel
    {
        public List<ScheduleEventBuildingJsonViewModel> locations { get; set; }
        public int id { get; set; }
    }

    public class ScheduleEventBuildingJsonViewModel
    {
        public string buildingName { get; set; }
        public List<ScheduleEventJsonViewModel[]> floors { get; set; }
    }
    
    public class ScheduleEventJsonViewModel
    {
        public int centreX { get; set; }
        public int centreY { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string TranslatedTitle { get; set; }
        public bool isOffsite { get; set; }
        public string linkedLocation { get; set; }
        public string sectionItemText { get; set; }
    }

    public class EditScheduleEventJsonViewModel: ScheduleEventJsonViewModel
    {
        public int Id { get; set; }
    }

    public class EditFloorNameJsonViewModel {
        public int id { get; set; }
        public string floorName { get; set; }
        public int index { get; set; }
    }

}
