
import React from 'react';

import "../Styles/boton.css";

const Boton = ({ texto, onClick }) => {
    return (
        <div className="boton_container">
            <button href="#" className="btn" onClick={onClick}>
                <span className="textBtn">{texto}</span>
            </button>
        </div>
    )
}

export default Boton;
