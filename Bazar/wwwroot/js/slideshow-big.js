$(document).ready(function () {

    const TRANSITION_MOVE_SLIDE = 0.5;

    /***************************************************
     *************   OPEN IMAGE VIEW   *****************
     ***************************************************/

    var Container = $(".gallery-image-view .images-container");
    var Inner = $(".gallery-image-view .inner");                    //For calculate size

    var Gallery_viewer = $(".gallery-image-view")[0];
    var List_of_gallery_imgs = $(".gallery-image-view .images-container");
    var Items_of_gallery_imgs;

    var Current_Slide;

    var Num_of_current_category = 0;
    var Count_img_by_category;
    var Num_of_current_slide;
    var Width_of_slides_list;
    var Width_of_slide;

    $(".gallery-item").on("click", function () {


        //Get imgs by category Id
        var Imgs_of_current_category = Num_of_current_category == 0
            ? $(".gallery-item")
            : $(".gallery-item[data-category=" + Num_of_current_category + "], .gallery-item[data-parent=" + Num_of_current_category + "]");
        Count_img_by_category = Imgs_of_current_category.length;

        for (var i = 0; i < Imgs_of_current_category.length; i++) {
            if ($(this).attr("data-number") == $(Imgs_of_current_category[i]).attr("data-number")) {
                Num_of_current_slide = i + 2;
                break;
            }
        }

        //Append imgs in gallery viewer
        var NewImgs = $(Imgs_of_current_category).clone();

        for (var i = 0; i < NewImgs.length; i++) {

            if ($(Imgs_of_current_category[i]).find(".gallery-image").width() / $(Imgs_of_current_category[i]).find(".gallery-image").height() < 1.35) {
                $(NewImgs[i]).find(".gallery-image").css("width", "auto").height("100%");
            }

        }

        NewImgs.appendTo(List_of_gallery_imgs);
        $(NewImgs[Count_img_by_category - 1]).clone().prependTo(List_of_gallery_imgs);
        $(NewImgs[Count_img_by_category - 2]).clone().prependTo(List_of_gallery_imgs);
        $(NewImgs[0]).clone().appendTo(List_of_gallery_imgs);
        $(NewImgs[1]).clone().appendTo(List_of_gallery_imgs);

        //Composite imgs in gallery viewer
        Count_img_by_category += 4;
        Items_of_gallery_imgs = $(".gallery-image-view .images-container .gallery-item");

        Width_of_slides_list = 100 * Count_img_by_category;
        Width_of_slide = 100 / Count_img_by_category;

        $(List_of_gallery_imgs).css("width", Width_of_slides_list + "%");
        $(Items_of_gallery_imgs).css("width", Width_of_slide + "%");
        $(List_of_gallery_imgs).css("transform", "translateX(-" + Width_of_slide * Num_of_current_slide + "%)");

        Current_Slide = $(Items_of_gallery_imgs[Num_of_current_slide]);
        $(Current_Slide).attr("data-active", "");

        //Show gallery viewer
        $(Gallery_viewer).fadeIn();

        ResizeViewer();
    });

    $(".arrow-nav.next, .arrow-nav.prev").on("click", function () {
        MoveSlide($(this).attr("data-direction") == "next" ? 1 : -1);
    });

    var Is_thread_free = true;
    function MoveSlide(Direction) {
        if (!Is_thread_free)
            return;

        Is_thread_free = false;

        Current_Slide.removeAttr("data-active");
        Current_Slide = $(Items_of_gallery_imgs[Num_of_current_slide + Direction]).attr("data-active", "");

        if (Direction == 1) {
            if (Num_of_current_slide + 1 == Count_img_by_category - 2) {
                $(Items_of_gallery_imgs[2]).attr("data-active", "");
            }
        }
        else if (Direction == -1) {
            if (Num_of_current_slide - 1 == 1) {
                $(Items_of_gallery_imgs[Count_img_by_category - 3]).attr("data-active", "");
            }
        }
        else {
            alert("Direction can't be zero!");
            return;
        }

        TweenLite.fromTo(List_of_gallery_imgs, TRANSITION_MOVE_SLIDE,
            {
                x: "-" + Width_of_slide * Num_of_current_slide + "%"
            },
            {
                x: "-" + Width_of_slide * (Num_of_current_slide + Direction) + "%",
                onComplete: function () {

                    if (Direction == 1) {
                        if (Num_of_current_slide + 1 == Count_img_by_category - 2) {
                            $(Items_of_gallery_imgs[Num_of_current_slide + 1]).removeAttr("data-active");

                            $(List_of_gallery_imgs).css("transform", "translateX(-" + Width_of_slide * 2 + "%)");

                            Num_of_current_slide = 2;

                        }
                        else
                            Num_of_current_slide++;
                    }
                    else if (Direction == -1) {
                        if (Num_of_current_slide - 1 == 1) {
                            $(Items_of_gallery_imgs[Num_of_current_slide - 1]).removeAttr("data-active");

                            $(List_of_gallery_imgs).css("transform", "translateX(-" + Width_of_slide * (Count_img_by_category - 3) + "%)");
                            Num_of_current_slide = Count_img_by_category - 3;
                        }
                        else
                            Num_of_current_slide--;
                    }
                    else {
                        alert("Direction can't be zero!");
                        return;
                    }

                    Current_Slide = $(Items_of_gallery_imgs[Num_of_current_slide]);

                    Is_thread_free = true;
                }
            });
    }

    /*******************************Resize viewer*******************************/
    ResizeViewer();
    $(window).resize(function () {
        ResizeViewer();
    });

    function ResizeViewer() {
        var ViewportRatio = $(window).width() / $(window).height();

        if (ViewportRatio < 1.55) {
            $(Inner).removeAttr("by-max-height");
            $(Inner).attr("by-max-width", "");
        }
        else {
            $(Inner).removeAttr("by-max-width");
            $(Inner).attr("by-max-height", "");
        }

        var WrapperWidth = $(".images-wrapper").width();
        var WrapperHeight = $(".images-wrapper").height();

        $(".gallery-image").each(function () {

            if ($(this).width() / $(this).height() < WrapperWidth / WrapperHeight) {
                $(this).removeAttr("by-max-height");
                $(this).attr("by-max-width", "");
            } else {
                $(this).removeAttr("by-max-width");
                $(this).attr("by-max-height", "");
            }

            if (WrapperWidth / WrapperHeight > 2) {
                $(this).removeAttr("by-max-height");
                $(this).attr("by-max-width", "");
            }
        });
    }

    /***************************************************
     *************   HIDE IMAGE VIEW   *****************
     ***************************************************/
    $(function () {
        $(document).click(function (event) {
            if ($(event.target).closest(".gallery-image-view .inner").length) return;
            if ($(event.target).closest(".gallery-item").length) return;

            $(".gallery-image-view").fadeOut();
            $(Container).html("");

            event.stopPropagation();
        });

        $("#gallery-close").click(function () {
            $(".gallery-image-view").fadeOut();
            $(Container).html("");
        });
    });


    /***************************************************
     *************   CHANGE CATEGORY   *****************
     ***************************************************/

    var ImagesContainer = $(".main-section .gallery-wrapper");
    var ImagesRepository = $(".gallery-repository");
    var Delay = 0.2;

    $(".nav-section li").each(function () {
        $(this).click(function () {

            Num_of_current_category = $(this).attr("data-category");

            var CategoryId = $(this).attr("data-category");
            var ParentId = $(this).attr("data-parent");
            var ImagesVisible = Array();
            var ImagesHidden = Array();

            //Show sublist
            if (!$(this).hasClass("subitem")) {
                $(".nav-section .sublist").each(function () {
                    if ($(this).attr("data-parent") == CategoryId) {
                        $(this).css("display", "flex");
                    } else {
                        $(this).css("display", "none");
                    }
                });
            }

            $(".nav-section li").each(function () {
                if ($(this).attr("data-category") != ParentId)
                    $(this).removeAttr("selected");
            });
            $(this).attr("selected", "");

            $(".gallery-item").each(function () {
                if (CategoryId == 0 || $(this).attr("data-category") == CategoryId || $(this).attr("data-parent") == CategoryId) {
                    ImagesVisible.push($(this));
                } else {
                    ImagesHidden.push($(this));
                }
            });

            for (var i = 0; i < ImagesHidden.length; i++) {
                $(ImagesRepository).append($(ImagesHidden[i]));
            }

            var flag = 0;
            for (var i = ImagesVisible.length - 1; i >= 0; i--) {
                $(ImagesVisible[i]).attr("data-number", flag);
                flag++;
            }

            for (var i = 0; i < ImagesVisible.length; i++) {

                $(ImagesContainer).append($(ImagesVisible[i]));
                AnimateElement($(ImagesVisible[i]), Delay + Delay / (i + 1), 1);
            }


        });
    });

    function AnimateElement(Element, Delay, Visible) {
        if (Element == undefined || Element == null)
            return;

        TweenLite.fromTo(Element, 0.2,
            {
                opacity: !Visible
            },
            {
                opacity: Visible,
                delay: Delay
            });
    }
});
