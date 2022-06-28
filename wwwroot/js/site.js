// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getShortUrl() {
    $.ajax({
        url: '/Home/CreateUrl',
        dataType: "json",
        async: true,
        jsonp: false,
        type: "POST",
        data: {
            url: $("#OriginalUrl").val(),
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Error: " + errorThrown);
        },
        success: function (data) {
            //console.log(data); 

            if (data.validUrl == true) {
                $("#shortenedOutput").html('<a href="' + data.url.shortUrl + '" target="_blank" id=shortened>' + data.url.shortUrl + '</a>  <button class="btn btn-primary" onclick="copyUrl()">Copy Url</button>');

            } else {
                if (data.originalUrl != null) {
                    $("#shortenedOutput").html('<p>' + data.originalUrl + ' is not a valid URL. ' + data.message + '</p > ');
                } else {
                    $("#shortenedOutput").html('<p>' + data.message + '</p > ');

                }

            }
        }
    });
}

function getCopiedUrl() {
    //Get the text field 
    var copyText = document.getElementById("shortened");
    /* Copy the text inside the text field */
    navigator.clipboard.writeText(copyText.innerText);
    ///* Alert the copied text */
    alert("Copied: " + copyText.innerText);
}