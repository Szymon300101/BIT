import React from 'react'

export default function Navbar() {
  return (
      <>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
            <a className="navbar-brand" href="#">BIT</a>
            <div className="collapse navbar-collapse" id="navbarSupportedContent">
                <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                <li className="nav-item">
                    <a className="nav-link active" aria-current="page" href="#">Inicjatywa</a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="#">Battlemapa</a>
                </li>
                </ul>
            </div>
            </div>
        </nav>
      </>
      
  )
}
