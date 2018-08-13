let MonitorHelpSurvey = (function () {
    let sharedPages = [];
    sharedPages[0] = `<h4>What do you need a monitor for ?</h4>
                                <div class="radio">
                                    <label>
                                        <input onclick="MonitorHelpSurvey.SetMonitorFor(this)"
                                               type="radio"
                                               name="monitorFor"
                                               value="Basic Computing">
                                        Basic Computing
                                        <span class="badge">
                                            <span class="tooltip">
                                                For tasks like e-mail, social media, surfing the Web and paying bills online, you   probably don't need a high-performance monitor with lots of extra features. Many  lower-priced monitors are available, and you don't have to settle for a small screen.     Even average-sized monitors (23"-24") with Full HD are probably more affordable than    you think.
                                            </span>
                                            ?
                                        </span>
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input onclick="MonitorHelpSurvey.SetMonitorFor(this)"
                                               type="radio"
                                               name="monitorFor"
                                               value="Multipurpose">
                                        Multipurpose
                                        <span class="badge">
                                            <span class="tooltip">
                                                Monitors for multipurpose use provide the display quality and performance you need for  everyday activities, like streaming music and movies, sharing photos and slideshows,     videoconferencing and creating spreadsheets. If you get a larger monitor (27" and up)    or touchscreen monitor, it can double as your home entertainment center.
                                            </span>
                                            ?
                                        </span>
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input onclick="MonitorHelpSurvey.SetMonitorFor(this)"
                                               type="radio"
                                               name="monitorFor"
                                               value="Multimedia/Professional">
                                        Multimedia/Professional
                                        <span class="badge">
                                            <span class="tooltip">
                                                Photographers, graphic designers, video production artists and other multimedia     professionals need a monitor designed for content creation. This means a larger     screen with at least Full HD or Quad HD resolution for more screen space and highly     detailed images.
                                            </span>
                                            ?
                                        </span>
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input onclick="MonitorHelpSurvey.SetMonitorFor(this)"
                                               type="radio"
                                               name="monitorFor"
                                               value="Gaming">
                                        Gaming
                                        <span class="badge">
                                            <span class="tooltip">
                                                Gamers need a monitor that can keep up with the speed and intensity of today's games.   Monitors incorporating NVIDIA G-SYNC or AMD FreeSync technology will work with any    PC, but give you added benefits when paired with a compatible NVIDIA or AMD graphics   card; together, they synchronize the refresh rate between the GPU and the display for  smooth gameplay.
                                            </span>
                                            ?
                                        </span>
                                    </label>
                                </div>`;
    sharedPages[1] = `<h4>What size are you looking for ?
                        <span class="badge">
                            <div class="tooltip">
                            <p>Screen Size</p>
                            
                            <p>Monitor screen size is measured diagonally. As the screen size goes up, usually so does  the price, so consider how much space you have as well as how much you want to  spend.</p>
                            </div>
                         ?
                        </span>
                    </h4>
                    <div class="radio">
                    <label>
                    <input onclick="MonitorHelpSurvey.SetScreenSize(this)" 
                        type="radio"
                        name="sreenSize"
                        value="22 - 24">
                        22" - 24"
                        <span class="badge">
                            <div class="tooltip">
                            These reasonably priced monitors deliver quality performance for viewing e-mail, sharing photos, using MS Office applications and surfing the Web. 
                            </div>
                         ?
                        </span>
                    </label>`;
    //content of selected category
    let basicComputingPages = [];
    let multipurposePages = [];
    let professionalPages = [];
    let gamingPages = [];

    let currentPages = [];
    let index = 0;
    let surveyContent = $('#surveyContent > .modal-body');
    let surveyPagination = $('#surveyPagination');
    let surveyFooter = $('.modal-footer');
    let monitorFor = "";
    let screenSize = "";

    for (var i = 0; i < sharedPages.length; i++) {
        currentPages[i] = sharedPages[i];
    }

    return {
        Begin: function () {
            $('#surveyPage1').html(sharedPages[0]);
            for (var i = 1; i < sharedPages.length; i++) {
                surveyContent.append($(`<div id="surveyPage${i + 1}"></div>`).html(sharedPages[i]).hide());
            }
        },
        NextPage: function () {
            if (index + 1 <= currentPages.length - 1) {
                index++;
                MonitorHelpSurvey.UpdateSurvey();
            }
        },
        PrevPage: function () {
            if (index - 1 >= 0) {
                index--;
                MonitorHelpSurvey.UpdateSurvey();
            }
        },
        UpdateSurvey: function () {
            surveyContent.children().hide();
            $(`#surveyPage${index + 1}`).show();
            surveyPagination.html((index + 1) + "/" + currentPages.length);
        },
        SetMonitorFor: function (radioButton) {
            monitorFor = radioButton.value;
            MonitorHelpSurvey.SwitchSurveyCategory();
        },
        SwitchSurveyCategory: function () {
            currentPages.splice(sharedPages.length);
            if (monitorFor === "Basic Computing") {
                for (var i = 0; i < basicComputingPages.length; i++) {
                    currentPages[sharedPages.length + i] = basicComputingPages[i];
                }
            } else if (monitorFor === "Multipurpose") {
                for (var i = 0; i < multipurposePages.length; i++) {
                    currentPages[sharedPages.length + i] = multipurposePages[i];
                }
            }
            else if (monitorFor === "Multimedia/Professional") {
                for (var i = 0; i < professionalPages.length; i++) {
                    currentPages[sharedPages.length + i] = professionalPages[i];
                }
            }
            else if (monitorFor === "Gaming") {
                for (var i = 0; i < gamingPages.length; i++) {
                    currentPages[sharedPages.length + i] = gamingPages[i];
                }
            }

            surveyContent.children().slice(sharedPages.length + 1).remove();
            for (var i = sharedPages.length; i < currentPages.length; i++) {
                surveyContent.append($(`<div id="surveyPage${i + 1}"></div>`).html(currentPages[i]).hide());
            }

            surveyPagination.html((index + 1) + "/" + currentPages.length);
            surveyFooter.show(600);
        },
        SetScreenSize: function (radioButton) {
            screenSize = radioButton.value;
        },
    };
})();