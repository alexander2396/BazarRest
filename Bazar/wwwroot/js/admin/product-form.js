var ImagesCount = $(".product-image").length;
var ImagesContainer = $("#product-images-container");

$("#add-image-btn").click(function () {

    var ImageContainer = $('<div/>', {
        'class': 'mt-5 product-attachment'
    });

    var ImageFileContainer = $('<div/>', {
        'class': 'form-group'
    });
    var ImageFileInput = $('<input/>', {
        'class': 'UploadedImageInput',
        'type': 'file',
        'name': 'ProductImages[' + ImagesCount + '].FileName'
    });
    var ImagePreview = $('<img/>', {
        'class': 'image-preview',
        'src': '#'
    });
    ImageFileInput.appendTo(ImageFileContainer);
    ImagePreview.appendTo(ImageFileContainer);

    var ImageSrcContainer = $('<div/>', {
        'class': 'form-group'
    });
    var ImageSrcLabel = $('<label/>', {
        'html': 'Ссылка с youtube'
    });
    var ImageSrcInput = $('<input/>', {
        'class': 'form-control',
        'type': 'text',
        'name': 'ProductImages[' + ImagesCount + '].Link'
    });
    ImageSrcLabel.appendTo(ImageSrcContainer);
    ImageSrcInput.appendTo(ImageSrcContainer);

    var ImageTitleContainer = $('<div/>', {
        'class': 'form-group'
    });
    var ImageTitleLabel = $('<label/>', {
        'html': 'Title изображения'
    });
    var ImageTitleInput = $('<input/>', {
        'class': 'form-control',
        'type': 'text',
        'name': 'ProductImages[' + ImagesCount + '].Title'
    });
    ImageTitleLabel.appendTo(ImageTitleContainer);
    ImageTitleInput.appendTo(ImageTitleContainer);

    var ImageAltContainer = $('<div/>', {
        'class': 'form-group'
    });
    var ImageAltLabel = $('<label/>', {
        'html': 'Alt изображения'
    });
    var ImageAltInput = $('<input/>', {
        'class': 'form-control',
        'type': 'text',
        'name': 'ProductImages[' + ImagesCount + '].Alt'
    });
    ImageAltLabel.appendTo(ImageAltContainer);
    ImageAltInput.appendTo(ImageAltContainer);

    ImageFileContainer.appendTo(ImageContainer);
    ImageSrcContainer.appendTo(ImageContainer);
    ImageTitleContainer.appendTo(ImageContainer);
    ImageAltContainer.appendTo(ImageContainer);

    //ImageContainer.appendTo(ImagesContainer);
    ImageContainer.insertAfter($("#add-image-btn"));

    ImagesCount++;
});

$(".btn-remove-image").click(function () {

    $(this).siblings(".is-deleted-input").val("true");
    $(this).parent(".product-image").hide();

});