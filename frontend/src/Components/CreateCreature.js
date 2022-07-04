import React, { useState, useEffect } from "react";
import FormInput from "./Helpers/FormInput";
import params from "..";
import axios from "axios";
import jquery from "jquery";
import GetImage from "../Functions/GetImage";

const CreateCreature = props => {
    const [id, setId] = useState(0);
    const [name, setName] = useState('');
    const [initiativeBonus, setInitiativeBonus] = useState(0);
    const [ac, setAc] = useState(10);
    const [maxHP, setMaxHP] = useState(1);
    const [group, setGroup] = useState('');
    const [imageURL, setImageURL] = useState('');
    const [imagePath, setImagePath] = useState('');

    const onSubmit = (e) => {
        e.preventDefault();

        const isNameProvided = name && name !== "";
        const isInitiativeBonusProvided = initiativeBonus && initiativeBonus !== "";
        const isAcProvided = ac && ac !== 0;
        const isMaxHpProvided = maxHP && maxHP !== 0;
        const isGroupProvided = group && group !== "";

        if(isNameProvided && isInitiativeBonusProvided && isAcProvided && isMaxHpProvided && isGroupProvided){
            console.log("Submit");

            if(!props.updateMode){
                props.addCreature(name, initiativeBonus, ac, maxHP, group, imagePath);
            }else{
                props.updateCreature(id, name, initiativeBonus, ac, maxHP, group, imagePath);
            }

            props.closeModal();
        }
        else
        {
            console.log(id, name, initiativeBonus, ac, maxHP, group)
            alert("Źle");
        }
    }

    useEffect(() => {
        if(props.updateMode){
            
            setId(props.item.id)
            setName(props.item.name);
            setInitiativeBonus(props.item.initiativeBonus);
            setAc(props.item.ac);
            setMaxHP(props.item.maxHP);
            setGroup(props.item.group);
            setImagePath(props.item.imagePath)
        }
    }, []);

    useEffect(() => {
        if(imagePath !== ''){
            GetImage(imagePath)
            .then(result => setImageURL(result.url));
        }
    }, [imagePath, '']);

    const onClear = (e) =>{
        e.preventDefault();

        setName('');
        setInitiativeBonus(0);
        setAc(10);
        setMaxHP(1);
        setGroup('');
        setImagePath('');
        setImageURL('');
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
                    setImagePath(result.path)
                }
            },
            (error) => {
                console.log(error)
            }
        )
    }

    return (
        <>
        <form 
            onSubmit={onSubmit}>
                <FormInput 
                    type = "text" 
                    title = "Nazwa" 
                    name = "name" 
                    value = {name}
                    onChange = {(value) => setName(value)} 
                    description = {"opis"}/>
                <div className="row">
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "Max HP" 
                            name = "maxHP" 
                            value = {maxHP}
                            onChange = {(value) => setMaxHP(value)} 
                            description = {""}/>
                    </div>
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "AC" 
                            name = "ac" 
                            value = {ac}
                            onChange = {(value) => setAc(value)} 
                            description = {""}/>
                    </div>
                </div>
                
                <div className="row">
                    <div className="col-md-6">
                        <FormInput 
                            type = "combo" 
                            title = "Grupa" 
                            name = "group" 
                            value = {group}
                            comboBoxList = {props.groupList}
                            onChange = {(value) => setGroup(value)} 
                            description = {""}/>
                    </div>
                    <div className="col-md-6">
                        <FormInput 
                            type = "number" 
                            title = "Bonus do do inicjatywy" 
                            name = "initiativeBonus" 
                            value = {initiativeBonus}
                            onChange = {(value) => setInitiativeBonus(value)} 
                            description = {""}/>
                    </div>
                </div>

                <input hidden id={`image-upload-${id}`} type="file" onChange={(e) => onImageUpload(e.target.files[0])} />
                <label htmlFor={`image-upload-${id}`} className="image-upload-field">
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