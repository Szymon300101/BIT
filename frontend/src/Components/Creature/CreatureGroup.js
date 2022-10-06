import React from 'react'
import DOMPurify from 'dompurify';
import CreatureTile from './CreatureTile';

export default function CreatureGroup({ name, items,  groupList, onEditCreature, onDeleteCreature, onEnrollCreature }) {
    return (
        <>
            <div className="accordion-item">
                <h2 className="accordion-header" id={`accordion-heading-${name}`}>
                    <button className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target={`#accordion-collapse-${name}`} aria-expanded="true" aria-controls={`accordion-collapse-${name}`}>
                        {`${DOMPurify.sanitize(name)}`}
                    </button>
                </h2>
                <div id={`accordion-collapse-${name}`} className="accordion-collapse collapse show" aria-labelledby={`accordion-heading-${name}`}>
                    <div className="accordion-body">
                        <ul className="list-group list-group-flush">
                            {
                                items.map(item =>
                                    <div key={item.id} >
                                         <CreatureTile 
                                            creature = {item}
                                            onEditCreature = {onEditCreature}
                                            onDeleteCreature = {onDeleteCreature}
                                            onEnroll = {onEnrollCreature}

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
