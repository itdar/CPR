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
        $("#collapseArea").collapse('hide');
        $("#collapseBtn").collapse('hide');
    });
});