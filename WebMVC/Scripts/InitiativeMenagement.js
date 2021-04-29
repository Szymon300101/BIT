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
