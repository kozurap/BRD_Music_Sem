// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {

    //Tracks Display
    function getTrackTable(data, status) {
        let str;
        data.map(item => str += `<tr><td>${item.Name}</td><td>${item.Author}</td></tr>`);
        $("#TData").append(str)
    };

    $.ajax({
        type: "GET",
        url: "https://localhost:44321/Music/GetList",
        success: (data, status) => { getTrackTable(data, status) }
    });

    //Groups Display
    function getGroupTable(data, status) {
        let str;
        data.map(item => str += `<tr><td>${item}</td></tr>`);
        $("#GData").append(str)
    };
    $.ajax({
        type: "GET",
        url: "https://localhost:44321/Music/GetGList",
        success: (data, status) => { getGroupTable(data, status) }
    });

    //Track Search
    function getSRecTable(data, status) {
        var table = document.getElementById("SrcTable");
        while (table.rows.length > 0) {
            table.deleteRow(0);
        }
        let str;
        data.map(item => str += `<tr><td>${item.Name}</td><td>${item.Author}</td></tr>`);
        $("#TSData").append(str)
    };
    $("#FindTrack").on("click", function () {
        var author = document.getElementById("SearchName").value;
        console.log(JSON.stringify(author));
        console.log(author);
        $.ajax({
            type: "GET",
            data: {
                name: author
            },
            contentType: "application/json; charset=utf-8",
            url: "https://localhost:44321/TrackList/SearchTrack",
            success: (data, status) => { getSRecTable(data, status) }
        });
        return false;
    });

    //Group Search
    function getSTourTable(data, status) {
        var table = document.getElementById("SrcGrTable");
        while (table.rows.length > 0) {
            table.deleteRow(0);
        }
        let str;
        data.map(item => str += `<tr><td><a href="">${item.Author}</a></td></tr>`);
        $("#GSData").append(str)
    };
    $("#FindGroup").on("click", function () {
        var author = document.getElementById("SearchGr").value;
        $.ajax({
            type: "GET",
            data: {author : author},
            contentType: "application/json; charset=utf-8",
            url: "https://localhost:44321/GroupList/SearchGroup",
            success: (data, status) => { getSTourTable(data, status) }
        });
        return false;
    });






    //Topic Lists


    $.ajax({
        type: "GET",
        url: "https://localhost:44321/Forum/GetTopics",
        success: function (data, status) {
            console.log(data);
            let str = "";
            data.map(item => str += `<li><a class="nav-link buttonsMoi" id="${item.Id}" href="Forum/Topic">${item.Name}</a></li>`);
            console.log(str);
            $("#FList").append(str)
        }
    });

    //Topic View
    if (document.querySelector('#FList')) {
        document.querySelector('#FList').addEventListener('click', function (e) {
            var url = "https://localhost:44321/Forum/Topic/" + e.target.id;
            console.log(url);
            localStorage.setItem('url', url);
            localStorage.setItem('id', e.target.id);
            console.log(localStorage.getItem(url));
        });
    }
    $.ajax({
        type: "GET",
        url: localStorage.getItem('url'),
        success: function (data, status) {
            let str = "";
            data.map(item => str += `<div class="text-center"><p><b>${item.Author}</b></p><p>${item.Name}</p></div>`);
            console.log(str);
            $("#MessageShow").append(str)
        }
    });
    //Send Message
    function sendNewMessage(data, status) {
        let str = "";
        data.map(item => str += `<div class="text-center"><p><b>${item.Author}</b></p><p>${item.Name}</p></div>`);
        $("#MessageShow").append(str)
    };
    $("#SendMess").on("click", function () {
        var message = document.getElementById("PostMessage").value;
        let b = localStorage.getItem('id').toString();
        $.ajax({
            url: "https://localhost:44321/Forum/Topic",
            type: "POST",
            data: { id: b, message: message },

            success: (data, status) => { getSTourTable(data, status) }
        });
        return false;
    });



    //Profile


});