import React, { useState, useEffect } from "react";
import FormInput from "../Helpers/FormInput";
import params from "../..";
import GetImage from "../../Functions/GetImage";

class Creature{
    constructor(){
        this.id = 0;
        this.name = "";
        this.ac = 10;
        this.maxHp = 10;
        this.group = "";
        this.initiativeBonus = 0;
        this.imagePath = "";
    }
}

const hasRequiredFields = (creature) => {
    if(!creature.name || creature.name == "") return false;
    if(!creature.group || creature.group == "") return false;
    if(!creature.ac || creature.ac === 0) return false;
    if(!creature.maxHp || creature.maxHp === 0) return false;

    return true;
}

const toBackendModel = (creature) => {
    return {
        Id: creature.id,
        Name: creature.name,
        AC: 1 * creature.ac,
        MaxHP: 1 * creature.maxHp,
        Group: creature.group,
        InitiativeBonus: 1 * creature.initiativeBonus ,
        ImagePath: creature.imagePath
    }
}

const CreateCreature = props => {
    const [creature, setCreature] = useState(new Creature());
    const [imageURL, setImageURL] = useState('');

    const onSubmit = (e) => {
        e.preventDefault();

        if(hasRequiredFields(creature)){
            console.log("Submit");

            if(!props.updateMode){
                addCreature(toBackendModel(creature));
            }else{
                updateCreature(toBackendModel(creature));
            }

            props.closeModal();
        }
        else
        {
            console.log(creature)
            alert("Brak wymaganych pól.");
        }
    }

    useEffect(() => {
        if(props.updateMode && props.item !== null){
            let newCreature = new Creature();
            newCreature.id = props.item.id;
            newCreature.name = props.item.name;
            newCreature.maxHp = props.item.maxHP;
            newCreature.ac = props.item.ac;
            newCreature.group = props.item.group;
            newCreature.initiativeBonus = props.item.initiativeBonus;
            newCreature.imagePath = props.item.imagePath;
            setCreature(newCreature);
        }
    }, [props.item]);

    useEffect(() => {
        if(creature.imagePath !== ''){
            GetImage(creature.imagePath)
            .then(result => setImageURL(result.url));
        }else{
            setImageURL("")
        }
    }, [creature.imagePath]);

    const onClear = (e) =>{
        e.preventDefault();

        setCreature(new Creature())
    }

    const onImageUpload = (image) => {

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
                    setCreature({...creature, imagePath: result.path})
                }
            },
            (error) => {
                console.log(error)
            }
        )
    }

    const addCreature = async (model) => {

        try {
            console.log(model);         
            await props.connection.send('AddCreature', model);
        }
        catch(e) {
            console.log(e);
        }
    }
    
    const updateCreature = async (model) => {

        try {
            console.log(model);         
            await props.connection.send('UpdateCreature', model);
        }
        catch(e) {
            console.log(e);
        }
    }

    return (
        <>
        <form 
            onSubmit={onSubmit}>
                <FormInput 
                    type = "text" 
                    title = "Nazwa" 
                    name = "name" 
                    value = {creature.name}
                    onChange = {(value) => setCreature({...creature, name: value})} 
                    description = {"opis"}/>
                <div className="row">
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "Max HP" 
                            name = "maxHP" 
                            value = {creature.maxHp}
                            onChange = {(value) => setCreature({...creature, maxHp: value})} 
                            description = {""}/>
                    </div>
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "AC" 
                            name = "ac" 
                            value = {creature.ac}
                            onChange = {(value) => setCreature({...creature, ac: value})}
                            description = {""}/>
                    </div>
                </div>
                
                <div className="row">
                    <div className="col-md-6">
                        <FormInput 
                            type = "combo" 
                            title = "Grupa" 
                            name = "group" 
                            value = {creature.group}
                            comboBoxList = {props.groupList}
                            onChange = {(value) => setCreature({...creature, group: value})}
                            description = {""}/>
                    </div>
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "Bonus do do inicjatywy" 
                            name = "initiativeBonus" 
                            value = {creature.initiativeBonus}
                            onChange = {(value) => setCreature({...creature, initiativeBonus: value})}
                            description = {""}/>
                    </div>
                </div>

                <input hidden id={`image-upload-${creature.id}`} type="file" onChange={(e) => onImageUpload(e.target.files[0])} />
                <label htmlFor={`image-upload-${creature.id}`} className="image-upload-field">
                    {imageURL === '' ? 
                        <div><div className="btn btn-primary" role="button">Prześlij zdjęcie</div></div> : 
                        <img src={imageURL} height="200" width="200"/>}
                </label> <br/>

            <button className="btn btn-primary">Zatwierdź</button>

            <button role="button" className="btn btn-default" onClick={(e) => onClear(e)}>Wyczyść</button> 
        </form>
        </>
        
    )
}

export default CreateCreature;