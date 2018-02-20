
$(function () {

    //set file change event on images uploader
    $('.file-input')
        .on('change',
            function () {
                previewImage(this);
            });
});


/********** EDIT PLANT *******/

function deleteSubject(e)
{
    data = {
        id: e
    }

    $.ajax({
        type: "POST",
        url: "/Subject/DeleteNode",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response == true) {

            }
            else {
                alert(response);
            }
        }
    });

}

function deleteSubjectInSearch(e) {
    data = {
        id: e
    }

    $.ajax({
        type: "POST",
        url: "/Subject/DeleteNode",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response == true) {

                $("#" + e).remove();
                window.location.href = "/Search/Search";
            }
            else {
                alert(response);
            }
        }
    });

}

function savePlant() {

    //alert("start saving");

    if ($("form").valid()) {

        //get Timeperiods list
        //string listId = type + "-list";

        var timeperiods = $("#timeperiod-list");
        var lifecycles = $("#lifecycle-list")

        var precultures = $(".preculture .simplelinkmodel-list");
        var aftercultures = $(".afterculture .simplelinkmodel-list");

        //all tr of table
        //var interactionsFromSide = $(".interactions-table tr.interaction-row");

        console.log(timeperiods);
        console.log(precultures);
        console.log(aftercultures);
        //console.log(interactionsFromSide);

        //var tmp = getSimpleLinksJSON(precultures);
        //console.log("tmp -> preculture");
        //console.log(tmp);
        //alert("test");
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
            LocationType: $("#plant #LocationType").val(),
            LifeCycles: getLifeCycle(lifecycles, $("#plant #Id").val()),

            PreCultures: getSimpleLinksJSON(precultures),
            AfterCultures: getSimpleLinksJSON(aftercultures)
        };

        console.log(plant);

        //var interactions = getInteractionsJSON(interactionsFromSide);


        var data = {
            plantModel: plant,
            //interactions: interactions
        };

        //alert("DATA:");
        console.log(data);
        //alert("bevor save");
        $.ajax({
            type: "POST",
            url: "/Subject/SavePlant",
            data: data,
            dataType: "html",
            success: function(response) {


                //alert(response);
                var image = getImage();

                if (image) {
                    //upload image
                    $.ajax({
                        type: "POST",
                        url: '/Subject/SaveImage?id=' + response,
                        contentType: false,
                        processData: false,
                        data: image,
                        success: function (result) {
                            console.log(result);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                        }
                    });
                }
                window.location.href = "/Subject/Details?id=" + response + "&type=Plant";
            }
        });
    }
}

function saveAnimal() {


    if ($("form").valid()) {

        var lifecycles = $("#lifecycle-list");

        console.log("test");

        var animal = {
            Id: $($("#animal #Id")).val(),
            Name: $($("#animal #Name")).val(),
            ScientificName: $($("#animal #ScientificName")).val(),
            Rank: $($("#animal #TaxonRank")).val(),
            Description: $($("#animal #Description")).val(),
            LifeCycles: getLifeCycle(lifecycles, $($("#animal #Id")).val()),

        };


        console.log(animal);

        var data = {
            animalModel: animal,
        };


        $.ajax({
            type: "POST",
            url: "/Subject/SaveAnimal",
            data: data,
            dataType: "html",
            success: function (response) {
                

                var image = getImage();

                if (image) {
                    //upload image
                    $.ajax({
                        type: "POST",
                        url: '/Subject/SaveImage?id=' + response,
                        contentType: false,
                        processData: false,
                        data: image,
                        success: function (result) {
                            console.log(result);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                        }
                    });
                }

                window.location.href = "/Subject/Details?id=" + response + "&type=Animal";
            }
        });
    }
}

