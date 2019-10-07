var IBA = IBA || {};

IBA.CanvasZoom = {
    TEN: 0,
    TWENTYFIVE: 1,
    FIFTY: 2,
    SEVENTYFIVE: 3,
    HUNDRED: 4
}

var HotspotsCanvas = function () {
    this._hotspots = [];
    this._canvasContext;
    this._hasClicked;
    this._lastMousePosition = {};
    this._canvasIsValid = true;
    this._selectedBuilding = 0;
    this._eventsEnabled = true;
    this._rightOfHotspotCenter = false;
    this._topOfHotspotCenter = false;
    this._zoomAmount = IBA.CanvasZoom.HUNDRED;
    this._zoomFactor = 100;
    this._lastSelectedHotpot;
    this._indexOfLastSelectedHotspot;

    this.initialise = function (context, loadedHotspots, eventsEnabled) {
        _canvasContext = context;
        this.loadHotspots(loadedHotspots);
        _eventsEnabled = eventsEnabled;
        if (eventsEnabled) {
            context.canvas.onmousedown = this.mouseDownEvent;
            context.canvas.onmousemove = this.mouseMoveEvent;
            context.canvas.onmouseup = this.mouseUpEvent;
            context.canvas.onmouseleave = this.mouseLeaveEvent;
        }
        this.draw();

        return true;
    }

    this.loadHotspots = function (loadedHotspots) {
        for (var i = 0; i < loadedHotspots.length; i++) {
            var hotspot = new Hotspot(_canvasContext, loadedHotspots[i].LinkedLocation, loadedHotspots[i].LinkedType, loadedHotspots[i].HotspotId, _zoomAmount,100);
            hotspot.SetCentreX(loadedHotspots[i].CentreX, true);
            hotspot.SetCentreY(loadedHotspots[i].CentreY, true);
            hotspot.SetRadius(loadedHotspots[i].Radius);
            hotspot.SetTitle(loadedHotspots[i].SectionItemText);
            _hotspots.push(hotspot);
        }
    }

    this.createHotspot = function (id, linkedItemType, title) {
        var hotspot = new Hotspot(_canvasContext, id, linkedItemType, this._hotspots.length, this._zoomAmount, this._zoomFactor);
        hotspot.SetTitle(title);
        this._hotspots.push(hotspot);
        _lastSelectedHotspot = hotspot;
        _indexOfLastSelectedHotspot = this._hotspots.length - 1;
        this.checkForCollision(hotspot);
        this.draw();
        return hotspot;
    }

    this.draw = function () {
        _canvasContext.clearRect(0, 0, _canvasContext.canvas.width, _canvasContext.canvas.height);
        this._hotspots.forEach(this.drawHotspot);
        if (this._eventsEnabled)
            this.checkCanvasIsValid();
    }

    this.selectHotspot = function (id) {

        var oldHotspot = this.getSelectedHotspot()
        if (oldHotspot.length > 0) {
            oldHotspot[0].isSelected = false;
            oldHotspot[0].isMoving = false;
            oldHotspot[0].isResizing = false;
            oldHotspot[0].Draw();
        }
        
        var hotspot = this._hotspots.filter(function (hotspot) {
            return hotspot.linkedLocation === id
        })
        if (hotspot.length > 0) {
            hotspot[0].isSelected = true;
            hotspot[0].Draw();
        }
        
    }

    this.drawHotspot = function (element, index, array) {
 
        element.Draw();
    }

    this.checkMouseClickToHotspot = function (hotspot, event) {
        var hotspotCentreX = hotspot.centreX;
        var hotspotCentreY = hotspot.centreY;
        var mouseX = event.offsetX;
        var mouseY = event.offsetY;
        var distanceSquared = (mouseX - hotspotCentreX) * (mouseX - hotspotCentreX) + (mouseY - hotspotCentreY) * (mouseY - hotspotCentreY);

        if (distanceSquared <= (hotspot.radius - 10) * (hotspot.radius - 10)) {
            console.log("Seleced Hotspot:" + hotspot.sectionItemText);
            return "Move";

        }
        else if (distanceSquared <= hotspot.radius * hotspot.radius && distanceSquared > hotspot.radius - 10 * hotspot.radius - 10) {
            if (mouseX > hotspotCentreX) {
                _rightOfHotspotCenter = true;
            } else {
                _rightOfHotspotCenter = false;
            }
            if (mouseY > hotspotCentreY) {
                _topOfHotspotCenter = true
            } else {
                _topOfHotspotCenter = false
            }
            return "Resize";
        }
        return false;
    }

    this.mouseDownEvent = function (ev) {
        var canvas = event.CurrentBuilding().CurrentFloor().hotspotManager
        for (var i = 0; i < canvas._hotspots.length; i++) {
            switch (canvas.checkMouseClickToHotspot(canvas._hotspots[i], ev)) {
                case "Move":
                    var oldHotspot = canvas.getSelectedHotspot()
                    if (oldHotspot.length > 0) {
                        oldHotspot[0].isSelected = false;
                        oldHotspot[0].isMoving = false;
                        oldHotspot[0].isResizing = false;
                    }
                    canvas._hotspots[i].isMoving = true;
                    canvas._hotspots[i].isResizing = false;
                    canvas._hotspots[i].isSelected = true;
              
                    canvas._lastSelectedHotpot = canvas._hotspots[i];
                    canvas._indexOfLastSelectedHotspot = i
                    break;
                case "Resize":
                    canvas._hotspots[i].isResizing = true;
                    canvas._hotspots[i].isMoving = false;
                    canvas._lastSelectedHotpot = canvas._hotspots[i];
                    canvas._indexOfLastSelectedHotspot = i
                    break;
                default:
                    canvas._hotspots[i].isMoving = false;
                    canvas._hotspots[i].isResizing = false;
            }
        }
        canvas.draw();
        canvas._lastMousePosition = {
            x: ev.offsetX,
            y: ev.offsetY
        };
        canvas._hasClicked = true;
    }

    this.deselectCurrentHotspot = function () {
        var oldSelect = this.getSelectedHotspot()[0];
        oldSelect.isSelected = false;
        oldSelect.draw();
    }

    this.mouseUpEvent = function (ev) {
        var canvas = event.CurrentBuilding().CurrentFloor().hotspotManager
        canvas._hasClicked = false;
        canvas._lastMousePosition = {};
    }

    this.mouseMoveEvent = function (ev) {
        var canvas = event.CurrentBuilding().CurrentFloor().hotspotManager
        
        if (canvas._hasClicked) {
            var hotspot = canvas.getSelectedHotspot()

            if (hotspot.length > 0) {
                if (hotspot[0].isMoving) {
                    hotspot[0].SetCentreX(ev.offsetX, false);
                    hotspot[0].SetCentreY(ev.offsetY, false);
                }
                else {
                    if (typeof (canvas._lastMousePosition.x) != 'undefined') {
                        var deltaX = canvas._lastMousePosition.x - ev.offsetX;
                        var deltaY = canvas._lastMousePosition.y - ev.offsetY;
                    }
                    console.log("Last mouse x :" + canvas._lastMousePosition.x);
                    console.log("deltaX:" + deltaX);
                    var hotSpotRadius = hotspot[0].GetRadius();
                    if (canvas._rightOfHotspotCenter == true) {
                        if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX > 0) {
                            hotspot[0].SetRadius(hotSpotRadius - deltaX)
                        } else if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX < 0) {
                            hotspot[0].SetRadius(hotSpotRadius - deltaX)
                        }
                    }
                    else {
                        if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX > 0) {
                            hotspot[0].SetRadius(hotSpotRadius + deltaX)
                        } else if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX < 0) {
                            hotspot[0].SetRadius(hotSpotRadius + deltaX)
                        }
                    }
                    if (canvas._topOfHotspotCenter == true) {
                        if (Math.abs(deltaY) > Math.abs(deltaX) && deltaY > 0) {
                            hotspot[0].SetRadius(hotSpotRadius - deltaY)
                        } else if (Math.abs(deltaY) > Math.abs(deltaX) && deltaY < 0) {
                            hotspot[0].SetRadius(hotSpotRadius - deltaY)
                        }
                    } else {
                        if (Math.abs(deltaY) > Math.abs(deltaX) && deltaY > 0) {
                            hotspot[0].SetRadius(hotSpotRadius + deltaY)
                        } else if (Math.abs(deltaY) > Math.abs(deltaX) && deltaY < 0) {
                            hotspot[0].SetRadius(hotSpotRadius + deltaY)
                        }
                    }

                    canvas._lastMousePosition = {
                        x: ev.offsetX,
                        y: ev.offsetY
                    };
                }
                canvas.checkForCollision(hotspot[0]);
                canvas.draw();
            }
        }
    }

    this.mouseLeaveEvent = function (ev) {
        var canvas = event.CurrentBuilding().CurrentFloor().hotspotManager
        var selectedHotspot = canvas.getSelectedHotspot();
        if (selectedHotspot.length > 0) {
            selectedHotspot[0].isMoving = false;
            selectedHotspot[0].isResizing = false;
        }
    }

    this.getSelectedHotspot = function () {
        return this._hotspots.filter(function (hotspot) {
            return hotspot.isMoving || hotspot.isResizing || hotspot.isSelected;
        });
    }

    this.checkForCollision = function (hotspotToCheck) {

        var hotspotCentreX = hotspotToCheck.centreX;
        var hotspotCentreY = hotspotToCheck.centreY;
        var hasCollided = false;
        this._hotspots.filter(function (hotspot) {
            return hotspot.hotspotId != hotspotToCheck.hotspotId
        }).forEach(function (element, index, array) {
            var otherHotpotCentreX = element.GetPosition().centreX;
            var otherHotspotCentreY = element.GetPosition().centreY;

            var distanceSquared = Math.pow(element.GetPosition().centreX - hotspotCentreX, 2) + Math.pow(hotspotCentreY - element.GetPosition().centreY, 2);

            if ((distanceSquared <= Math.pow(hotspotToCheck.GetRadius() + element.GetRadius(), 2))) {
                element.SetHasCollided(true);
                hasCollided = true
            }
            else {
                element.SetHasCollided(false);
            }
        });
        hotspotToCheck.isColliding = hasCollided;
    }

    this.checkCanvasIsValid = function () {
        var invalidHotspots = this._hotspots.filter(function (hotspot) {
            return hotspot.isColliding;
        });

        
        $('.error-container').text('');
        $('.hotspot-alert').addClass('hidden');

        return true;
    }

    this.showHotspotError = function (message) {
        $('.alert').html(message);
        $('.hotspot-alert').removeClass('hidden');

    }

    this.changeZoomLevel = function (newZoomAmount) {
        var newZoomLevel;
        switch (newZoomAmount) {
            case "10":
                newZoomLevel = IBA.CanvasZoom.TEN;
                _zoomFactor = 10;
                break;
            case "25":
                newZoomLevel = IBA.CanvasZoom.TWENTYFIVE;
                _zoomFactor = 25;
                break;
            case "50":
                newZoomLevel = IBA.CanvasZoom.FIFTY;
                _zoomFactor = 50;
                break;
            case "75":
                newZoomLevel = IBA.CanvasZoom.SEVENTYFIVE;
                _zoomFactor = 75;
                break;
            case "100":
                newZoomLevel = IBA.CanvasZoom.HUNDRED;
                _zoomFactor = 100;
                break;
            default:
                _zoomFactor = 100;
                newZoomLevel = IBA.CanvasZoom.HUNDRED;
                break;
        }

        if (newZoomLevel !== _zoomAmount) {
            _zoomAmount = newZoomLevel;
            adjustToNewZoom();
            draw()
        }

    }

    this.adjustToNewZoom = function () {
        this._hotspots.forEach(function (hotspot) {
            hotspot.AdjustToZoom(_zoomAmount)
        });
    }

    this.removeLastSelectedHotspot = function () {
        _lastSelectedHotpot.centreY = 10000;
        this.checkForCollision(_lastSelectedHotpot);
        this.checkCanvasIsValid();
        this._hotspots.splice(_indexOfLastSelectedHotspot, 1);
        this.draw();
    }
}

