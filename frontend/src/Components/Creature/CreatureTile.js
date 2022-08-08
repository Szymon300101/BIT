import React from 'react'
import DOMPurify from 'dompurify';

import CreateCreature from './CreateCreature';
import Modal from "../Helpers/Modal";

export default function CreatureTile(props) {


    const removeCreature = async (id) => {
        try {
            console.log(id);         
            await props.connection.send('RemoveCreature', id);
        }
        catch(e) {
            console.log(e);
        }
    }

  return (
    <li key={props.item.id} className="list-group-item">
        <div className='row'>
            <div className='col-md-8' key={'Name'}>
                {`${DOMPurify.sanitize(props.item.name)}`}
            </div>
            <div className='col-md-1' key={'Edit'}>
                <Modal 
                    btn_text = ""
                    btn_className = "btn btn-sm bi bi-pencil-square no-padding"
                    id = {`update-${props.item.id}`}
                    body = {<CreateCreature 
                                connection = {props.connection}
                                groupList = {props.groupList} 
                                closeModal = {() => document.getElementById(`update-${props.item.id}-modal-close`).click()}
                                updateMode = {true}

                                item = {props.item}
                            />}
                    title = "Zmień stworzenie"
                />
            </div>
            <div className='col-md-1' key={'Remove'}>
                <i className="bi bi-trash3" onClick={() => removeCreature(props.item.id)}></i>
            </div>
            <div className='col-md-1' key={'Use'}>
                <i className="bi bi-arrow-right-square"></i>
            </div>
        </div>
    </li>
  )
}
