var el = document.getElementById('sortable');
var sortable = new Sortable(el, {
    animation: 150,
    onEnd: function (evt) {

        var data = [];

        var counter = 1;
        $("#sortable .gallery-item").each(function (el) {

            var slide = {
                "SlideId": $(this).attr("data-id"),
                "Order": counter
            }

            data.push(slide);
            counter++;
        });

        $.ajax({
            url: "/api/slide/saveorder",
            method: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (data) {


            }
        });

    },
});