HotspotsCanvas.prototype.Initialise = function (context, loadedHotspots, eventsEnabled) {
    this.initialise(context, loadedHotspots, eventsEnabled);
}
HotspotsCanvas.prototype.GetHotspots = function () {
    return this._hotspots;
}
HotspotsCanvas.prototype.GetCanvasWidth = function () {
    return this._canvasContext.canvas.width;
}
HotspotsCanvas.prototype.GetCanvasHeight = function () {
    return this._canvasContext.canvas.height;
}
HotspotsCanvas.prototype.PlaceHotspot = function (id, linkedType, title) {
    var hotspot = this.createHotspot(id, linkedType, title);
    return hotspot
}
HotspotsCanvas.prototype.GetCanvasObjectsForSave = function () {
    if (this.checkCanvasIsValid()) {
        return this._hotspots;
    }
}
HotspotsCanvas.prototype.ShowHotspotError = function (message) {
    this.showHotspotError(message);
}
HotspotsCanvas.prototype.ChangeZoomLevel = function (zoomAmount) {
    this.changeZoomLevel(zoomAmount)
}
HotspotsCanvas.prototype.RemoveLastSelectedHotpot = function() {
    this.removeLastSelectedHotspot()
}
HotspotsCanvas.prototype.Draw = function () {
    this.draw()
}
HotspotsCanvas.prototype.SelectHotspot = function (id) {
    this.selectHotspot(id)
}

