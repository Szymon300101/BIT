import React, {useState} from 'react'

export default function Error(props) {
    
    const [show, setShow] = useState(false);

  return (
      <>
      {show == true ? 
        <div class="alert alert-danger fade show" role="alert">
            <h4 class="alert-heading">Błąd!</h4>
            <strong>Holy guacamole!</strong> You should check in on some of those fields below.
            <button type="button" class="btn-close" onClick={setShow(false)}></button>
        </div> : ""
        }
      </>
      
    
  )
}
