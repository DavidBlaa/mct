$("#plus").click(function () {
    //zoom in
    zoom(getSnap(), 0.1);
})

$("#minus").click(function () {
    // zoom out
    zoom(getSnap(), -0.1);
})

$("#scrolleft").click(function () {

    console.log(getSvgContainer());
    console.log($(getSvgContainer()).scrollLeft());
    var currentPositon = $(getSvgContainer()).scrollLeft();

    $(getSvgContainer()).scrollLeft(currentPositon - 10);
})

$("#scrollright").click(function () {
    var currentPositon = $(getSvgContainer()).scrollLeft();
    $(getSvgContainer()).scrollLeft(currentPositon + 10);
})

// zoom a svg object, position directionvalue = zoom in, negative = zoom out
function zoom(svgObj, directionValue) {
    var s = svgObj;

    var scaleX = s.transform().localMatrix.a + directionValue;
    var scaleY = s.transform().localMatrix.d + directionValue;



    var x = s.transform().localMatrix.e;
    var y = s.transform().localMatrix.f;

    if (scaleX > 1 || scaleY > 1) {

       x = (width * (scaleX - 1)) / 2;
       y = (height * (scaleY - 1)) / 2;

    }
    else {
        x = 0;
        y = 0;
    }

    s.attr({
        transform: "martix(" + scaleX + "," + s.transform().localMatrix.b + "," + s.transform().localMatrix.c + "," + scaleY + "," + x + "," + y + ")"
    })

    var newTransform = s.transform().localMatrix;
    console.log("nT: " + newTransform);

}

$(".add-plant-to-patch-bt").click(function (e) {

    //alert("click");
    var id = $(e.currentTarget).attr("plantid");

    var patchid = $($(e.currentTarget).parents(".patch-planer-container")[0]).attr("id");

    console.log(id);
    console.log(patchid);

    $.get("/PatchPlaner/AddPlant", { id: id, patchId: patchid},
        function (data, textStatus, jqXHR) {

            var s = getSnap();

            // parse partial view to svg element
            var fragement = Snap.parse(data);
            console.log(fragement);

            // add svg elemengt to svg
            var x = s.append(fragement);

            // set drag to the new last plant object
            var all = s.selectAll(".pflanze");
            console.log(all);
            var last = all[all.length - 1];
            setDragElements(last);

            setEvents(last);
        }
    );
})

function addPlant(e) {

    //;
    var id = $(e).attr("plantid");

    var patchid = $(e).attr("patchid");

    console.log(id);
    console.log(patchid);

    $.get("/PatchPlaner/AddPlant", { id: id, patchId: patchid },
        function (data, textStatus, jqXHR) {

            var s = getSnap();

            // parse partial view to svg element
            var fragement = Snap.parse(data);
            console.log(fragement);

            // add svg elemengt to svg
            var x = s.append(fragement);

            // set drag to the new last plant object
            var all = s.selectAll(".pflanze");
            console.log(all);
            var last = all[all.length - 1];
            setDragElements(last);

            setEvents(last);
        }
    );
}

function deletePlacementBt(e) {


    console.log(".delete-placement-btn");
    console.log(e);
    //console.log(e.currentTarget);

    var selectedObj = e;

    //remove
    var id = $(selectedObj).attr("placementid");
    var patchid = $(selectedObj).attr("patchid");

    data = {
        id: id,
        patchid: patchid
    }

    $.ajax({
        type: "POST",
        url: "/PatchPlaner/RemovePlacement",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response == true) {

                //remove g from svg
                var parent = $(selectedObj).parents(".pflanze")[0];
                $(parent).remove();


            }
            else {
                alert(response);
            }
        }
    });
}

var s = getSnap();

// grid size
var gridsize = 10;

// draw grid
var width = $("#x").attr("width");
var height = $("#x").attr("height");
drawLines(s, width, height, gridsize)

// get all plants
var testObjs = s.selectAll(".pflanze");

// set drag to loaded plant objects
$.each(testObjs, function (index, value) {
    //alert("each");
    

    setEvents(value);
    setDragElements(value);

    //set dbclick event
    //value.click(dbclick(value))

    
});

