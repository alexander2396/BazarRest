 /***************************************************
     *************   SLIDE SHOW  *****************
     ***************************************************/

    const TRANSITION_MOVE_SLIDE = 0.5;

    var SlideShowContainer = $(".slide-show .gallery-wrapper");
    var Images = $(".gallery-item");
    var ImagesCount = Images.length;
    var Width_of_slide_show_list;
    var Width_of_slide_show_slide;
    var Num_of_current_slide;
    var Current_Small_Slide;
    var Num_of_current_small_slide;
    var List_of_slide_show_images;

    for (var i = 0; i < ImagesCount; i++) {
        if ($(Images[i]).find(".gallery-image").width() / $(Images[i]).find(".gallery-image").height() > 1.35) {
            $(Images[i]).find(".gallery-image").css("width", "auto").height("100%");
        }
    }

    ImagesCount += 5;

    Width_of_slide_show_list = 100 * ImagesCount / 5;
    Width_of_slide_show_slide = 100 / ImagesCount;

    $(SlideShowContainer).css("width", Width_of_slide_show_list + "%");
    $(Images).css("width", Width_of_slide_show_slide + "%");
    $(SlideShowContainer).css("transform", "translateX(-" + Width_of_slide_show_slide * 2  + "%)");

    $(Images[ImagesCount - 6]).clone().prependTo(SlideShowContainer);
    $(Images[ImagesCount - 7]).clone().prependTo(SlideShowContainer);
    $(Images[ImagesCount - 8]).clone().prependTo(SlideShowContainer);
    $(Images[0]).clone().appendTo(SlideShowContainer);
    $(Images[1]).clone().appendTo(SlideShowContainer);

    Current_Small_Slide = $(Images[1]);
    $(Images[1]).attr("data-active", "");

    Num_of_current_small_slide = 4;

    List_of_slide_show_images = $(".slide-show .gallery-item");

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
    var Width_of_slides_list;
    var Width_of_slide;

    $(".gallery-item").on("click", function () {
        
        //Get imgs by category Id
        var Imgs_of_current_category = $(".gallery-item");

        Count_img_by_category = Imgs_of_current_category.length;
        
        for (var i = 0; i < Imgs_of_current_category.length; i++) {
            if (i > 1 && i < Imgs_of_current_category.length - 1) {
                if ($(this).attr("data-number") == $(Imgs_of_current_category[i]).attr("data-number")) {
                    Num_of_current_slide = i;
                    break;
                }
            }         
        }
 
        //Append imgs in gallery viewer
        var NewImgs = $(Imgs_of_current_category).clone();

        for (var i = 0; i < NewImgs.length; i++) {

            if ($(Imgs_of_current_category[i]).find(".gallery-image").width() / $(Imgs_of_current_category[i]).find(".gallery-image").height() < 1.3) {
                $(NewImgs[i]).find(".gallery-image").css("width", "100%").height("auto");
            }
        }

        NewImgs.appendTo(List_of_gallery_imgs);
        /*$(NewImgs[Count_img_by_category - 1]).clone().prependTo(List_of_gallery_imgs);
        $(NewImgs[Count_img_by_category - 2]).clone().prependTo(List_of_gallery_imgs);
        $(NewImgs[0]).clone().appendTo(List_of_gallery_imgs);
        $(NewImgs[1]).clone().appendTo(List_of_gallery_imgs);*/

        //Composite imgs in gallery viewer
        //Count_img_by_category += 4;
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
    });

    $(".gallery-image-view .arrow-nav.next, .gallery-image-view .arrow-nav.prev").on("click", function () {
        MoveSlide($(this).attr("data-direction") == "next" ? 1 : -1);
    });

    $(".slider .arrow-nav.next, .slider .arrow-nav.prev").on("click", function () {
        MoveSmallSlide($(this).attr("data-direction") == "next" ? 1 : -1);
    });


    var Is_thread_free = true;
    function MoveSlide(Direction) {
        if (!Is_thread_free)
            return;

        $('video').each(function () {
            this.pause();
        });
        /*$('.youtube-iframe').each(function () {
            var src = $(this).attr("src");
            $(this).attr("src", src);
        });*/

        Is_thread_free = false;

        Current_Slide.removeAttr("data-active");
        Current_Slide = $(Items_of_gallery_imgs[Num_of_current_slide + Direction]).attr("data-active", "");

        if (Direction == 1) {
            if (Num_of_current_slide + 1 == Count_img_by_category - 2) {
                $(Items_of_gallery_imgs[3]).attr("data-active", "");
            }
        }
        else if (Direction == -1) {
            if (Num_of_current_slide - 1 == 1) {
                $(Items_of_gallery_imgs[Count_img_by_category - 4]).attr("data-active", "");
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

                            $(List_of_gallery_imgs).css("transform", "translateX(-" + Width_of_slide * 3 + "%)");

                            Num_of_current_slide = 3;

                        }
                        else
                            Num_of_current_slide++;
                    }
                    else if (Direction == -1) {
                        if (Num_of_current_slide - 1 == 1) {
                            $(Items_of_gallery_imgs[Num_of_current_slide - 1]).removeAttr("data-active");

                            $(List_of_gallery_imgs).css("transform", "translateX(-" + Width_of_slide * (Count_img_by_category - 4) + "%)");
                            Num_of_current_slide = Count_img_by_category - 4;
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

    function MoveSmallSlide(Direction) {
        if (!Is_thread_free)
            return;

        Is_thread_free = false;

        Current_Small_Slide.removeAttr("data-active");
        Current_Small_Slide = $(List_of_slide_show_images[Num_of_current_small_slide + Direction]).attr("data-active", "");
        
        if (Direction == 1) {
            if (Num_of_current_small_slide + 1 == ImagesCount - 3) {
                $(List_of_slide_show_images[2]).attr("data-active", "");
            }
        }
        else if (Direction == -1) {
            if (Num_of_current_small_slide - 1 == 2) {
                $(List_of_slide_show_images[ImagesCount - 3]).attr("data-active", "");
            }
        }
        else {
            alert("Direction can't be zero!");
            return;
        }

        /*if (Num_of_current_small_slide == ImagesCount - 6) {
            $(SmallSlideArrowNext).hide();
        } else {
            $(SmallSlideArrowNext).show();
        }
      
        if (Num_of_current_small_slide == 5) {
            $(SmallSlideArrowPrev).hide();
        } else {
            $(SmallSlideArrowPrev).show();
        }*/

        TweenLite.fromTo(SlideShowContainer, TRANSITION_MOVE_SLIDE,
            {
                x: "-" + Width_of_slide_show_slide * (Num_of_current_small_slide - 2) + "%"
            },
            {
                x: "-" + Width_of_slide_show_slide * (Num_of_current_small_slide + Direction - 2) + "%",
                onComplete: function () {

                    if (Direction == 1) {
                        if (Num_of_current_small_slide + 1 == ImagesCount - 3) {
                            $(List_of_slide_show_images[Num_of_current_small_slide + 1]).removeAttr("data-active");

                            $(SlideShowContainer).css("transform", "translateX(-" + 0 + "%)");

                            Num_of_current_small_slide = 2;

                        }
                        else
                            Num_of_current_small_slide++;
                    }
                    else if (Direction == -1) {
                        if (Num_of_current_small_slide - 1 == 2) {
                            
                            $(List_of_slide_show_images[Num_of_current_small_slide - 1]).removeAttr("data-active");

                            $(SlideShowContainer).css("transform", "translateX(-" + Width_of_slide_show_slide * (ImagesCount - 5) + "%)");
                            Num_of_current_small_slide = ImagesCount - 3;
                        }
                        else
                            Num_of_current_small_slide--;
                    }
                    else {
                        alert("Direction can't be zero!");
                        return;
                    }

                    Current_Small_Slide = $(List_of_slide_show_images[Num_of_current_small_slide]);

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