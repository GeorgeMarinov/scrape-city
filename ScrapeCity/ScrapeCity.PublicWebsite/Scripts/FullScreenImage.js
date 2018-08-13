let FullScreenImage = (function () {
    let fullScreenImageTemplate = 
        `<div class="img-full-screen-container">
        <span class="img-full-screen-close-button" onclick="FullScreenImage.HideFullScreenImage()">&times;</span>
        <img class="img-full-screen" src="{{src}}" >
        <div class="img-full-screen-caption">{{captionText}}</div>
        </div>`;
    let monitorImagesContainer = $('#monitorImages');

    return {
        ShowFullScreenImage: function (image) {
            //let imageSrc = image.src.replace(/width=[0-9]+/g, 'width=700');
            //imageSrc = imageSrc.replace(/height=[0-9]+/g, 'height=700');
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
    };
})();
