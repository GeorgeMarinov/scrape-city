let ShowMoreMonitorCards = (function () {

    let trackShown = {

    }

    let navLinks = $('.nav-tabs--vertical .nav-item .nav-link');
    for (var i = 0; i < navLinks.length; i++) {
        console.log();
        trackShown[navLinks[i].hash.substring(1)] = 10;

    }

    return {
        GetCards: function (button) {
            let brand = button.attributes["data-brand"].nodeValue;
            let container = $(`#${brand}`).children().first();
            let html = ``;
            button.setAttribute('disabled', '');
            $.ajax(
                {
                    type: "POST",
                    url: "/Monitor/ShowMoreMonitors",
                    data: {
                        brand: brand,
                        offset: trackShown[brand]
                    },
                    success: function (data) {
                        for (var i = 0; i < data.length; i++) {
                            html += `<div class="card mr-1 mb-1" style="max-width:200px">
                                        <img class="card-img-top" src="${data[i].Thumbnail}?width=200&height=200">
                                        <div class="card-footer">
                                            <p class="text-center">${data[i].Model}</p>
                                            <a href="/Monitor/Details/${data[i].Id}"
                                               class="btn btn-secondary rounded-0">Details</a>
                                            <a href="/Monitor/Edit/${data[i].Id}"
                                               class="btn btn-secondary rounded-0">Edit</a>
                                        </div>
                                    </div>`;
                        }
                        container.append(html);
                        trackShown[brand] = trackShown[brand] + 10;
                        button.removeAttribute('disabled');
                    }
                });
        }
    };
})();