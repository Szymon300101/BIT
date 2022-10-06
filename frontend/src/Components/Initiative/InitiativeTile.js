import React from 'react'
import DOMPurify from 'dompurify';

export default function InitiativeTile({ participant, onDelete, onEdit }) {
  return (
    <li className="list-group-item">
        <div className='row'>
            <div className='col-md-1'>
                {`${DOMPurify.sanitize(participant.initiative)}`}
            </div>
            <div className='col-md-2'>
                {`${DOMPurify.sanitize(participant.name)}`}
            </div>
            <div className='col-md-2'>
                <b>{`${DOMPurify.sanitize(participant.hp)}`}</b> / {`${DOMPurify.sanitize(participant.maxHP)}`}
            </div>
            <div className='col-md-5'>
                {`${DOMPurify.sanitize(participant.ac)}`}
            </div>
            <div className='col-md-1'>
                <i className="bi bi-pencil-square" onClick={() => onEdit(participant)}></i>
            </div>
            <div className='col-md-1'>
                <i className="bi bi-trash3" onClick={() => onDelete(participant)}></i>
            </div>
        </div>
    </li>
  )
}
