let UploadImageLinkJs = (function () {
    return {
        AddUploadInputField: function AddUploadInputField() {
            $('#UploadImageFieldContainer').append('<br /><input class=\"form-control\" name=\"uploadImageLink\" type=\"text\" />');
        },
        UploadInputFields: function UploadInputFields() {
            let uploadImageLink = "";

            let uploadImageLinks = $('[name="uploadImageLink"]');
            for (let i = 0; i < uploadImageLinks.length; i++) {
                uploadImageLink += "uploadImageLink=" + uploadImageLinks[i].value;
            }

            $.ajax(
                {
                    type: "POST",
                    url: "/Monitor/UploadImageLinks",
                    data: {
                        uploadImageLink: uploadImageLink,
                        monitorId: $("#monitorId").val()
                    },
                    success: function () {
                        location.reload();
                        UploadImageLinkJs.ResetUploadInputFields();
                    }
                });
        },
        ResetUploadInputFields: function ResetUploadInputFields() {
            $('#UploadImageFieldContainer').children().remove();
            UploadImageLinkJs.AddUploadInputField();
        }
    };
})();

//function UploadInputFields() {
//    let xmlHttp;
//    //Let us create the XML http object  
//    xmlHttp = null;

//    if (window.XMLHttpRequest) {
//        //for new browsers  
//        xmlHttp = new XMLHttpRequest();
//    }
//    else if (window.ActiveXObject) {
//        //for old ones  
//        //xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");  
//        let strName = "Msxml2.XMLHTTP"
//        if (navigator.appVersion.indexOf("MSIE 5.5") >= 0) {
//            strName = "Microsoft.XMLHTTP"
//        }
//        try {
//            xmlHttp = new ActiveXObject(strName);
//        }
//        catch (e) {
//            alert("Error. Scripting for ActiveX might be disabled")
//            return false;
//        }
//    }

//    if (xmlHttp !== null) {
//        //Handle the response of this async request we just made(subscribe to callback)  
//        //xmlHttp.onreadystatechange = state_Change;  
//        let monitorId = $("#monitorId").val();
//        let uploadImageLink = "";

//        let uploadImageLinks = $('[name="uploadImageLink"]');
//        for (let i = 0; i < uploadImageLinks.length; i++) {
//            uploadImageLink += "uploadImageLink=" + uploadImageLinks[i].value;
//        }
        
//        //Pass the value to a web page on server as query string using XMLHttpObject.  
//        xmlHttp.open("POST", "/Monitor/UploadImage", true);
//        xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//        xmlHttp.send("monitorId=" + monitorId + "&uploadImageLink=" + uploadImageLink);
//        ResetUploadInputFields();
//    }
//}  