$(document).ready(function () {

    $(function () {
        $('[data-toggle="popover"]').popover()
    })

    //Dropdown sidebar list
    $('.btn-open-nav').each(function () {
        $(this).click(function () {

            $(this).siblings(".nav").toggle("fast");

            ToggleArrow(this);
        });
    });

    //Confirm delete alex
    $(".delete-button").click(function () {
        var el = $(this);

        event.preventDefault();

        $("#ConfirmDeleteModal").modal("show");
        $("#btn-confirm-delete").click(function () {
            $(el).closest("form").submit();
        });
    });

    //Toggle navebar
    $(".nav-toggle").click(function () {
        if ($(".main-sidebar").hasClass("sidebar-sm")) {
            $(".main-sidebar").removeClass("sidebar-sm");
            $("header").removeClass("wide");
        } else {
            $('.btn-open-nav+.nav').each(function () {
                $(this).hide("fast");
            });
            $(".main-sidebar").addClass("sidebar-sm");
            $("header").addClass("wide");
        }
    });
    if ($(window).width() < 988) {
        $(".main-sidebar").addClass("sidebar-sm");
        $("header").addClass("wide");
    }
});

function ToggleArrow(el) {
    var arrow = $(el).find(".arrow-button i");

    if (arrow.hasClass("fa-angle-down")) {
        arrow.removeClass("fa-angle-down").addClass("fa-angle-up");
    } else {
        arrow.removeClass("fa-angle-up").addClass("fa-angle-down");
    }
}


