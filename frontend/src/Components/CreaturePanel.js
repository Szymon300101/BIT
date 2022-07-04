import React, {useState, useEffect} from 'react'
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

import CreateCreature from './CreateCreature';
import CreatureGroup from './CreatureGroup';
import Modal from "./Helpers/Modal";
import params from '..';

export default function CreaturePanel() {

    const [state, setState] = useState([]);
    const [ connection, setConnection ] = useState(null);
    const [ groupList, setGroupList ] = useState([]);

    function getCreatures()
    {
        fetch(params.localHostPath + "/Initiative/GetCreatures")
        .then(res => res.json())
        .then(
          (result) => {
            setState({
              isLoaded: true,
              items: result.items
            });
            console.log(result);
          },
          // Uwaga: to ważne, żeby obsłużyć błędy tutaj, a
          // nie w bloku catch(), aby nie przetwarzać błędów
          // mających swoje źródło w komponencie.
          (error) => {
            setState({
              isLoaded: true,
              error
            });
          }
        )
    }

    useEffect(() => {
        getCreatures();
    }, []);

    useEffect(() => {
        setGroupList([... new Set(state?.items?.map(item => item.group))])
    }, [state]);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
        .withUrl('https://localhost:7131/hubs/initiative', {
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets
          })
        .withAutomaticReconnect()
        .build();
    
        setConnection(newConnection);
    }, []);

    
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
    

    
    const addCreature = async (name, initiativeBonus, ac, maxHP, group, imagePath) => {
        const model = {
            Id: 0,
            Name: name,
            AC: 1 * ac,
            MaxHP: 1 * maxHP,
            Group: group,
            InitiativeBonus: 1 * initiativeBonus ,
            ImagePath: imagePath
        };

        try {
            console.log(model);         
            await connection.send('AddCreature', model);
        }
        catch(e) {
            console.log(e);
        }
    }
    
    const updateCreature = async (id, name, initiativeBonus, ac, maxHP, group, imagePath) => {
        const model = {
            Id: id,
            Name: name,
            AC: 1 * ac,
            MaxHP: 1 * maxHP,
            Group: group,
            InitiativeBonus: 1 * initiativeBonus ,
            ImagePath: imagePath
        };

        try {
            console.log(model);         
            await connection.send('UpdateCreature', model);
        }
        catch(e) {
            console.log(e);
        }
    }

    const removeCreature = async (id) => {
        try {
            console.log(id);         
            await connection.send('RemoveCreature', id);
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
                        addCreature = {addCreature}
                        groupList = {groupList} 
                        closeModal = {() => {document.getElementById('create-modal-close').click()}}
                        updateMode = {false}
                    />}
            title = "Dodaj stworzenie"
        />
            <div className="accordion" id="accordion-creatures">
                {
                    groupList.map( group =>
                        <div key = {group} >
                            <CreatureGroup 
                                name = {group} 
                                items = { state.items.filter(item => item.group === group)} 
                                removeCreature = {removeCreature}
                                updateCreature = {updateCreature}
                                groupList = {groupList}
                            /> 
                        </div>
                    ) ?? ""
                }
            </div>
            
        </>
    );
}