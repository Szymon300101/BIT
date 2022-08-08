import React, {useState, useEffect} from 'react'
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

import params from '../..';

export default function InitiativePanel(props) {

    const [state, setState] = useState([]);
    const [ connection, setConnection ] = useState(props.connection);

    function getInitiative()
    {
        fetch(params.localHostPath + "/Initiative/GetInitiative")
        .then(res => res.json())
        .then(
          (result) => {
            setState({
              isLoaded: true,
              items: result.items
            });
            console.log(result);
          },
          (error) => {
            setState({
              isLoaded: true,
              error
            });
          }
        )
    }

    useEffect(() => {
        getInitiative();
    }, []);
    
    

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected initiative!');
    
                    connection.on('RefreshInitiative', message => {
                        getInitiative();
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

  return (
    <>
        {
            state.items?.map( item =>
                <div key={item.id}>
                    {item.name}
                    <br/>
                </div>
                ) ?? ""
        }
    </>
  )
}
