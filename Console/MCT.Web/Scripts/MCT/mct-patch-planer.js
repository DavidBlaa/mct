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

        var d = scaleX - 1;
        d = d * 1000;
        x = d;
        console.log(x);
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

$("#add").click(function () {

    $.get("/PatchPlaner/AddRandomPlant", {id :1},
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
        }
    );
})

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
    setDragElements(value);
});


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
        console.log(o);

        var width = o.attr("width");
        var height = o.attr("height");
        var maxX = this.paper.attr("width");
        var maxY = this.paper.attr("height");

        if (x < 0) x = 0;
        if (x >= (maxX - width)) x = (maxX - width);


        if (y < 0) y = 0;
        if (y >= (maxY - height)) y = (maxY - height);
        console.log("x:" + x + " y:" + y);
        console.log("maxX:" + maxX + " maxY:" + maxY);
        console.log("height:" + height + " maxYwidth:" + width);
        //console.log("SnapX:"+xSnap+" SnapY:"+ ySnap);


        console.log("x:" + x + " y:" + y);
        this.transform("t" + x + "," + y);

        // set dropzone
        var xSnap = Snap.snapTo(gridsize, x, 100000);
        var ySnap = Snap.snapTo(gridsize, y, 100000);
        //console.log("x:" + x + " y:" + y);
        //console.log("dx:" + dx + " dy:" + dy);
        //console.log("SnapX:"+xSnap+" SnapY:"+ ySnap);

        var s = getSnap();
        var tmp = s.select("#dropzone");
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

        console.log(this.transform().localMatrix.e + 'x' + this.transform().localMatrix.f);
        //this.paper.drag();

        //remove tmp rectangle
        var tmp = getSnap().select("#dropzone");
        tmp.remove();

    }

    obj.drag(move, start, stop);

}

// draw lines
function drawLines(s, maxX, maxY, gridSize) {
    var group = s.g();

    // draw x
    for (var i = 0, l = parseInt(maxX / gridSize) + 1; i < l; i++) {
        var x = gridSize * i;

        var line = s.line(x, 0, x, maxY);
        line.attr({
            stroke: "#eee",
            strokeWidth: 1
        })
        group.add(line);
    }
    console.log(parseInt(maxY / gridSize));
    for (var i = 0, l = parseInt(maxY / gridSize) + 1; i < l; i++) {
        var y = gridSize * i;

        var line = s.line(0, y, maxX, y);
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
    console.log(obj);

    var o = obj.select("#r");
    console.log(o);

    var width = o.attr("width");
    var height = o.attr("height");

    var s = getSnap();

    var tmp = s.rect(0, 0, width, height).attr({
        id: "dropzone"
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


//*MOUSE SCROLL DRAG*//