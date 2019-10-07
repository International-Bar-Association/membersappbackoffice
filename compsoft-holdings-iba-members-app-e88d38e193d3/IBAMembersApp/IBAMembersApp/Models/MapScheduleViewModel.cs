using IBAMembersApp.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.Models
{
    public class MapScheduleViewModel
    {
        public string VenueName { get; set; }
        public List<ConferenceRoomViewModel> Rooms { get; set; }
    }

    public class ScheduleEventLocationViewModel
    {
        public string EventName { get; set; }
        public List<ScheduleEventLocationItemViewModel> Locations { get; set; }
    }

    public class ScheduleEventLocationItemViewModel
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public int BuildingId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public string FloorName { get; set; }
        public int CentreX { get; set; }
        public int CentreY { get; set; }
        public bool IsOffsite { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string TranslatedTitle { get; set; }
    }
}