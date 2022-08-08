import React, {useState, useEffect} from 'react'
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

import CreateCreature from './CreateCreature';
import CreatureGroup from './CreatureGroup';
import Modal from '../Helpers/Modal';
import params from '../..';

export default function CreaturePanel(props) {

    const [state, setState] = useState([]);
    const [ connection, setConnection ] = useState(props.connection);
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
        setGroupList([...new Set(state?.items?.map(item => item.group))])
    }, [state]);

    
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
            <div className="accordion" id="accordion-creatures">
                {
                    groupList.map( group =>
                        <div key = {group} >
                            <CreatureGroup 
                                name = {group} 
                                items = { state.items.filter(item => item.group === group)} 
                                connection = {connection}
                                groupList = {groupList}
                            /> 
                        </div>
                    ) ?? ""
                }
            </div>
            
        </>
    );
}