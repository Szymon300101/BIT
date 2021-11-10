
function loadSyncBreaks() {

    let links = document.getElementsByClassName("nav_link");

    for (var i = 0; i < links.length; i++) {
        links[i].addEventListener("click", breakSyncConnection);
    }

}



function breakSyncConnection() {
    console.log("call")
    if (source != null) {
        source.close();
    }
}