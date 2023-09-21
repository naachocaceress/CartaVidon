import React, { useState } from 'react';
import Boton from '../Componentes/Boton';

import "../Styles/navbar.css";
import imagenes from "../Assets/imagenes";

const Navbar = () => {
    // Define un estado para controlar la posición vertical del div.
    const [scrollY, setScrollY] = useState(0);

    // Función para desplazar el div hacia arriba.
    const handleScrollUp = () => {

        
        const newScrollY = scrollY - 1000; // Cambia 100 según tus necesidades.

        // Actualiza el estado para cambiar la posición del div.
        setScrollY(newScrollY);

        // Desplaza el div hacia la nueva posición.
        window.scrollTo(0, newScrollY);
    };

    return (
        <div className="navbar_container">

            <div className="divLogo">
                <img src={imagenes.logo2} alt='Logo central' className='img-circulo' />
            </div>

            <div className="divInfo">
                <img src={imagenes.wave1} alt='wave del fondo' className='wave2' />
            </div>

            <div className="contenedor_btn" style={{ transform: `translateY(${scrollY}px)` }}>
            
                <div className="divBotones">
                    <span className="textoSucu">Selecciona la sucursal en la que te encuentras:</span>
                    <div className="btn1">
                        <Boton texto={"ALTA CORDOBA"} onClick={handleScrollUp} />
                    </div>
                    <div className="btn1">
                        <Boton texto={"NUEVA CORDOBA"} onClick={handleScrollUp} />
                    </div>
                    <div className="btn1">
                        <Boton texto={"CERRO"} onClick={handleScrollUp} />
                    </div>
                    <div className="btn1">
                        <Boton texto={"BARRIO JARDIN"} onClick={handleScrollUp} />
                    </div>
                    <div className="btn1">
                        <Boton texto={"RONDEAU"} onClick={handleScrollUp} />
                    </div>
                    <div className="btn1">
                        <Boton texto={"VILLA ALLENDE"} onClick={handleScrollUp} />
                    </div>
                </div>


            </div>


        </div>
    )
}

export default Navbar;
