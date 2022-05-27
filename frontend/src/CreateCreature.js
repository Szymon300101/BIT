import React, { useState } from "react";

const CreateCreature = props => {
    const [name, setName] = useState('');
    const [initiativeBonus, setInitiativeBonus] = useState('');
    const [ac, setAc] = useState('');

    const onSubmit = (e) => {
        e.preventDefault();

        const isNameProvided = name && name !== "";
        const isInitiativeBonusProvided = initiativeBonus && initiativeBonus !== "";
        const isAcProvided = ac && ac !== "";

        if(isNameProvided && isInitiativeBonusProvided && isAcProvided){
            props.addCreature(name, initiativeBonus, ac);
        }
        else
        {
            alert("Źle");
        }
    }

    const onNameUpdate = (e) =>{
        setName(e.target.value);
    }

    const onInitiativeBonusUpdate = (e) =>{
        setInitiativeBonus(e.target.value);
    }

    const onAcUpdate = (e) =>{
        setAc(e.target.value);
    }

    return (
        <form 
            onSubmit={onSubmit}>
            <label htmlFor="name">name:</label>
            <br />
            <input 
                id="name" 
                name="name" 
                value={name}
                onChange={onNameUpdate} />
            <br/>
            <label htmlFor="initiativeBonus">initiativeBonus:</label>
            <br />
            <input 
                type="text"
                id="initiativeBonus"
                name="initiativeBonus" 
                value={initiativeBonus}
                onChange={onInitiativeBonusUpdate} />
            <br/>
            <label htmlFor="ac">ac:</label>
            <br />
            <input 
                type="text"
                id="ac"
                name="ac" 
                value={ac}
                onChange={onAcUpdate} />
            <br/><br/>
            <button>Submit</button>
        </form>
    )
}

export default CreateCreature;