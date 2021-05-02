window.onload = load;

function load() {

    $('#myTabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })


}



function highlightButton(id) {
    var button = document.getElementById(`${id}-Submit-Button`);
    button.classList = "btn btn-danger";
}


function requestSaveImg() {
    var data = document.getElementById("Upload-File-Button").files[0];

    var formData = new FormData();
    formData.append("fileUploadForm", data);
    $.ajax({
        url: `/Creature/SaveImg`,
        cache: false,
        type: "POST",
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

//function fillZero() {
//    if (document.getElementById("Initiative-Input").value == "")
//        document.getElementById("Initiative-Input").value = 0;
//    if (document.getElementById("HP-Input").value == "")
//        document.getElementById("HP-Input").value = 0;
//    if (document.getElementById("InitiativeBonus-Input").value == "")
//        document.getElementById("InitiativeBonus-Input").value = 0;
//    if (document.getElementById("AC-Input").value == "")
//        document.getElementById("AC-Input").value = 0;
//}
