import params from "..";

export function GetInitiative(callback)
{
    fetch(params.localHostPath + "/Initiative/GetInitiative")
    .then(res => res.json())
    .then(
      (result) => {
        callback(result.items);
        console.log(result);
      },
      (error) => {
        console.error(error);
      }
    )
}

export function UploadImage(image, callback){
  const formData = new FormData();
  formData.append("file", image);
  fetch(params.localHostPath + "/Initiative/SaveImg",{
      method: `POST`,
      body: formData,
  })
  .then(res => res.json())
  .then(
      (result) => {
          //console.log(result);

          if(result.error !== ""){
              console.error(result.error);
          }else{
            callback(result.path);
          }
      },
      (error) => {
          console.log(error)
      }
  )
}

export async function AddToInitiative(connection, model){
    try {
        console.log(model);         
        await connection.send('AddToInitiative', model);
    }
    catch(e) {
        console.log(e);
    }
}

export async function UpdateInitiative(connection, model){
    try {
        console.log(model);         
        await connection.send('UpdateInitiative', model);
    }
    catch(e) {
        console.log(e);
    }
}