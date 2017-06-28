
/********** EDIT PLANT *******/

function savePlant() {

    //get Timeperiods list
    //string listId = type + "-list";

    var bloom = $("#bloom-list");
    var sowing = $("#sowing-list");
    var harvest = $("#harvest-list");
    var seedMaturity = $("#seedmaturity-list");

    var precultures = $(".preculture .simplelinkmodel-list");
    var aftercultures = $(".afterculture .simplelinkmodel-list");

    var tmp = getSimpleLinksJSON(precultures);
    console.log("tmp -> preculture");
    console.log(tmp);

    var plant = {
        Id: $("#plant #Id").val(),
        Name: $("#plant #Name").val(),
        ScientificName: $("#plant #ScientificName").val(),
        Rank: $("#plant #TaxonRank").val(),
        Description: $("#plant #Description").val(),
        Width: $("#plant #Width").val(),
        Height: $("#plant #Height").val(),
        RootDepth: $("#plant #RootDepth").val(),
        NutrientClaim: $("#plant #NutrientClaim").val(),
        SowingDepth: $("#plant #SowingDepth").val(),
        Bloom: getTimePeriodsJSON(bloom),
        Sowing: getTimePeriodsJSON(sowing),
        Harvest: getTimePeriodsJSON(harvest),
        SeedMaturity: getTimePeriodsJSON(seedMaturity),
        PreCultures: getSimpleLinksJSON(precultures),
        AfterCultures: getSimpleLinksJSON(aftercultures)
    }

    console.log(plant);

    $.ajax({
        type: "POST",
        url: "/Search/SavePlant",
        data: plant,
        dataType: "json",
        success: function (response) {

        }
    });
}

function getTimePeriodsJSON(source) {
    var JSONArray = [];
    var list = $(source).find("li").each(function () {

        var tp = getTimePeriodJSON(this);
        JSONArray.push(tp);
    });

    return JSONArray;
}


function getTimePeriodJSON(e) {
    console.log("e: " + e);
    var tp = {
        StartArea: $(e).find(".start-area").find("select").val(),
        StartMonth: $(e).find(".start-month").find("select").val(),
        EndArea: $(e).find(".end-area").find("select").val(),
        EndMonth: $(e).find(".end-month").find("select").val()
    }

    return tp;
}


function getSimpleLinksJSON(source) {

    var JSONArray = [];
    var list = $(source).find("li").each(function () {

        var simplelink = getSimpleLinkJSON(this);
        JSONArray.push(simplelink);
    });

    return JSONArray;
}

function getSimpleLinkJSON(e) {

    /*     public long Id { get; set; }
    public String Name { get; set; }
    public SubjectType Type { get; set; }
*/
    console.log($(e));

    var simplelink = {
        Id: $(e).find("#Id").val(),
        Name: $(e).find("#Name").val(),
        Type: $(e).find("#Type").val()
    }

    return simplelink;
}




/*** TIME PERIODS ***/

function addTP(e) {

    var newElement = $('<li class="timeperiod-li">');

    $.get("/Search/GetEmptyTimePeriod", function (data) {

        newElement.prepend($(data));
        //console.log(newElement);
        var list = $(e).parent().find("ul");
        //console.log("list");
        //console.log(list);
        $(list).append(newElement);

    });
}

function removeTP(e) {

    //console.log("e --->");
    //console.log(e);
    $(e).parents(".timeperiod-li")[0].remove();
}

/*** TIME PERIODS ***/

function addSimpleLink(e) {

    var newElement = $('<li class="timeperiod-li">');

    $.get("/Search/GetEmptySimpleLink", function (data) {

        newElement.prepend($(data));
        //console.log(newElement);
        var list = $(e).parent().find("ul");
        console.log("list");
        
        $(list).append(newElement);
        console.log(list);

    });
}

function removeSimpleLink(e) {

    console.log("e --->");
    console.log(e);
    $(e).parents(".simplelinkmodel-li")[0].remove();
}