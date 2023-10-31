
import React from 'react';

import "../Styles/rubros.css";

const Rubros = ({ texto, img }) => {
    return (
        <div className="rubros_container">

            <a href="#" className="rbr">
                <div className="imagenRubro">
                    <img src={img} alt="Rubros" className="imgRubros"></img>
                </div>
                <div className="tituloRubro">
                    ~{texto}~
                </div>
            </a>

        </div>
    )
}

export default Rubros;
