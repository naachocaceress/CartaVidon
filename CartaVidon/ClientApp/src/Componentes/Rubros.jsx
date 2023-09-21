
import React from 'react';

import "../Styles/rubros.css";

const Rubros = ({ texto, img }) => {
    return (
        <div className="rubros_container">

            <div className="rbr">
                <div className="imagenRubro">
                    <img src={img} alt="Rubros" className="imgRubros"></img>
                </div>
                <div className="tituloRubro">
                    ~{texto}~
                </div>
            </div>

        </div>
    )
}

export default Rubros;
