using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class ConferencResponseModel
    {
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public List<ConferenceBuildingResponseModel> Buildings { get; set; }
        public List<ConferenceEventResponseModel> Events { get; set; }

    }

    public class ConferenceBuildEventResponseModel: ConferencResponseModel
    {
        public List<ConferenceBuildingResponseModel> Buildings { get; set; }
        public List<ConferenceEventResponseModel> Events { get; set; }

    }

    public class ConferenceEventResponseModel
    {
        public int EventItemId { get; set; }
        public int ConferenceId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string Title { get; set; }
        public string TranslatedTitle { get; set; }
        public string SubTitle { get; set; }
        public bool Attending { get; set; }
        public string RoomName { get; set; }
        public int RoomCentreX { get; set; }
        public Double Lat { get; set; }
        public Double Long  { get; set; }
        public int RoomCentreY { get; set; }
        public int Floor { get; set; }

        public int? BuildingId { get; set; }
    }

    public class ConferenceBuildingResponseModel
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public List<ConferenceBuildingFloorResponseModel> Floors { get; set; }
    }

    public class ConferenceBuildingFloorResponseModel
    {
        public string Name { get; set; }
        public int FloorIndex { get; set; }
    }

    public class ConferenceRoomViewModel
    {
        [Obsolete("(2017-08-10) Shouldn't link to the conf_room, locations should be string -> conf_function.location.")]
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public int CentreX { get; set; }
        public int CentreY { get; set; }
    }
}