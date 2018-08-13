$('.confirmation-callback-update-thumbnail').confirmation({
    onConfirm: function (button, imgSrc) {

        let imageId = imgSrc[0].attributes["data-image-id"].nodeValue;
        let monitorId = $("#monitorId").val();
        $.ajax(
            {
                type: "POST",
                url: "/Monitor/UpdateThumbnail",
                data: {
                    monitorId: monitorId,
                    imageId: imageId
                },
                success: function () {
                    location.reload();
                }
            });
    }
});