function generateUUID() { 
    var d = new Date().getTime();
    if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
        d += performance.now(); //use high-precision timer if available
    }
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

var Hotspot = function (context, linkedLocation, linkedItemType, hotspotId, zoomLevel,zoomFactor) {
    this.hotspotId = hotspotId;
    this.unscaledRadius = 100;
    this.radius = zoomFactor;
    this.centreX = ((zoomFactor * context.canvas.width / 100) - this.radius) / 2;
    this.unscaledX = (context.canvas.width - this.unscaledRadius) / 2;
    this.centreY = ((zoomFactor * context.canvas.height / 100) - this.radius) / 2;
    this.unscaledY = (context.canvas.Height - this.unscaledRadius) / 2;
    this.colour = 0;
    this.linkedLocation = linkedLocation;
    this.linkedItemType = linkedItemType;
    this.isMoving = false;
    this.isResizing = false;
    this.canvasContext = context;
    this.sectionItemText = "N.A";
    this.isColliding = false;
    this.isSelected = true;
    this.pinImage = new Image();
    this.pinSelectedImage = new Image();
    this.pinImage.src = "../Images/icon_map_pin_now.png";
    this.pinSelectedImage.src = "../Images/icon_map_pin_next.png";
    this.zoomLevel = zoomLevel;

    this.isOffsite = false;
    this.lat = "";
    this.lon = "";
}

