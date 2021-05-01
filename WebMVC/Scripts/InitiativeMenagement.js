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
