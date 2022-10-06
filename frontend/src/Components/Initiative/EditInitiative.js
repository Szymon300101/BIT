import React, { useState, useEffect } from "react";
import FormInput from "../Helpers/FormInput";
import { UploadImage, AddToInitiative, UpdateInitiative } from "../../Functions/DBActions"
import GetImage from "../../Functions/GetImage";

function newParticipant(){
    return {
        id: 0,
        group: null,
        name: "",
        initiative: 0,
        ac: 10,
        hp: 1,
        maxHP: 1,
        initiativeBonus: 0,
        positionX: 0,
        positionY: 0,
        imagePath: null
    }
}

const hasRequiredFields = (item) => {
    if(!item.name || item.name === "") return false;
    if(item.ac === 0) return false;
    if(item.maxHp === 0) return false;

    return true;
}

const toBackendModel = (item) => {
    return {
        Id: item.id,
        Group: item.group,
        Name: item.name,
        Initiative: 1 * item.initiative,
        AC: 1 * item.ac,
        maxHP: 1 * item.maxHP,
        Hp: 1 * item.hp,
        InitiativeBonus: 1 * item.initiativeBonus ,
        PositionX: 1 * item.positionX ,
        PositionY: 1 * item.positionY ,
        ImagePath: item.imagePath
    }
}

export default function EditInitiative(props) {    

    const [participant, setParticipant] = useState(newParticipant());
    const [imageURL, setImageURL] = useState('');

    useEffect(() => {
        if(participant.imagePath !== ''){
            GetImage(participant.imagePath)
            .then(result => setImageURL(result.url));
        }else{
            setImageURL("")
        }
    }, [participant.imagePath]);

    useEffect(() => {
        if(props.updateMode && props.item !== null){
            setParticipant(props.item);
        }
    }, [props.item]);
    

    const onSubmit = (e) => {
        e.preventDefault();

        if(hasRequiredFields(participant)){
            console.log("Submit");

            if(!props.updateMode){
                AddToInitiative(props.connection, toBackendModel(participant));
            }else{
                UpdateInitiative(props.connection, toBackendModel(participant));
            }

            props.closeModal();
        }
        else
        {
            console.log(participant)
            alert("Brak wymaganych pól.");
        }
    }

    const onImageUpload = (image) => {
        UploadImage(image, (path) => {
            setParticipant({...participant, imagePath: path})
        })
    }

  return (
    <>
    <form 
        onSubmit={onSubmit}>
            <FormInput 
                type = "text" 
                title = "Nazwa" 
                name = "name" 
                value = {participant.name}
                onChange = {(value) => setParticipant({...participant, name: value})} 
                description = {"Nazwa stworzenia"}/>
            <div className="row">
                <div className="col-md-6">
                    <FormInput 
                        type = "number" 
                        title = "HP" 
                        name = "hp" 
                        value = {participant.hp}
                        onChange = {(value) => setParticipant({...participant, hp: value})} 
                        description = {""}/>
                </div>
                <div className="col-md-6">
                    <FormInput 
                        type = "number" 
                        title = "Max HP" 
                        name = "maxHP" 
                        value = {participant.maxHP}
                        onChange = {(value) => setParticipant({...participant, maxHP: value})} 
                        description = {""}/>
                </div>
            </div>
            <div className="row">
                <div className="col-md-6">
                    <FormInput 
                        type = "number" 
                        title = "Inicjatywa" 
                        name = "initiative" 
                        value = {participant.initiative}
                        onChange = {(value) => setParticipant({...participant, initiative: value})} 
                        description = {""}/>
                </div>
                <div className="col-md-6">
                    <FormInput 
                        type = "number" 
                        title = "Bonus do do inicjatywy" 
                        name = "initiativeBonus" 
                        value = {participant.initiativeBonus}
                        onChange = {(value) => setParticipant({...participant, initiativeBonus: value})}
                        description = {""}/>
                </div>
            </div>
            <div className="row">
                <div className="col-md-6">
                    <FormInput 
                        type = "combo" 
                        title = "Grupa" 
                        name = "group" 
                        value = {participant.group}
                        comboBoxList = {props.groupList}
                        onChange = {(value) => setParticipant({...participant, group: value})}
                        description = {""}/>
                </div>
                <div className="col-md-6">
                    <FormInput 
                        type = "number" 
                        title = "AC" 
                        name = "ac" 
                        value = {participant.ac}
                        onChange = {(value) => setParticipant({...participant, ac: value})} 
                        description = {""}/>
                </div>
            </div>

            <input hidden id={`image-upload-${participant.id}`} type="file" onChange={(e) => onImageUpload(e.target.files[0])} />
            <label htmlFor={`image-upload-${participant.id}`} className="image-upload-field">
                {imageURL === '' ? 
                    <div><div className="btn btn-primary" role="button">Prześlij zdjęcie</div></div> : 
                    <img src={imageURL} height="200" width="200"/>}
            </label> <br/>

        <button className="btn btn-primary">Zatwierdź</button>

        {/* <button role="button" className="btn btn-default" onClick={(e) => onClear(e)}>Wyczyść</button>  */}
    </form>
    </>
  )
}
