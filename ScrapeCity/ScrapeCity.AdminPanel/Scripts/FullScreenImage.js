//called from image with class"img-thumbnail" and onclick event from dom
//uses FullScreenImage.css to work
//images container must have id="monitorImages"

let FullScreenImage = (function () {
    let fullScreenImageTemplate = 
        `<div class="img-full-screen-container">
        <span class="img-full-screen-close-button" onclick="FullScreenImage.HideFullScreenImage()">&times;</span>
        <span class="img-full-screen-set-as-thumbnail-button" id="setThumbnail" onclick="FullScreenImage.SetImageAsThumbnail()">Thumbnail</span>
        <span class="img-full-screen-delete-button" id="deleteImage" onclick="FullScreenImage.DeleteImage()">Delete</span>
        <img class="img-full-screen" src="{{src}}" >
        <div class="img-full-screen-caption">{{captionText}}</div>
        </div>`;
    let monitorImagesContainer = $('#monitorImages');
    let currentPictureId;
    let monitorId;

    return {
        ShowFullScreenImage: function (image) {
            //let imageSrc = image.src.replace(/width=[0-9]+/g, 'width=700');
            //imageSrc = imageSrc.replace(/height=[0-9]+/g, 'height=700');
            currentPictureId = image.attributes["data-image-id"].nodeValue;
            monitorId = image.attributes["data-monitor-id"].nodeValue;
            let imageSrc = image.src.replace('?width=150&height=150', '');
            let fullScreenImageHtml = fullScreenImageTemplate.replace(/{{captionText}}/g, image.alt);
            fullScreenImageHtml = fullScreenImageHtml.replace(/{{src}}/g, imageSrc);
            monitorImagesContainer.append(fullScreenImageHtml);
            document.addEventListener('keyup',
                function HideFullScreenImageOnEsc (button) {
                    if (button.key === "Escape") {
                        $('.img-full-screen-container').remove();
                        document.removeEventListener('keyup', HideFullScreenImageOnEsc,true);
                    }
                },true);
        },
        HideFullScreenImage: function () {
            $('.img-full-screen-container').remove();
        }
        ,
        SetImageAsThumbnail: function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/Monitor/UpdateThumbnail",
                    data: {
                        monitorId: monitorId,
                        imageId: currentPictureId
                    },
                    success: function () {
                        location.reload();
                    }
                });
        },
        DeleteImage: function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/Monitor/DeleteImage",
                    data: {
                        monitorId: monitorId,
                        imageId: currentPictureId
                    },
                    success: function () {
                        location.reload();
                    }
                });
        }
    };
})();
