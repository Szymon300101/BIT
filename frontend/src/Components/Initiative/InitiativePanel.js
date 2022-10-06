import React, {useState, useEffect} from 'react'

import { GetInitiative } from '../../Functions/DBActions';
import InitiativeHeader from './InitiativeHeader';
import InitiativeTile from './InitiativeTile';
import EditInitiative from './EditInitiative';
import Modal from '../Helpers/Modal';

export default function InitiativePanel({ connection }) {

    const [participants, setParticipants] = useState([]);

    const [participantToEdit, setParticipantToEdit] = useState(null);

    

    useEffect(() => {
        GetInitiative((items) => {
          setParticipants(items);
        });
    }, []);
    
    useEffect(() => {
        if (connection) {
            connection.on('RefreshInitiative', message => {
                GetInitiative((items) => {
                  setParticipants(items);
                });
              });
        }
    }, [connection]);

    const onDeleteParticipant = async (participant) => {
        try {
          console.log(participant);
          await connection.send('RemoveFromInitiative', participant.id);
      }
      catch(e) {
          console.log(e);
      }
    }

    const onAddParticipant = () => {
      document.getElementById('createInit-modal-open').click()
    }

    const onEditParticipant = (item) => {
      setParticipantToEdit(item);
      document.getElementById('updateInit-modal-open').click()
    }

    const onClear = () => {
      participants?.map(item => onDeleteParticipant(item))
    }

  return (
    <>
      <ul className="list-group" style={{marginTop: '55px'}}>
          <InitiativeHeader onAdd = {onAddParticipant} onClear = {onClear}/>
          {
            participants?.map( item =>
              <InitiativeTile key={item.id} participant={item} onDelete = {onDeleteParticipant} onEdit = {onEditParticipant}/>
            ) ?? ""
          }
      </ul>
      <Modal 
            btn_text = "Nowy"
            btn_className = "hidden"
            id = "createInit"
            body = {<EditInitiative
                      connection = {connection}
                      closeModal = {() => {document.getElementById('createInit-modal-close').click()}}
                      updateMode = {false}
                    />}
            title = "Dodaj stworzenie"
        />
        <Modal 
            btn_text = ""
            btn_className = "hidden"
            id = "updateInit"
            body = {<EditInitiative 
                        connection = {connection}
                        closeModal = {() => {document.getElementById('updateInit-modal-close').click()}}
                        updateMode = {true}
                        item = {participantToEdit}
                    />}
            title = "Edytuj stworzenie"
        />
    </>
  )
}
