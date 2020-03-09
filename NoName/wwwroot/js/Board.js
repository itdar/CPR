//게시판에 관련된 javascripts


/***************************
about Writing Function in Board/Index
title click -> collapse.show
postbutton -> collapse.hide
****************************/

$(document).ready(function () {
    $("#postTitle").click(function () {
        $("#collapseArea").collapse('show');
        $("#collapseBtn").collapse('show');
    });

    $("#postBtn").click(function () {
        $.ajax({
            type: "POST",
            url: "/Board/Index",
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function () {
                $("#collapseArea").collapse('hide');
                $("#collapseBtn").collapse('hide');
            }
        });
    });
});