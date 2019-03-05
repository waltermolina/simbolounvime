//#region vars
var refreshIntervalMS = 1000 * 1 * 60; // 1 minute
var interval = null;

//#endregion

var TopFiveDS = new kendo.data.DataSource({
    transport: {
        read: {
            type: "GET",
            url: "./api/Eslogans/GetTopTenEslogans",
            dataType: "json",
            complete: function (jqXHR, textStatus) {
                console.log(textStatus, "read")
                //createTopCounter();
            },
            error: function (jqXHR, textStatus) { console.log(textStatus, "read") },
        },
    },
});

var WordListDS = new kendo.data.DataSource({
    transport: {
        read: {
            type: "GET",
            url: "./api/Eslogans/GetAllWords",
            dataType: "json",
            complete: function (jqXHR, textStatus) { console.log(textStatus, "read") },
            error: function (jqXHR, textStatus) { console.log(textStatus, "read") },
        },
    },
});

//#region Top 5 Pie Chart
function Top5PieChart() {
    $("#top5PieChart").kendoChart({
        theme: "Material",
        dataSource: TopFiveDS,
        title: {
            text: "Palabras más usadas"
        },
        legend: {
            position: "top"
        },
        seriesDefaults: {
            labels: {
                template: "#= category # - #= kendo.format('{0:P}', percentage)#",
                position: "outsideEnd",
                visible: true,
                background: "transparent"
            }
        },
        series: [{
            type: "pie",
            field: 'NumberWords',
            categoryField: 'Words'
        }],
        tooltip: {
            visible: true,
            template: "#= category # - #= kendo.format('{0:P}', percentage) #"
        }
    });
}
//#endregion

//#region Words ListView
function createWordListView() {
    $("#wordsListView").kendoListView({
        dataSource: WordListDS,
        template: kendo.template($("#wordsTemplate").html())
    });

}
//#endregion

//#region TopCounter
function createTopCounter() {
    //Acá va el valor total de participaciones
    $.ajax({
        timeout: 60000,
        type: "GET",
        datatype: "JSON",
        url: "/api/Eslogans/GetEsloganCount",
        success: function (e) {

            $("#topCounter > h1").text(e);
            //createSchoolsCounters();

        },
        error: function () {
           
        }
    });

}
//#endregion


//#region TopCounter
function createSchoolsCounters() {
    //Acá va el valor total de participaciones pero por cada escuela
    $.ajax({
        timeout: 60000,
        type: "GET",
        datatype: "JSON",
        url: "/api/Eslogans/GetAllCountsForEveryTeam",
        success: function (e) {

            $("#topBySchool > .school1").text(e[0].Numbers);
            $("#topBySchool > .school2").text(e[1].Numbers);
            $("#topBySchool > .school3").text(e[2].Numbers);
            $("#topBySchool > .school4").text(e[3].Numbers);

        },
        error: function () {

        }
    });
    

}
//#endregion

function autoRefresh() {
    interval = setInterval(function () {
        $("#top5PieChart").data("kendoChart").dataSource.read();
        $("#wordsListView").data("kendoListView").dataSource.read();
        createTopCounter();
        createSchoolsCounters();

    }, refreshIntervalMS);
}

function resizeRealTime() {
    try {
        $("#top5PieChart").data("kendoChart").resize();
    }
    catch (e) {

    }
}

$(window).resize(resizeRealTime);

$(document).ready(function () {
    Top5PieChart();
    createWordListView();
    createTopCounter();
    createSchoolsCounters();
    autoRefresh();
});