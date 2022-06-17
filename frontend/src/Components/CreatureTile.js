import React from 'react'
import DOMPurify from 'dompurify';

import CreateCreature from './CreateCreature';
import Modal from "./Helpers/Modal";

export default function CreatureTile(props) {
  return (
    <li key={props.item.id} className="list-group-item">
        <div className='row'>
            <div className='col-md-9' key={'Name'}>
                {`${DOMPurify.sanitize(props.item.name)}`}
            </div>
            <div className='col-md-1' key={'Edit'}>
                <Modal 
                    btn_text = ""
                    btn_className = "btn btn-sm bi bi-pencil-square no-padding"
                    id = {`update-${props.item.id}`}
                    body = {<CreateCreature 
                                updateCreature = {props.updateCreature}
                                groupList = {props.groupList} 
                                close = {null}
                                updateMode = {true}

                                id = {props.item.id}
                                name = {props.item.name}
                                initiativeBonus = {props.item.initiativeBonus}
                                maxHP = {props.item.maxHP}
                                ac = {props.item.ac}
                                group = {props.item.group}
                            />}
                    title = "Zmień stworzenie"
                />
            </div>
            <div className='col-md-1' key={'Remove'}>
                <i className="bi bi-trash3" onClick={() => props.removeCreature(props.item.id)}></i>
            </div>
            <div className='col-md-1' key={'Use'}>
                <i className="bi bi-arrow-right-square"></i>
            </div>
        </div>
    </li>
  )
}
