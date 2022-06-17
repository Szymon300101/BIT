import React, { useState, useEffect } from "react";
import FormInput from "./Helpers/FormInput";

const CreateCreature = props => {
    const [id, setId] = useState(0);
    const [name, setName] = useState('');
    const [initiativeBonus, setInitiativeBonus] = useState(0);
    const [ac, setAc] = useState(10);
    const [maxHP, setMaxHP] = useState(1);
    const [group, setGroup] = useState('');

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
                props.addCreature(name, initiativeBonus, ac, maxHP, group);
            }else{
                props.updateCreature(id, name, initiativeBonus, ac, maxHP, group);
            }
        }
        else
        {
            console.log(id, name, initiativeBonus, ac, maxHP, group)
            alert("Źle");
        }
    }

    useEffect(() => {
        if(props.updateMode){
            
            setId(props.id)
            setName(props.name);
            setInitiativeBonus(props.initiativeBonus);
            setAc(props.ac);
            setMaxHP(props.maxHP);
            setGroup(props.group);
        }
    }, []);

    const onClear = (e) =>{
        e.preventDefault();

        setName('');
        setInitiativeBonus(0);
        setAc(10);
        setMaxHP(1);
        setGroup('');
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

            <button className="btn btn-primary">Zatwierdź</button>

            <button role="button" className="btn btn-default" onClick={(e) => onClear(e)}>Wyczyść</button> 
        </form>
        </>
        
    )
}

export default CreateCreature;