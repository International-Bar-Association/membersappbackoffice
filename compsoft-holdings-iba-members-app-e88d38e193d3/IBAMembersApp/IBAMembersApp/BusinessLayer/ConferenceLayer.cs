using IBA_Common;
using IBA_Common.Models;
using IBAMembersApp.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IBAMembersApp.BusinessLayer
{
    public class ConferenceLayer
    {
        public static ConferencResponseModel GetConferenceById(IBAEntities1 db, int id)
        {
            var conference = db.conf_conference.SingleOrDefault(r => r.id == id);
            if (conference != null)
            {
                return new ConferencResponseModel
                {
                    Start = AppSettings.ConferenceStart,
                    End = AppSettings.ConferenceEnd,
                    Venue = conference.venue,
                    Name = conference.title
                };
            }
            return new ConferencResponseModel();
        }
        //NOTE: Paging not currently supported in app.
        public static ConferenceBuildEventResponseModel GetConferenceWithBuildingEventsById(IbaCmsDbContext db, IBAEntities1 ibaDb, int conferenceId, decimal userId, int take = 20, int skip = 0)
        {
            var conference = ibaDb.conf_conference.SingleOrDefault(r => r.id == conferenceId);
            if (conference != null)
            {
                return new ConferenceBuildEventResponseModel
                {
                    Start = AppSettings.ConferenceStart,
                    End = AppSettings.ConferenceEnd,
                    Venue = conference.venue,
                    Name = conference.title,
                    Events = GetEventsForConferenceById(db,ibaDb,conferenceId,userId,take,skip),
                    Buildings = GetEventsForConferenceById(db,ibaDb,conferenceId,take,skip)
                };
            }
            return new ConferenceBuildEventResponseModel();
        }

        public static List<ConferenceBuildingResponseModel> GetEventsForConferenceById(IbaCmsDbContext db, IBAEntities1 ibaDb, int conferenceId, int take = 20, int skip = 0)
        {

            var buildings = db.EventBuildings.Where(r => r.ConferenceId == conferenceId).Select(t => new ConferenceBuildingResponseModel() {
                BuildingId = t.ScheduleEventBuildingId,
                BuildingName = t.Name,
                Floors = db.FloorNames.Where(r => r.Building.ScheduleEventBuildingId == t.ScheduleEventBuildingId).Select(x => new ConferenceBuildingFloorResponseModel()
                {
                    Name = x.Name,
                    FloorIndex = x.Floor
                }).ToList()
        }).ToList();
            
            return buildings;
        }

        public static List<ConferenceEventResponseModel> GetEventsForConferenceById(IbaCmsDbContext db, IBAEntities1 ibaDb, int conferenceId, decimal userId, int take = 20, int skip = 0)
        {
            var result = new List<ConferenceEventResponseModel>();
            var acceptableTypes = new List<decimal> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var conferenceFunctions =
                from cf in ibaDb.conf_function
                join ta in ibaDb.conf_function_ticket_allocation.DefaultIfEmpty()
                on new { ConferenceId = cf.conference_id, FunctionId = cf.id, GuestId = false, MemberId = userId }
                equals new { ConferenceId = (decimal)ta.ConferenceID, FunctionId = (decimal)ta.FunctionID, GuestId = ta.GuestID.HasValue, MemberId = (decimal)ta.MemberID }
                into temp
                from e in temp.DefaultIfEmpty()
                where cf.conference_id == conferenceId && acceptableTypes.Contains(cf.type_id) && cf.status_id == 2
                select new { cf.id, cf.function_start, cf.function_end, cf.title, cf.location, cf.description, Attending = e != null };
            var conferenceRooms = db.EventLocations.Where(r=>r.ConferenceId == conferenceId).ToList();
            foreach (var eventLocation in conferenceRooms)
            {

                var eventRoom = eventLocation.LocationName;
                var conferenceFunctionsForRoom =
                    conferenceFunctions.Where(cf => cf.location == eventRoom);
                foreach(var conferenceFunction in conferenceFunctionsForRoom)
                {
                    var start = new DateTime(conferenceFunction.function_start.Year, conferenceFunction.function_start.Month, conferenceFunction.function_start.Day, conferenceFunction.function_start.Hour,
                        conferenceFunction.function_start.Minute, conferenceFunction.function_start.Second, DateTimeKind.Utc);
                    var end = new DateTime(conferenceFunction.function_end.Year, conferenceFunction.function_end.Month, conferenceFunction.function_end.Day, conferenceFunction.function_end.Hour,
                        conferenceFunction.function_end.Minute, conferenceFunction.function_end.Second, DateTimeKind.Utc);

                    result.Add(new ConferenceEventResponseModel
                    {
                        ConferenceId = conferenceId,
                        StartTime = start,// conferenceFunction.function_start,
                        EndTime = end,//conferenceFunction.function_end,
                        EventItemId = (int)conferenceFunction.id,
                        Title = conferenceFunction.title,
                        TranslatedTitle = eventLocation.TranslatedTitle,
                        RoomName = conferenceFunction.location,
                        RoomCentreX = eventLocation.CentreX,
                        RoomCentreY = eventLocation.CentreY,
                        Lat = eventLocation.Lat,
                        Long = eventLocation.Long,
                        Floor = eventLocation.Floor,
                        SubTitle = conferenceFunction.description,
                        Attending = conferenceFunction.Attending,
                        BuildingId = eventLocation.Building.ScheduleEventBuildingId
                    });
                }

            }
            var existingLocations = conferenceRooms.Select(r => r.LocationName);
            var allLocations = conferenceFunctions.Select(r => r.location).Where(t=> !string.IsNullOrEmpty(t));

            return result.OrderBy(r => r.StartTime).Skip(skip).Take(take).ToList();
        }

        public static List<ConferenceRoomViewModel> GetConfRoomsForConference(IBAEntities1 ibaDb, int conferenceId)
        {
            var acceptableTypes = new List<decimal> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var funct = ibaDb.conf_function.Single(r => r.id == 29952);
            return ibaDb.conf_function
                .Where(r => r.conference_id == conferenceId && acceptableTypes.Contains(r.type_id) && r.status_id == 2 && !string.IsNullOrEmpty(r.location))
                .Select(cf => cf.location)
                .Distinct()
                .Select(l => new ConferenceRoomViewModel
                {
                    RoomName = l
                })
                .ToList();
        }

        public static void CreateScheduleEventLocationsIfDoesntExist(IbaCmsDbContext db, List<ConferenceRoomViewModel> conferenceRooms)
        {
            foreach (var room in conferenceRooms)
            {
                var existingRoom = db.EventLocations.Any(r => r.LocationName == room.RoomName);
                if (!existingRoom)
                {
                    var location = CreateScheduleEventLocation(room);
                    db.EventLocations.Add(location);
                }
            }
            db.SaveChanges();

        }

        public static ScheduleEventLocation CreateScheduleEventLocation(ConferenceRoomViewModel conferenceRoom)
        {
            return new IBA_Common.Models.ScheduleEventLocation()
            {
                LocationName = conferenceRoom.RoomName
            };
        }
    }
}
