import React from 'react'

export default function InitiativeHeader(props) {
  return (
    <li className="list-group-item" style={{background: '#e7f1ff', color: '#0c63e4', fontWeight: 'bold'}}>
        <div className='row'>
            <div className='col-md-1'>
                Inicjatywa
            </div>
            <div className='col-md-2'>
                Nazwa
            </div>
            <div className='col-md-2'>
                HP
            </div>
            <div className='col-md-5'>
                AC
            </div>
            <div className='col-md-1'>
                <i className="bi bi-plus-circle" onClick={() => {props.onAdd()}}></i>
            </div>
            <div className='col-md-1'>
                <i className="bi bi-x-circle" onClick={() => {props.onClear()}}></i>
            </div>
        </div>
    </li>
  )
}
