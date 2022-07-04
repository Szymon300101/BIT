import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import InitPage from './InitPage';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <InitPage />
  </React.StrictMode>
);

const params = {localHostPath: "https://localhost:7131"}
export default params;