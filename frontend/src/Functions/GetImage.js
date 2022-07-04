import params from "..";

export default function GetImage(path) {
    
    const data = new FormData();
    data.append( "path", path );

    //console.log(path)

    return fetch(params.localHostPath + "/DataAccess/GetImg",{
        method: `POST`,
        body: data,
    })
    .then(res => res.json())
}