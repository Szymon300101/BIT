import React from 'react'
import DOMPurify from 'dompurify';

import ComboBox from 'react-responsive-combo-box'
import 'react-responsive-combo-box/dist/index.css'

export default function FormInput(props) {
//name
//title
//type
//value
//onChange
//description
//comboBoxList

  return (
    <div className="mb-3">
        <label htmlFor={props.name} className="form-label">{`${DOMPurify.sanitize(props.title)}`}</label>
        {
          props.type === "combo" ?
            <ComboBox 
              options = {props.comboBoxList ?? []} 
              name = {props.name} 
              className="full-width" 
              inputClassName='form-control' 
              enableAutocomplete 
              id={props.name} 
              onChange={(e) => props.onChange(e.target.value)} 
              defaultValue={props.value} 
              onSelect={(value) => props.onChange(value)}
              onOptionsChange={(value) => props.onChange(value)}
              aria-describedby={props.name + "-desc"}/>
          :
            <input 
              type={props.type} 
              className="form-control" 
              id={props.name} 
              onChange={(e) => props.onChange(e.target.value)} 
              value={props.value} 
              aria-describedby={props.name + "-desc"} />
        }
          <div id={props.name + "-desc"} className="form-text">{`${DOMPurify.sanitize(props.description)}`}</div>
    </div>
  )
}
