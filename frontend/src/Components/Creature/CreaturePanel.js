import React, {useState, useEffect} from 'react'
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

import CreateCreature from './CreateCreature';
import CreatureGroup from './CreatureGroup';
import Modal from '../Helpers/Modal';
import params from '../..';

export default function CreaturePanel({ connection }) {

    const [creatures, setCreatures] = useState([]);
    const [ groupList, setGroupList ] = useState([]);

    const [ creatureToEdit, setCreatureToEdit ] = useState(null);

    function getCreatures()
    {
        fetch(params.localHostPath + "/Initiative/GetCreatures")
        .then(res => res.json())
        .then(
          (result) => {
            setCreatures(result.items);
            console.log(result);
          },
          (error) => {
              console.error(error);
          }
        )
    }

    useEffect(() => {
        getCreatures();
    }, []);

    useEffect(() => {
        setGroupList([...new Set(creatures?.map(item => item.group))])
    }, [creatures]);

    
    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');
    
                    connection.on('RefreshCreatures', message => {
                        getCreatures();
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);
    
    const onEditCreature = (creature) => {
        setCreatureToEdit(creature);
        document.getElementById('update-modal-open').click()
    }

    const onDeleteCreature = async (creature) => {
        try {
            console.log(creature);         
            await connection.send('RemoveCreature', creature.id);
        }
        catch(e) {
            console.log(e);
        }
    }
    

    

    return (
        <>
        <Modal 
            btn_text = "Nowy"
            btn_className = "btn btn-primary margin"
            id = "create"
            body = {<CreateCreature 
                        connection = {connection}
                        groupList = {groupList} 
                        closeModal = {() => {document.getElementById('create-modal-close').click()}}
                        updateMode = {false}
                    />}
            title = "Dodaj stworzenie"
        />
        <Modal 
            btn_text = ""
            btn_className = "btn btn-primary margin hidden"
            id = "update"
            body = {<CreateCreature 
                        connection = {connection}
                        groupList = {groupList} 
                        closeModal = {() => {document.getElementById('update-modal-close').click()}}
                        updateMode = {true}
                        item = {creatureToEdit}
                    />}
            title = "Edytuj stworzenie"
        />
            <div className="accordion" id="accordion-creatures">
                {
                    groupList.map( group =>
                        <div key = {group} >
                            <CreatureGroup 
                                name = {group} 
                                items = { creatures.filter(item => item.group === group)} 
                                groupList = {groupList}
                                onEditCreature = {onEditCreature}
                                onDeleteCreature = {onDeleteCreature}
                            /> 
                        </div>
                    ) ?? ""
                }
            </div>
            
        </>
    );
}