function setEvents(value) {

    value.mouseover(function (o) {

        getSnap().append(this);
        $(this.node).find(".additional-options").show();
        //console.log("mouseover");
    });

    value.mouseout(function (o) {

        $(this.node).find(".additional-options").hide();
        //console.log("mouseout");
    });

    value.mouseup(function (o) {

        //this.undrag();
        //sconsole.log("mouseup");
    });

    value.mousedown(function (o) {

        //console.log(o);

        console.log("mousedown");

        if (o.button == 0) {
            console.log("left");
            this.undrag();
        }

        if(o.button == 1) {
            console.log("1")
        }

        if (o.button == 2) {
            console.log("2")
        }
        
    });

    value.click(function (o) {

        console.log("click");

        if (o.button == 0) {
            console.log("left");
            setDragElements(this);
        }

        
    });

    value.dblclick(function (o) {

        console.log("dblclick");

    });

}


// set drag to plant object
function setDragElements(obj) {

    var move = function (dx, dy) {

        var paperScaleX = this.paper.transform().localMatrix.a;
        var paperScaleY = this.paper.transform().localMatrix.d;

        this.attr({
            transform: this.data('origTransform') + (this.data('origTransform') ? "T" : "t") + [dx / paperScaleX, dy / paperScaleY]
        });

        //get current coordinates
        var x = this.transform().localMatrix.e;
        var y = this.transform().localMatrix.f;

        // check if new x or y is out of the box

        var o = this.select("#r");
        //console.log(o);

        var width = o.attr("width");
        var height = o.attr("height");
        var maxX = this.paper.attr("width");
        var maxY = this.paper.attr("height");

        if (x < 0) x = 0;
        if (x >= (maxX - width)) x = (maxX - width);


        if (y < 0) y = 0;
        if (y >= (maxY - height)) y = (maxY - height);
        //console.log("x:" + x + " y:" + y);
        //console.log("maxX:" + maxX + " maxY:" + maxY);
        //console.log("height:" + height + " maxYwidth:" + width);
        //console.log("SnapX:"+xSnap+" SnapY:"+ ySnap);


        //console.log("x:" + x + " y:" + y);
        this.transform("t" + x + "," + y);

        // set dropzone
        var xSnap = Snap.snapTo(gridsize, x, 100000);
        var ySnap = Snap.snapTo(gridsize, y, 100000);
        //console.log("x:" + x + " y:" + y);
        //console.log("dx:" + dx + " dy:" + dy);
        //console.log("SnapX:"+xSnap+" SnapY:"+ ySnap);

        var s = getSnap();
        var tmp = s.select(".dropzone");
        tmp.attr({
            stroke: "#eee",
            strokeWidth: 1,
            fill: "#eee",
        })
        $(tmp).show();
        tmp.transform("t" + xSnap + "," + ySnap);

        //console.log("-----------------------------");
        //console.log(this.transform().localMatrix);
        //console.log(tmp.transform().localMatrix);
    }

    var start = function () {
        console.log("start drag");
        this.paper.undrag();
        this.data('origTransform', this.transform().local);

        getSnap().append(this);

        createTmpRec(this);

    }

    var stop = function (e) {

        var x = this.transform().localMatrix.e;
        var y = this.transform().localMatrix.f;


        var xSnap = Snap.snapTo(gridsize, x, 100000);
        var ySnap = Snap.snapTo(gridsize, y, 100000);

        this.attr({
            transform: "martix(1,0,0,1," + xSnap + "," + ySnap + ")"
        })

        //console.log(this.transform().localMatrix.e + 'x' + this.transform().localMatrix.f);
        //this.paper.drag();

        //remove tmp rectangle
        //var tmp = getSnap().select("#dropzone");
        //tmp.remove();

        $(".dropzone").remove();

        detectNeighbors(this);
    }

    obj.drag(move, start, stop);

}


