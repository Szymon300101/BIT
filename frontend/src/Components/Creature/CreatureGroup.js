import React from 'react'
import DOMPurify from 'dompurify';
import CreatureTile from './CreatureTile';

export default function CreatureGroup(props) {
    return (
        <>
            <div className="accordion-item">
                <h2 className="accordion-header" id={`accordion-heading-${props.name}`}>
                    <button className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target={`#accordion-collapse-${props.name}`} aria-expanded="true" aria-controls={`accordion-collapse-${props.name}`}>
                        {`${DOMPurify.sanitize(props.name)}`}
                    </button>
                </h2>
                <div id={`accordion-collapse-${props.name}`} className="accordion-collapse collapse show" aria-labelledby={`accordion-heading-${props.name}`}>
                    <div className="accordion-body">
                        <ul className="list-group list-group-flush">
                            {
                                props.items.map(item =>
                                    <div key={item.id} >
                                         <CreatureTile 
                                            item = {item}
                                            connection = {props.connection}
                                            groupList = {props.groupList} 
                                        />
                                    </div>
                                    
                                )
                            }

                        </ul>
                    </div>
                </div>
            </div>
        </>

    )
}
