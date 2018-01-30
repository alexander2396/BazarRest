var el = document.getElementById('sortable');
var sortable = new Sortable(el, {
    animation: 150,
    onEnd: function (evt) {

        var data = [];

        var counter = 1;
        $("#sortable .product-item").each(function (el) {

            var product = {
                "ProductId": $(this).attr("data-id"),
                "Order": counter
            }

            data.push(product);
            counter++;
        });

        $.ajax({
            url: "/api/product/saveorder",
            method: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (data) {


            }
        });

    },
});