Hotspot.prototype.GetPosition = function () {
    return { centreX: this.centreX, centreY: this.centreY };
};

Hotspot.prototype.IsMoving = function () {
    return this.isMoving;
};

Hotspot.prototype.SetCentreX = function (centreX, isStart) {
    var boundsX = this.canvasContext.canvas.width
    console.log(boundsX);
    switch (this.zoomLevel) {
        case IBA.CanvasZoom.FIFTY:
            boundsX = boundsX * 0.5
            break;
        case IBA.CanvasZoom.TWENTYFIVE:
            boundsX = boundsX * 0.25
            break;
        case IBA.CanvasZoom.TEN:
            boundsX = boundsX * 0.10
            break;
        case IBA.CanvasZoom.SEVENTYFIVE:
            boundsX = boundsX * 0.75
            break;
        case IBA.CanvasZoom.HUNDRED:
            break;
    }

    if (centreX - this.radius > 0 && centreX + this.radius < boundsX) {

            switch (this.zoomLevel) {
                case IBA.CanvasZoom.FIFTY:
                    this.unscaledX = centreX / 0.50
                    break;
                case IBA.CanvasZoom.TWENTYFIVE:
                    this.unscaledX = centreX / 0.25
                    break;
                case IBA.CanvasZoom.TEN:
                    this.unscaledX = centreX / 0.10
                    break;
                case IBA.CanvasZoom.SEVENTYFIVE:
                    this.unscaledX = centreX / 0.75
                    break;
                case IBA.CanvasZoom.HUNDRED:
                    this.unscaledX = centreX
                    break;
            }
        this.centreX = centreX;
    }
    else if (isStart === true) {
        this.centreX = centreX;
    }
};

Hotspot.prototype.SetCentreY = function (centreY, isStart) {

    var boundsY = this.canvasContext.canvas.height
    switch (this.zoomLevel) {
        case IBA.CanvasZoom.FIFTY:
            boundsY = boundsY * 0.5
            break;
        case IBA.CanvasZoom.TWENTYFIVE:
            boundsY = boundsY * 0.25
            break;
        case IBA.CanvasZoom.TEN:
            boundsY = boundsY * 0.10
            break;
        case IBA.CanvasZoom.SEVENTYFIVE:
            boundsY = boundsY * 0.75
            break;
        case IBA.CanvasZoom.HUNDRED:
            break;
    }
    if (centreY - this.radius > 0 && centreY + this.radius < boundsY) {
        switch (this.zoomLevel) {

            case IBA.CanvasZoom.SEVENTYFIVE:
                this.unscaledY = centreY / 0.75
                break;
            case IBA.CanvasZoom.FIFTY:
                this.unscaledY = centreY / 0.50
                break;
            case IBA.CanvasZoom.TWENTYFIVE:
                this.unscaledY = centreY / 0.25
                break;
            case IBA.CanvasZoom.TEN:
                this.unscaledY = centreY / 0.10
                break;
            case IBA.CanvasZoom.HUNDRED:
                this.unscaledY = centreY
                break;
        }

        this.centreY = centreY;
    }
    else if (isStart === true) {
        this.centreY = centreY;
    }
};

Hotspot.prototype.SetTitle = function (title) {
    this.sectionItemText = title;
};

