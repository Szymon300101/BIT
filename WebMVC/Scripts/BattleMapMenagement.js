











function requestSaveImg() {
    var data = document.getElementById("Upload-File-Button").files[0];

    var formData = new FormData();
    formData.append("fileUploadForm", data);
    $.ajax({
        url: `/BattleMap/SaveImg`,
        cache: false,
        type: "POST",
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            //location.reload();
            document.getElementById("FilePath-Field").value = response.path;
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function dmgModalSetup(id) {
    document.getElementById("DmgCreatureId").value = id;


}