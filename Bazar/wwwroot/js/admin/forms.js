
$(document).ready(function () {

    //display meta inputs max length
    $('.input-counter').each(function () {
        $(this).siblings("span.counter").children(".total").html($(this).attr("data-val-length-max"));
    });
    $('.input-counter').on('input', function () {
        var count = $(this).val();
        length = count.length;

        if (length > $(this).attr("data-val-length-max")) {
            $(this).siblings("span.counter").css("color", "red");
        } else {
            $(this).siblings("span.counter").css("color", "black");
        }
        $(this).siblings("span.counter").children(".current").html(length);
    });

    //display file input
    $(".show-hidden-image").each(function () {
        $(this).click(function () {
            $(this).siblings(".hidden-image").show();
            $(this).hide();
        });
    });

    //Creating new select item
    $(".new-item-btn").click(function () {
        var content = $(this).siblings(".multiple-select")[0].outerHTML;
        $(this).closest(".items-append-container").append('<div class="form-group">' + content + '</div>');
    });

    $(".UploadedImageInput").change(function () {
        var Element = $(this);
        if (this.files && this.files[0]) {

            var reader = new FileReader();

            reader.onload = function (e) {
                $(Element).parent(".hidden-image").next(".image-preview").attr('src', e.target.result);
                $(Element).next(".image-preview").attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);

            $(Element).siblings(".file-input").attr("value", this.files[0].name);
        }
    });


    //Recursion list
    $('.list-item-container').click(function () {
        $(this).siblings(".inner-list").slideToggle("fast");
        $(this).toggleClass("selected").parent(".list-item").toggleClass("selected");
    });

    $('.list-item-container.checked').each(function () {

        var parents = $(this).parents(".list-item");

        for (var i = 0; i < parents.length; i++) {
            $(parents[i]).addClass("selected");
            $(parents[i]).children(".inner-list").slideDown("fast");
            $(parents[i]).children(".list-item-container").addClass("selected");
        }

        $(this).siblings(".inner-list").addClass("parent-checked");
        $(this).siblings(".inner-list").find(".checkbox").addClass("checked-disabled");
        $(this).siblings(".inner-list").find(".list-item-container").removeClass("checked");
        $(this).siblings(".inner-list").find(".checked").removeClass("checked");
    });

    if ($("#type-select").val() === "NewPrice") {
        $(".checkbox").each(function () {
            var input = $(this).find("input");
            if ($(input).attr("name") === "SaleProductCategories") {
                $(this).hide();
                $(input).val(0);
            }
        });
    }

});

function SelectCheckbox(el) {

    $(".checkbox").each(function () {
        $(this).removeClass("checked");
        $(this).siblings(".list-item-container").removeClass("checked");
        $(this).find("input").val(0);
    });

    var id = $(el).attr("data-id");

    if ($(el).hasClass("checked")) {
        $(el).removeClass("checked");
        $(this).find("input").val(0);
    } else {
        $(el).addClass("checked");
        $(el).find("input").val(id);
    }
}