Hotspot.prototype.SetRadius = function (radius) {
    var boundsY = this.canvasContext.canvas.height
    var boundsX = this.canvasContext.canvas.width
    switch (this.zoomLevel) {
        case IBA.CanvasZoom.FIFTY:
            boundsY = boundsY * 0.5
            boundsX = boundsX * 0.5
            break;
        case IBA.CanvasZoom.TWENTYFIVE:
            boundsY = boundsY * 0.25
            boundsX = boundsX * 0.25
            break;
        case IBA.CanvasZoom.TEN:
            boundsY = boundsY * 0.10
            boundsX = boundsX * 0.10
            break;
        case IBA.CanvasZoom.SEVENTYFIVE:
            boundsY = boundsY * 0.75
            boundsX = boundsX * 0.75
            break;
        case IBA.CanvasZoom.HUNDRED:
            break;
    }
    var inBoundsLeftRight = (this.centreX - radius > 0 && this.centreX + radius < boundsX);
    var inBoundsUpDown = (this.centreY - radius > 0 && this.centreY + radius < boundsY);

    if (radius > 60 && inBoundsLeftRight && inBoundsUpDown) {

        switch (this.zoomLevel) {
            case IBA.CanvasZoom.SEVENTYFIVE:
                this.unscaledRadius = radius / 0.75
                break;
            case IBA.CanvasZoom.FIFTY:
                this.unscaledRadius = radius / 0.50
                break;
            case IBA.CanvasZoom.TWENTYFIVE:
                this.unscaledRadius = radius / 0.25
                break;
            case IBA.CanvasZoom.TEN:
                this.unscaledRadius = radius / 0.10
                break;
            case IBA.CanvasZoom.HUNDRED:
                this.unscaledRadius = radius
                break;
        }
        this.radius = radius;
    }
        
};

Hotspot.prototype.GetRadius = function () {
    return this.radius;
};

Hotspot.prototype.Draw = function () {
    var currentFontSize = 20;
    while (!this.WillTextFitInHotspot(currentFontSize)) {
        currentFontSize = currentFontSize - 1
        if (currentFontSize < 5)
            break;
    }
    var pinWidth = this.pinImage.width / 2;
    var pinHeight = this.pinImage.height / 2;
    var pinXpos = this.centreX - (pinWidth / 2);
    var pinYpos = this.centreY - (pinHeight);
    if (this.isSelected) {
        this.canvasContext.drawImage(this.pinSelectedImage, pinXpos, pinYpos, this.pinImage.width / 2, this.pinImage.height / 2);
    } else {
        this.canvasContext.drawImage(this.pinImage, pinXpos, pinYpos, this.pinImage.width / 2, this.pinImage.height / 2);
    }
    this.canvasContext.textAlign = "center";

    this.canvasContext.fillText(this.sectionItemText, this.centreX, this.centreY);

};

Hotspot.prototype.GetLinkedId = function () {
    return this.linkedLocation;
};

Hotspot.prototype.SetHasCollided = function (hasCollided) {
    this.isColliding = hasCollided;
};

Hotspot.prototype.WillTextFitInHotspot = function (fontSize) {
    this.canvasContext.font = fontSize.toString() + "px Arial";
    var metrics = this.canvasContext.measureText(this.sectionItemText);
    return metrics.width + 20 < (this.radius * 2);
}

Hotspot.prototype.AdjustToZoom = function (zoomAmount) {
    switch (zoomAmount) {
        case IBA.CanvasZoom.TEN:
            this.centreX = this.unscaledX * 0.10
            this.centreY = this.unscaledY * 0.10
            this.radius = this.unscaledRadius * 0.10
            break;
        case IBA.CanvasZoom.TWENTYFIVE:
            this.centreX = this.unscaledX * 0.25
            this.centreY = this.unscaledY * 0.25
            this.radius = this.unscaledRadius * 0.20
            break;
        case IBA.CanvasZoom.FIFTY:
            this.centreX = this.unscaledX * 0.50
            this.centreY = this.unscaledY * 0.50
            this.radius = this.unscaledRadius * 0.50
            break;
        case IBA.CanvasZoom.SEVENTYFIVE:
            this.centreX = this.unscaledX * 0.75
            this.centreY = this.unscaledY * 0.75
            this.radius = this.unscaledRadius * 0.75
            break;
        case IBA.CanvasZoom.HUNDRED:
            this.centreX = this.unscaledX
            this.centreY = this.unscaledY
            this.radius = this.unscaledRadius
            break;
    }
    this.zoomLevel = zoomAmount
    console.log("Current CentreX = " + this.centreX)
    console.log("Fullscale centre X = " + this.unscaledX)
}