function saveTaxon(e) {

    if ($("form").valid()) {
        var taxon = {
            Id: $("#taxon #Id").val(),
            Name: $("#taxon #Name").val(),
            ScientificName: $("#taxon #ScientificName").val(),
            Rank: $("#taxon #TaxonRank").val(),
            Description: $("#taxon #Description").val()
        };

        var data = {
            taxon: taxon,
        };


        $.ajax({
            type: "POST",
            url: "/Subject/SaveTaxon",
            data: data,
            dataType: "html",
            success: function (response) {


                var image = getImage();

                if (image) {
                    //upload image
                    $.ajax({
                        type: "POST",
                        url: '/Subject/SaveImage?id=' + response,
                        contentType: false,
                        processData: false,
                        data: image,
                        success: function (result) {
                            console.log(result);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                        }
                    });
                }

                window.location.href = "/Subject/Details?id=" + response + "&type=Taxon";
            }
        });
    }
}

function getInteractionsJSON(source)
{
    var JSONArray = [];
    //source = list of tr
    $(source).each(function () {
        var i = getInteractionJSON(this);
        JSONArray.push(i);
    });

    return JSONArray;
}

function getInteractionJSON(e)
{

    /**
     * $($($(".interactions-table tr.interaction-row")[0]).find("#Interactions_item_Predicate_Type")[0]).val()
        "Unknow"
     */
    console.log("interactionRow:  ");
    console.log($(e));
    console.log($(e)[0].id);

    var interaction = {
            Id : $(e)[0].id,
                Indicator: $(e).find("#Indicator").val(),
                Subject: {
                    Id: $(e).find(".mct-interaction-subject #Subject_Id").val(),
                    Name: $(e).find(".mct-interaction-subject #Subject_Name").val(),
                    Type: $(e).find(".mct-interaction-subject #Subject_Type").val()
                    },
                Predicate: {
                    Id: $(e).find(".mct-interaction-predicate #Predicate_Id").val(),
                    Name: $(e).find(".mct-interaction-predicate #Predicate_Name").val(),

                    Parent: {
                        Name: $(e).find(".mct-interaction-predicate #Predicate_ParentName").val()
                    }
                    },
                Object: {
                    Id: $(e).find(".mct-interaction-object #Object_Id").val(),
                    Name: $(e).find(".mct-interaction-object #Object_Name").val(),
                    Type: $(e).find(".mct-interaction-object #Object_Type").val()
                    },
                ImpactSubject: {

                    Id: $(e).find(".mct-interaction-impactsubject #ImpactSubject_Id").val(),
                    Name: $(e).find(".mct-interaction-impactsubject #ImpactSubject_Name").val(),
                    Type: $(e).find(".mct-interaction-impactsubject #ImpactSubject_Type").val()

                }

                };

   

    return interaction;
}

function getLifeCycle(source, parentid)
{
    //lifecycle-li

    var JSONArray = [];
    var list = $(source).find("li.lifecycle-li").each(function () {

        var lc = getTimePeriodsJSON(this, parentid);
        console.log(lc);
        JSONArray.push(lc);
        });

    return JSONArray;

}

function getTimePeriodsJSON(source, parentid) {
    var JSONArray = [];
    var list = $(source).find("li").each(function () {

        var tp = getTimePeriodJSON(this, parentid);
        console.log(tp);
        JSONArray.push(tp);
    });

    return JSONArray;
}

