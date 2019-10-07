

var ConferenceEvent = function (name) {
    this.name = name
    this.buildings = [];
    this.currentBuilding = -1;
}

ConferenceEvent.prototype.GetBuildings = function () {
    return this.buildings;
}

ConferenceEvent.prototype.AddBuilding = function (name) {
    var newBuilding = new Building(name, this.imageCanvas);
    this.buildings.push(newBuilding);
    this.SwitchBuilding(newBuilding.id);
    return newBuilding;
    
}

ConferenceEvent.prototype.SwitchBuilding = function (guid) {
    var building = this.buildings.filter(building => building.id === guid);
    var index = this.FindIndexOfBuilding(guid);
    this.currentBuilding = index;
    this.buildings[this.currentBuilding].ReloadBuilding();
}

ConferenceEvent.prototype.FindIndexOfBuilding = function (guid) {
    var result = -1;
    this.buildings.some(function (x, index) {
        if (x.id === guid) {
            result = index
            return true;
        }
    });
    return result;
}


ConferenceEvent.prototype.GetBuldingsLocationsAndHotspots = function () {
    var objToReturn = [];
    for (var building in this.buildings) {
        var buildingObj = {};
        var build = this.buildings[building]
        buildingObj.buildingName = build.name
        buildingObj.floors = []
        for (var floor in build.floors) {
            var fl = build.floors[floor]
            buildingObj.floors.push(fl.GetHotspotManger().GetHotspots());
        }
        objToReturn.push(buildingObj);
    }
    return objToReturn
}

ConferenceEvent.prototype.CurrentBuilding = function () {
    return this.buildings[this.currentBuilding]
}

var Building = function (name) {
    this.name = name
    this.id = generateUUID()
    this.floors = []
    this.currentFloor = -1;
}

Building.prototype.AddFloor = function (imageCanvas, hotspotCanvas) {
    var number = this.floors.length
    var floor = new Floor(number, imageCanvas, hotspotCanvas)
    this.floors.push(floor);
    return floor
}

Building.prototype.CurrentFloor = function () {
    return this.floors[this.currentFloor];
}

Building.prototype.GetFloors = function () {
    return this.floors;
}

Building.prototype.SwitchFloors = function (guid) {
    var floor = this.floors.filter(floor => floor.id === guid);
    var index = this.FindIndexOfFloor(guid);
    this.currentFloor = index;
    this.floors[this.currentFloor].ReloadFloor();
}

Building.prototype.FindIndexOfFloor = function (guid) {
    var result = -1;
    this.floors.some(function (x, index) {
        if (x.id === guid) {
            result = index
            return true;
        }
    });
    return result;
}


Building.prototype.ReloadBuilding = function () {
    if (this.currentFloor != -1) {
        this.CurrentFloor().ReloadFloor();
    }
    
}

var Floor = function (number,imageCanvas,hotspotCanvas) {
    this.number = number;
    this.id = generateUUID()
    this.floorImage = new Image();
    this.imageCanvas = imageCanvas
    this.imageContext = imageCanvas.getContext('2d');
    this.hotspotCanvas = hotspotCanvas
    this.hotspotContext = hotspotCanvas.getContext('2d');
    this.hotspotManager = new HotspotsCanvas();
    this.hotspotManager.Initialise(this.hotspotContext, [], true);

    this.floorImage.onload = function () {

        imageCanvas.width = this.width;
        imageCanvas.height = this.height;
        hotspotCanvas.width = this.width;
        hotspotCanvas.height = this.height;
        imageCanvas.getContext('2d').drawImage(this, 0, 0, this.width, this.height, 0, 0, this.width, this.height);
    };
}

Floor.prototype.GetHotspotManger = function () {
    return this.hotspotManager;
};

Floor.prototype.ReloadFloor = function () {
    this.imageCanvas.width = this.floorImage.width;
    this.imageCanvas.height = this.floorImage.height;
    this.hotspotCanvas.width = this.floorImage.width;
    this.hotspotCanvas.height = this.floorImage.height;
    this.imageContext.drawImage(this.floorImage, 0, 0, this.floorImage.width, this.floorImage.height, 0, 0, this.floorImage.width, this.floorImage.height);
    this.hotspotManager.Draw()
}
