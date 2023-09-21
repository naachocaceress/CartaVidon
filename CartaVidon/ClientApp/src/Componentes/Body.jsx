import Rubros from "../Componentes/Rubros"

import "../Styles/body.css";
import imagenes from "../Assets/imagenes";


const Body = () => {
    return (
        <div className="body_container">

            <table>
                    <tr>
                        <td><Rubros texto="PAPAS" img={imagenes.lomoEj} /></td>
                        <td><Rubros texto="HAMBUR" img={imagenes.hamburEj} /></td>
                    </tr>
                    <tr>
                        <td><Rubros texto="PAPAS" img={imagenes.papasEj} /></td>
                        <td><Rubros texto="TOSTADOS" img={imagenes.tostadosEj} /></td>
                    </tr>
            </table>
            
            
            

        </div>
    )
}

export default Body;