function getTimePeriodJSON(e, parentid, plant) {
    console.log("e: " + e);
    console.log(e);
    var tp = {

        StartArea: $(e).find(".start-area").find("select").val(),
        StartMonth: $(e).find(".start-month").find("select").val(),
        EndArea: $(e).find(".end-area").find("select").val(),
        EndMonth: $(e).find(".end-month").find("select").val(),
        Type: $(e).find(".type").val(),

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

function getImage() {

    var fileName = $('#previewImageInput').val().replace(/.*(\/|\\)/, '');
    if (fileName != "") {
        var formData = new FormData();

        formData.append('file', $('input[type=file]')[0].files[0]);

        return formData;

    }
}


/*** TIME PERIODS ***/

    function addTP(e) {

        var newElement = $('<li class="timeperiod-li">');

        $.get("/Subject/GetEmptyTimePeriod",
            function(data) {

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

    function addLifeCycle(e) {

        var newElement = $('<li class="lifecycle-li">');

        $.get("/Subject/GetEmptyLifeCycle",
            function(data) {

                newElement.prepend($(data));
                //console.log(newElement);
                var list = $(e).parent().find("ul#lifecycle-list");
                //console.log("list");
                    //console.log(list);
                $(list).append(newElement);

                });
    }

    function removeLifeCycle(e) {

        console.log("e --->");
        console.log(e);
        $(e).parents(".lifecycle-li")[0].remove();
    }

    

/*** Simple Links ***/

    function addSimpleLink(e) {

        var newElement = $('<li class="timeperiod-li">');

        $.get("/Subject/GetEmptySimpleLink",
            function(data) {

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

/*** Interactions ***/

function saveInteractions() {
    
    var interactionsFromSide = $(".interactions-table tr.interaction-row");
    var interactions = getInteractionsJSON(interactionsFromSide);


    $.ajax({
        type: "POST",
        url: "/Interaction/Save",
        data: interactions,
        dataType: "html",
        success: function (response) {

            alert("saved");
        }
    });


}

function addInteraction(e) {


        $.get("/Interaction/GetEmptyInteraction",
            function(data) {

                console.log(data);
                var table = $(e).parent().find(".interactions-table tbody tr.interaction-row")[0];
                $(table).prepend(data);
            });
        ////$($("table tbody")[0]).find("tr").last()
        //var table = $(e).parent().find(".interactions-table tbody")[0];
        //console.log(table);
        //console.log($(table).find("tr").last());

        //var newChild = $($(table).find("tr").last()).clone(false, false);
        //newChild = cleanInteraction(newChild);
        //$(table).append(newChild); 
        //console.log(newChild);
    }


function removeInteraction(e) {

        $(e).parents("tr")[0].remove();
    }



/************** Plant Show ******************/
    $(".containerSwitch").on("click",
    function () {
        console.log("click");
        $($($(this).parents(".row")[0]).find(".content")[0]).toggle(200);
        $(this).toggleClass("fa-angle-double-down", "fa-angle-double-down");
    });


/************** IMAGES *****************/


    function previewImage(input) {

        //alert("p-image");

        if (input.files && input.files[[0]]) {

            var reader = new FileReader(),
                preview = $('#' + $(input).data('target'));

            reader.onload = function(e) {
                preview.attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }


/*********** LOAD NAMES **************/
    $(".nameContainer #Name").change(function () {

        // if value not null activate scientifname load
        var value = $(this).val();
        console.log(value);

        if (value != null && value != "") {
            console.log("show");
            $("#scientificNameIcon").show();
        }
        else {
            console.log("hide");

            $("#scientificNameIcon").hide();
            $("#scientificNameIcon").removeClass("fa-spin");
        }
    });

    $(".scientficNameContainer #ScientificName").change(function () {

        // if value not null activate scientifname load
        var value = $(this).val();
        console.log(value);

        if (value != null && value != "") {
            $("#nameIcon").show();
        }
        else {

            $("#nameIcon").hide();
            $("#nameIcon").removeClass("fa-spin");

        }
    });

    $("#scientificNameIcon").click(function () {

        $("#scientificNameIcon").addClass("fa-spin");

        value = $(".nameContainer #Name").val();
        data = {
            name :value
        }

        $.ajax({
            type: "POST",
            url: "/Subject/GetScientificName",
            data: data,
            dataType: "json",
            success: function (response) {
                
                $("#scientificNameIcon").removeClass("fa-spin");

                $(".scientficNameContainer #ScientificName").val(response);
            }
        });
    });

    $("#nameIcon").click(function () {

        $("#nameIcon").addClass("fa-spin");

        value = $(".scientficNameContainer #ScientificName").val();
        data = {
            scientificName: value
        }

        $.ajax({
            type: "POST",
            url: "/Subject/GetName",
            data: data,
            dataType: "json",
            success: function (response) {

    
                $("#nameIcon").removeClass("fa-spin");

                $(".nameContainer #Name").val(response);
            }
        });
    });