//detect Neighbors
function detectNeighbors(placement) {

    var x = placement.transform().localMatrix.e;
    var y = placement.transform().localMatrix.f;

    var width = getWidth(placement);
    var height = getHeight(placement);

    var xmax = x + width;
    var ymax = y + height;

    var arr = new Array();
    // select all placements in patch
    //console.log("each plants");
    
    var allPlacemenets = getSnap().selectAll(".pflanze");
    //console.log(allPlacemenets)

    $.each(allPlacemenets, function (index, value) {

        if (value.attr("id") !== placement.attr("id")) {
            //console.log(value.attr("id"));
            //console.log(placement.attr("id"));

            var obj = value;
            
            // compare x,y,xmax, ymax
            var xTmp = obj.transform().localMatrix.e;
            var yTmp = obj.transform().localMatrix.f;

            var widthTmp = getWidth(obj);
            var heightTmp = getHeight(obj);
            

            var xmaxTmp = xTmp + widthTmp;
            var ymaxTmp = yTmp + heightTmp;

            

            var isIn = false;
            // oben links
            if ((x <= xTmp && xTmp <= xmax) && (y <= yTmp && yTmp <= ymax)) {
                isIn = true;
            }

            //oben rechts
            if ((x <= xmaxTmp && xmaxTmp <= xmax) && (y <= yTmp && yTmp <= ymax)) {
                isIn = true;
            }
            //unten links
            if ((x <= xTmp && xTmp <= xmax) && (y <= ymaxTmp && ymaxTmp <= ymax)) {
                isIn = true;
            }

            //unten rechts
            if ((x <= xmaxTmp && xmaxTmp <= xmax) && (y <= ymaxTmp && ymaxTmp <= ymax)) {
                isIn = true;
            }

            if (isIn) {

                //console.log("w:" + widthTmp + "h:" + heightTmp);
                //console.log("coord");
                //console.log("x: " + x + "xmax: " + xmax + " y: " + y + " ymax: " + ymax );

                //console.log("x: " + xTmp + " xmax: " + xmaxTmp + "y: " + yTmp + " ymax: " + ymaxTmp);

                arr.push(obj);
            }

        }

    });

    console.log("List of neighbors");
    console.log(arr);

}


// draw lines
function drawLines(s, maxX, maxY, gridSize) {
    var group = s.g();
    var line;
    // draw x
    for (var i = 0, l = parseInt(maxX / gridSize) + 1; i < l; i++) {
        var x = gridSize * i;

        line = s.line(x, 0, x, maxY);
        line.attr({
            stroke: "#eee",
            strokeWidth: 1
        })
        group.add(line);
    }
    console.log(parseInt(maxY / gridSize));
    for (var j = 0, le = parseInt(maxY / gridSize) + 1; j < le; j++) {
        var y = gridSize * j;

        line = s.line(0, y, maxX, y);
        line.attr({
            stroke: "#eee",
            strokeWidth: 1
        })

        group.add(line);

    }
    //console.log(maxY);
    //console.log(parseInt(maxY / gridSize));
    //console.log(maxX);
    //console.log(parseInt(maxX / gridSize));
    group.attr({ id: "raster" })
    s.prepend(group);


}

//create dropzone Rectangle
function createTmpRec(obj) {
    //var x = obj.transform().localMatrix.e;
    //var y = obj.transform().localMatrix.f;
    //console.log(obj);

    var o = obj.select("#r");
    //console.log(o);

    var width = o.attr("width");
    var height = o.attr("height");

    var s = getSnap();

    var tmp = s.rect(0, 0, width, height).attr({
        class: "dropzone",
        fill:"white"
    })



    $(tmp).hide();

    s.prepend(tmp);

}

//get svg container With Snap functionality
function getSnap() {
    return Snap("#x");
}

function getSvgContainer() {
    return $("#svgContainer")[0];
}

function getWidth(placement) {

    var o = placement.select("#r");
    //console.log(o);
    var width = o.attr("width");
    return parseInt(width);
}

function getHeight(placement) {

    var o = placement.select("#r");
    //console.log(o);
    var height = o.attr("height");
    return parseInt(height);
}

//*MOUSE SCROLL DRAG*//