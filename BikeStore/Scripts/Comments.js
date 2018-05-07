$(document).ready(function () {
    function LoadComments() {
        $.ajax({
            url: $("#comments").attr("data-url"),
            data: {},
            success: function (response) {
                $("#comments").html(response)
            }          
        });
    }
    if ($("#comments").length > 0) {
        setInterval(LoadComments, 10 * 1000);
    }
   
});