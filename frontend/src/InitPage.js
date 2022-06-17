import React from 'react';
import Navbar from './Components/Navbar';
import CreaturePanel from './Components/CreaturePanel';

function InitPage() {
  return (
    <div style={{ width: '90%', margin: 'auto' }}>
      <Navbar/> 
      <div className='row'>
        <div className='col-md-4'>
          <CreaturePanel />
        </div>
        <div className='col'>
        </div>
      </div>
    </div>
  );
}

export default InitPage;