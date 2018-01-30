$(document).ready(function () {

    $('.carousel').carousel();

    var mobileNavbar = $(".mobile-navbar");

    var menuOpenButton = $("#menu-show-btn");
    var foreground = $("#foreground");

    //Open mobile navbar
    $(menuOpenButton).click(function () {
        $(mobileNavbar).attr("expanded", "");
        $(foreground).fadeIn();
    });

    //Hide mobile navbar
    $("#menu-hide-btn, .mobile-navbar .menu-item").click(function () {
        $(mobileNavbar).removeAttr("expanded");
        $(foreground).fadeOut();
    });
    $(document).click(function (event) {
        if ($(event.target).closest(mobileNavbar).length || $(event.target).closest(menuOpenButton).length) return;
        $(mobileNavbar).removeAttr("expanded");
        $(foreground).fadeOut();
        event.stopPropagation();
    });

    //Anchor
    $(".mobile-navbar.with-anchors .menu-item a, .main-navbar.with-anchors .nav-item a").click(function () {
        event.preventDefault();
        var href = $(this).attr("href");
        $('html, body').animate({
            scrollTop: $(href).offset().top - 130
        }, 700);
    });
    if (window.location.hash.substr(1).length > 0) {
        var href = "#" + window.location.hash.substr(1);
        $('html, body').scrollTop($(href).offset().top - 130);
    }

    var loadBtn = $("#load-news");
    var preloader = $("#preloader");

    $(loadBtn).click(function () {

        $(loadBtn).hide();
        $(preloader).show();

        var count = $("#news .article-item").length;

        $.ajax({     
            type: "GET",
            url: "/api/home/news?count=" + count,
            success: function (data) {
                
                var DIV = '';
                $.each(data.articles, function (i, item) {

                   

                    var rows = '<div class="col-xl-4 col-lg-4 col-md-6 mt-4 article-item">' +
                        '<a href="/blog/' + item.categoryUrl + '/' + item.linkUrl + '">' +
                            '<div class="image-container">' +
                        '<img class="article-image" src="/DynamicImages/Article/' + item.imageFileName + '" alt="' + item.imageAlt + '" title="' + item.imageTitle + '" />' +
                        '</div><div class="title"> ' + item.linkTitle + '</div ></a>' +
                            '<div class="info">' +
                                '<div class="date">' +
                                    '<img src="/img/clock.png" alt="clock image" />' +
                                        item.publishDate +
                            '</div>' +
                                '<div class="category">' +
                                   '<a href="/blog/' + item.categoryUrl + '" style="color:#' + item.color + '">' +
                                         '<span class="stick">|</span>' + item.categoryTitle +
                                   '</a>' +
                            '</div>' +
                            '</div>' +
                        '</div>';

                    $('.articles-row').append(rows);
                });

                $(preloader).hide();
                if(data.more)
                    $(loadBtn).show();

            }, failure: function (data) {
                console.log(data);
            }, 
            error: function (data) {
                console.log(data);
            } 

        }); 

    });

  
    //Autoplay video

    var VideoJQ = $("#bar-video");
    var Video = document.getElementById("bar-video");

    if (VideoJQ.offset() != undefined) {
        var VideoToTop = $(VideoJQ).offset().top;
        var VideoHeight = $(VideoJQ).height();
        var WindowHeight = $(window).height();

        $(window).scroll(function () {

            var ToTop = $(window).scrollTop();
           
                if (ToTop > VideoToTop - WindowHeight && ToTop < VideoToTop + WindowHeight / 2) {
                    Video.play();
                } else {
                    Video.pause();
                }

        });
    } 

    //Shares
    $("#fb-share").on("click", function () {
        var Social_href = $(this).attr("data-social-href");
        open("http://www.facebook.com/sharer.php?u=" + Social_href,
            "displayWindow",
            "width=300, height=300, left=350, top=170, status=no, toolbar=no, menubar=no");
    });
    $("#google-share").on("click", function () {
        var Social_href = $(this).attr("data-social-href");
        open("https://plus.google.com/share?url=" + Social_href,
            "displayWindow",
            "width=300, height=300, left=350, top=170, status=no, toolbar=no, menubar=no");
    });

});