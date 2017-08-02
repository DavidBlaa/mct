
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
        url: "/Subject/DeleteSubject",
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
        url: "/Subject/DeleteSubject",
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


    if ($("form").valid()) {

        //get Timeperiods list
        //string listId = type + "-list";

        var bloom = $("#bloom-list");
        var sowing = $("#sowing-list");
        var harvest = $("#harvest-list");
        var seedMaturity = $("#seedmaturity-list");

        var precultures = $(".preculture .simplelinkmodel-list");
        var aftercultures = $(".afterculture .simplelinkmodel-list");

        //all tr of table
        var interactionsFromSide = $(".interactions-table tr.interaction-row");


        //var tmp = getSimpleLinksJSON(precultures);
        //console.log("tmp -> preculture");
        //console.log(tmp);

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
            Bloom: getTimePeriodsJSON(bloom, $("#plant #Id").val()),
            Sowing: getTimePeriodsJSON(sowing, $("#plant #Id").val()),
            Harvest: getTimePeriodsJSON(harvest, $("#plant #Id").val()),
            SeedMaturity: getTimePeriodsJSON(seedMaturity, $("#plant #Id").val()),
            PreCultures: getSimpleLinksJSON(precultures),
            AfterCultures: getSimpleLinksJSON(aftercultures)
        };

        console.log(plant);

        var interactions = getInteractionsJSON(interactionsFromSide);


        var data = {
            plant: plant,
            interactions: interactions
        };

        console.log("DATA:");
        console.log(data);

        $.ajax({
            type: "POST",
            url: "/Subject/SavePlant",
            data: data,
            dataType: "html",
            success: function(response) {
                
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

function saveAnimal(e) {

    //$("input").validate();

    //alert($("form").valid());

    if ($("form").valid()) {
        // some other code
        // maybe disabling submit button
        // then:
        //alert("This is a valid form!");

        //get Timeperiods list
        //string listId = type + "-list";


        //all tr of table
        var interactionsFromSide = $(".interactions-table tr.interaction-row");


        //var tmp = getSimpleLinksJSON(precultures);
        //console.log("tmp -> preculture");
        //console.log(tmp);

        var animal = {
            Id: $("#animal #Id").val(),
            Name: $("#animal #Name").val(),
            ScientificName: $("#animal #ScientificName").val(),
            Rank: $("#animal #TaxonRank").val(),
            Description: $("#animal #Description").val()
        };

        var interactions = getInteractionsJSON(interactionsFromSide);

        var data = {
            animal: animal,
            interactions: interactions
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

    //$("form").validate({
    //    submitHandler: function (form) {
            

    //        
    //    }
    //});

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
    //console.log("interactionRow:  " + $(e));
    //console.log($(e)[0].id);

    var interaction = {
        Id: $(e)[0].id,
        Indicator: $(e).find("#Indicator").val(),
        Subject: {
            Id: $(e).find("#Subject_Id").val(),
            Name: $(e).find("#Subject_Name").val(),
            Type: $(e).find("#Subject_Type").val()
        },
        Predicate: {
            Id: $(e).find("#Predicate_Id").val(),
            Name: $(e).find("#Predicate_Name").val(),

            Parent: {
                Name: $(e).find("#Predicate_ParentName").val()
            }
        },
        Object: {
            Id: $(e).find("#Object_Id").val(),
            Name: $(e).find("#Object_Name").val(),
            Type: $(e).find("#Object_Type").val()
        },
        ImpactSubject: {

            Id: $(e).find("#ImpactSubject_Id").val(),
            Name: $(e).find("#ImpactSubject_Name").val(),
            Type: $(e).find("#ImpactSubject_Type").val()

        },

    };
    
    return interaction
}

function getTimePeriodsJSON(source, parentid) {
    var JSONArray = [];
    var list = $(source).find("li").each(function () {

        var tp = getTimePeriodJSON(this, parentid);
        JSONArray.push(tp);
    });

    return JSONArray;
}

function getTimePeriodJSON(e, parentid) {
    console.log("e: " + e);
    var tp = {
        Subject: parentid,
        StartArea: $(e).find(".start-area").find("select").val(),
        StartMonth: $(e).find(".start-month").find("select").val(),
        EndArea: $(e).find(".end-area").find("select").val(),
        EndMonth: $(e).find(".end-month").find("select").val(),
        Type: $(e).find("#Type select").val()
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

    function addInteraction(e) {


        $.get("/Subject/GetEmptyInteraction",
            function(data) {

                console.log(data)
                var table = $(e).parent().find(".interactions-table tbody")[0];
                $(table).append(data);
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
