import React, {useState, useEffect} from 'react'
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";
import Navbar from './Components/Navbar';
import CreaturePanel from './Components/Creature/CreaturePanel';
import InitiativePanel from './Components/Initiative/InitiativePanel';

import params from '.';

function InitPage() {

  const [ connection, setConnection ] = useState(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:7131/hubs/initiative', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
    .withAutomaticReconnect()
    .build();

    setConnection(newConnection);
}, []);

  return (
    <div style={{ width: '90%', margin: 'auto' }}>
      <Navbar/> 
      <div className='row'>
        <div className='col-md-4'>
          <CreaturePanel connection = {connection}/>
        </div>
        <div className='col'>
          <InitiativePanel connection = {connection}/>
        </div>
      </div>
    </div>
  );
}

export default InitPage;