import React from 'react'
import DOMPurify from 'dompurify';

import CreateCreature from './CreateCreature';
import Modal from "../Helpers/Modal";

export default function CreatureTile({ creature, onEditCreature, onDeleteCreature }) {



  return (
    <li key={creature.id} className="list-group-item">
        <div className='row'>
            <div className='col-md-8' key={'Name'}>
                {`${DOMPurify.sanitize(creature.name)}`}
            </div>
            <div className='col-md-1' key={'Edit'}>
                <i className="bi bi-pencil-square" onClick={() => onEditCreature(creature)}></i>
            </div>
            <div className='col-md-1' key={'Remove'}>
                <i className="bi bi-trash3" onClick={() => onDeleteCreature(creature)}></i>
            </div>
            <div className='col-md-1' key={'Use'}>
                <i className="bi bi-arrow-right-square"></i>
            </div>
        </div>
    </li>
  )
}
