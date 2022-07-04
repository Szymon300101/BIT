import React from 'react'
import DOMPurify from 'dompurify';

export default function Modal(props) {
//btn_text
//btn_className
//id
//body
//title

  return (
    <>
        <button type="button" className={props.btn_className} data-bs-toggle="modal" data-bs-target={"#" + props.id + "-modal"}>
            {`${DOMPurify.sanitize(props.btn_text)}`}
        </button>

        <div className="modal fade" id={props.id + "-modal"} tabIndex="-1" aria-labelledby={props.id + "-modalLabel"} aria-hidden="true">
        <div className="modal-dialog">
            <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id={props.id + "-modalLabel"}>{`${DOMPurify.sanitize(props.title)}`}</h5>
                    <button type="button" id={props.id + "-modal-close"} className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                    {props.body}
                </div>
            </div>
        </div>
        </div>
    </>
  )
}
