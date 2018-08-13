let MonitorPortsJs = (function () {
    //templete for adding port fields
    let portFieldTemplete =
        '<tbody name="~~Ports[#]">'+
            '<tr>' +
                '<td>'+
                    '<div class="checkbox">'+
                        '<select onchange="MonitorPortsJs.updateOptions(this)" name="~~Ports[#].Type" class="form-control rounded-0">'+
                            '~~optionsHtml'+
                        '</select>'+
                    '</div>'+
                '</td>'+
                '<td>'+
                    '<div class="checkbox">'+
                        '<input class="form-control rounded-0" value="1" type="number" name="~~Ports[#].Num" />'+
                    '</div>'+
                '</td>'+
                '<td>'+
                    '<input type="hidden" name="~~Ports[#].Index" value="%" />'+
                '</td>' +
                 '<td>' +
                    '<a name="~~Ports[#]" class="checkbox btn btn-secondary rounded-0" onclick="MonitorPortsJs.removeUploadInputField(this)">&times;</a>' +
                '</td>' +
            '</tr >' +
        '</tbody>';

    //variables
    let options = [];
    let selectedOptions = [];
    let dropdownLists = "";
    let monitorVideoPortIndex = 0;
    let monitorUSBPortIndex = 0;

    return {
        addPortField: function(portType) {
            

            selectedOptions = [];
            let portFieldHtml = portFieldTemplete;
            portFieldHtml = portFieldHtml.replace(/~~Ports/g, portType + 'Ports')
            MonitorPortsJs.loadVideoPortData(portType);
            dropdownLists = $(`#${portType}Ports > tbody > tr > td > div > select`);
            MonitorPortsJs.pushSelectedOptionsIntoArray(dropdownLists);
            MonitorPortsJs.removeOptions(options, selectedOptions);
            let optionsHtml = MonitorPortsJs.getHtmlFromOptionsArray(options);
            if (optionsHtml !== "") {
                let variableIndex = 0;
                if (portType === "video") {
                    variableIndex = monitorVideoPortIndex;
                    monitorVideoPortIndex++;
                } else if (portType === "usb") {
                    variableIndex = monitorUSBPortIndex;
                    monitorUSBPortIndex++;
                }

                portFieldHtml = portFieldHtml.replace('~~optionsHtml', optionsHtml)
                portFieldHtml = portFieldHtml.replace(/\[#\]/g, '[' + variableIndex + ']')
                portFieldHtml = portFieldHtml.replace(/"%"/g, '"' + variableIndex + '"')
                $(`#${portType}Ports`).append(portFieldHtml);
                MonitorPortsJs.updateOptions(portType);
            }
        },
        removeUploadInputField: function() { 
            let buttonName = arguments[0].name.toLowerCase();
            let startIndex = buttonName.indexOf("[")+1;
            let endIndex = buttonName.indexOf("]");
            let indexOfClickedButton = parseInt(buttonName.substring(startIndex, endIndex));
            let portType = "";
            if (buttonName.includes("video")) {
                portType = "video";
                monitorVideoPortIndex--;
            } else if (buttonName.includes("usb")) {
                portType = "usb";
                monitorUSBPortIndex--;
            }

            let inputFields = $(`#${portType}Ports > tbody`);
            let loopCount = inputFields.length;
            for (var i = 0; i < loopCount; i++) {
                if (i === indexOfClickedButton) {
                    inputFields[i].remove();
                }
                if (i > indexOfClickedButton) {
                    let oldIndex = "[" + i + "]";
                    let newIndex = "[" + (i - 1) + "]";
                    inputFields[i].outerHTML = inputFields[i].outerHTML.split(oldIndex).join(newIndex);
                }
            }
        },
        updateOptions: function (portType) {
            if (typeof portType !== 'string') {
                portType = arguments[0].name;
                if (portType.includes("video")) {
                    portType = "video";
                } else if (portType.includes("usb")) {
                    portType = "usb";
                }
            }
            
            selectedOptions = [];
            dropdownLists = $(`#${portType}Ports > tbody > tr > td > div > select`);
            MonitorPortsJs.pushSelectedOptionsIntoArray(dropdownLists);
            dropdownLists.each(function () {
                let value = this.value;
                MonitorPortsJs.loadVideoPortData(portType);
                MonitorPortsJs.removeOptions(options, selectedOptions);
                let optionsHtml = MonitorPortsJs.getHtmlFromOptionsArray(options);
                this.innerHTML = optionsHtml;
                this.innerHTML = (`<option value=\"${value}\">${value}</option>`) + this.innerHTML;
                this.value = value;
            });
        },
        resetPortFields: function(portType) {
            if (portType === 'video') {
                monitorVideoPortIndex = 0;  
            }
            else if (portType === 'usb') {
                monitorUSBPortIndex = 0;
            }

            $(`#${portType}Ports>tbody`).remove();
        },
        setGlobalVideoPortInt: function(number) {
            monitorVideoPortIndex = number;
        },
        setGlobalUSBPortInt: function(number) {
            monitorUSBPortIndex = number;
        },
        //gets all the port options from the dom's viewdata and pushesh them into videoPortData[] array
        loadVideoPortData: function(portType) {
            let data = $(`#${portType}Port-data`).clone().html();
            options = data.split('<br />');
            options.shift();
            for (let i = 0; i < options.length; i++) {
                options[i] = options[i].trim();
            }
        },
        //gets all the currently selected options and stores them in selectedVideoPorts[] array
        pushSelectedOptionsIntoArray: function(dropdownLists) {
            dropdownLists.each(function () {
                let value = this.value;
                if (selectedOptions.indexOf(value) === -1) {
                    selectedOptions.push(value);
                }
            });
        },
        removeOptions: function(options, selectedOptions) {
            for (let i = 0; i < selectedOptions.length; i++) {
                let indexOfselectedOptions = options.indexOf(selectedOptions[i]);
                if (indexOfselectedOptions > -1) {
                    options[indexOfselectedOptions] = "";
                }
            }
        },
        //pass a string array to get select options as html string
        getHtmlFromOptionsArray: function(options) {
            let html = "";
            for (let i = 0; i < options.length; i++) {
                if (options[i] !== "") {
                    html += `<option value=\"${options[i]}\">${options[i]}</option>`;
                }
            }
            return html;
        }
    };
})();