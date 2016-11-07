function handleError(response) {
    alert("Invalid server response. \nServer message: " + response.responseText);
}

// If server sends an error, alert is sent to user and returns false
// If success, returns true
function handleServerError(serverResponse) {
    if (serverResponse == null) { // No response means no error -> success
        return true;
    }
    var errorType = serverResponse['error_type'];
    var errorMsg = serverResponse['error_msg'];
    if (errorType != null) {
        alert(errorMsg == null ? errorType : errorMsg);
    }
    return errorType == null
}

// Learning functions
function updateLearning(utfCode, intent, callback) {
    var request = [ "<query>", "<character>", utfCode, "</character><intent>", intent, "</intent></query>" ].join(""); 
    $.ajax({
        url: "/Dictionary/Queries/LearningUpdate.ashx",
        type: "post",
        dataType: "xml",
        data: request,
        success: function (data) {
            if (handleServerError(data)) {
                callback(data);
            }
        },
        error: handleError
    });
}