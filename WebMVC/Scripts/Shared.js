
function loadSyncBreaks() {

    let links = document.getElementsByClassName("nav_link");

    for (var i = 0; i < links.length; i++) {
        links[i].addEventListener("click", breakSyncConnection);
    }

}

function breakSyncConnection() {
    document.getElementById("break-sync-button").classList = "btn btn-default";
    document.getElementById("break-sync-button").innerHTML = "SYNC OFF";
    document.getElementById("break-sync-button").disabled = true;
    if (source != null) {
        source.close();
    }
}


function openedSyncConnection() {
    document.getElementById("break-sync-button").classList = "btn btn-primary";
    document.getElementById("break-sync-button").innerHTML = "SYNC";
    document.getElementById("break-sync-button").disabled = false;
}

